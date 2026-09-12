using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTop.Models;

namespace MovieTop.Controllers;

/// <summary>
/// Контроллер для управления фильмами.
/// </summary>
public class MovieController : Controller
{
    private readonly MovieContext _context;
    private readonly IWebHostEnvironment _environment;

    /// <summary>
    /// Инициализирует контроллер фильмов.
    /// </summary>
    /// <param name="context">Контекст базы данных фильмов.</param>
    /// <param name="environment">Окружение веб-приложения для работы с файлами.</param>
    public MovieController(
        MovieContext context,
        IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    /// <summary>
    /// Возвращает список всех фильмов.
    /// </summary>
    /// <returns>Представление со списком фильмов.</returns>
    public async Task<IActionResult> Index()
    {
        var movies = await _context.Movies.ToListAsync();

        return View(movies);
    }

    /// <summary>
    /// Возвращает подробную информацию о выбранном фильме.
    /// </summary>
    /// <param name="id">Идентификатор фильма.</param>
    /// <returns>Представление с информацией о фильме или результат 404.</returns>
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var movie = await _context.Movies
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null)
            return NotFound();

        return View(movie);
    }

    /// <summary>
    /// Открывает форму создания нового фильма.
    /// </summary>
    /// <returns>Представление с формой создания фильма.</returns>
    public IActionResult Create()
    {
        return View();
    }

    /// <summary>
    /// Создаёт новый фильм и сохраняет его в базе данных.
    /// </summary>
    /// <param name="movie">Данные нового фильма.</param>
    /// <param name="posterFile">Файл постера фильма.</param>
    /// <returns>Перенаправление к списку фильмов или форма с ошибками валидации.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Movie movie, IFormFile? posterFile)
    {
        if (!ModelState.IsValid)
            return View(movie);

        if (posterFile != null && posterFile.Length > 0)
        {
            var uploadsFolder = Path.Combine(
                _environment.WebRootPath,
                "images");

            Directory.CreateDirectory(uploadsFolder);

            var extension = Path.GetExtension(posterFile.FileName);
            var fileName = Guid.NewGuid() + extension;

            var filePath = Path.Combine(uploadsFolder, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await posterFile.CopyToAsync(stream);

            movie.Poster = "/images/" + fileName;
        }

        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Открывает форму редактирования фильма.
    /// </summary>
    /// <param name="id">Идентификатор фильма.</param>
    /// <returns>Представление с формой редактирования или результат 404.</returns>
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var movie = await _context.Movies.FindAsync(id);

        if (movie == null)
            return NotFound();

        return View(movie);
    }

    /// <summary>
    /// Обновляет данные существующего фильма.
    /// </summary>
    /// <param name="id">Идентификатор редактируемого фильма.</param>
    /// <param name="movie">Обновлённые данные фильма.</param>
    /// <param name="posterFile">Новый файл постера фильма.</param>
    /// <returns>Перенаправление к списку фильмов или форма с ошибками валидации.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        Movie movie,
        IFormFile? posterFile)
    {
        if (id != movie.Id)
            return NotFound();

        if (!ModelState.IsValid)
            return View(movie);

        if (posterFile != null && posterFile.Length > 0)
        {
            var uploadsFolder = Path.Combine(
                _environment.WebRootPath,
                "images");

            Directory.CreateDirectory(uploadsFolder);

            var extension = Path.GetExtension(posterFile.FileName);
            var fileName = Guid.NewGuid() + extension;

            var filePath = Path.Combine(uploadsFolder, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await posterFile.CopyToAsync(stream);

            movie.Poster = "/images/" + fileName;
        }

        _context.Movies.Update(movie);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Открывает страницу подтверждения удаления фильма.
    /// </summary>
    /// <param name="id">Идентификатор фильма.</param>
    /// <returns>Представление подтверждения удаления или результат 404.</returns>
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var movie = await _context.Movies
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null)
            return NotFound();

        return View(movie);
    }

    /// <summary>
    /// Удаляет фильм из базы данных.
    /// </summary>
    /// <param name="id">Идентификатор удаляемого фильма.</param>
    /// <returns>Перенаправление к списку фильмов.</returns>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var movie = await _context.Movies.FindAsync(id);

        if (movie != null)
        {
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}