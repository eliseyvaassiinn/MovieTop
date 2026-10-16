using Microsoft.AspNetCore.Mvc;
using MovieTop.DAL.UnitOfWork;
using MovieTop.Models;

namespace MovieTop.Controllers;

public class MovieController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _environment;

    public MovieController(
        IUnitOfWork unitOfWork,
        IWebHostEnvironment environment)
    {
        _unitOfWork = unitOfWork;
        _environment = environment;
    }

    public async Task<IActionResult> Index()
    {
        var movies = await _unitOfWork.Movies.GetAllAsync();
        return View(movies);
    }

    public async Task<IActionResult> Details(int id)
    {
        var movie = await _unitOfWork.Movies.GetByIdAsync(id);

        if (movie == null)
            return NotFound();

        return View(movie);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Movie movie, IFormFile? posterFile)
    {
        if (!ModelState.IsValid)
            return View(movie);

        if (posterFile != null && posterFile.Length > 0)
        {
            movie.Poster = await SavePosterAsync(posterFile);
        }

        await _unitOfWork.Movies.AddAsync(movie);
        await _unitOfWork.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var movie = await _unitOfWork.Movies.GetByIdAsync(id);

        if (movie == null)
            return NotFound();

        return View(movie);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Movie movie, IFormFile? posterFile)
    {
        if (!ModelState.IsValid)
            return View(movie);

        var existingMovie = await _unitOfWork.Movies.GetByIdAsync(movie.Id);

        if (existingMovie == null)
            return NotFound();

        existingMovie.Title = movie.Title;
        existingMovie.Director = movie.Director;
        existingMovie.Genre = movie.Genre;
        existingMovie.Year = movie.Year;
        existingMovie.Description = movie.Description;

        if (posterFile != null && posterFile.Length > 0)
        {
            existingMovie.Poster = await SavePosterAsync(posterFile);
        }

        _unitOfWork.Movies.Update(existingMovie);
        await _unitOfWork.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var movie = await _unitOfWork.Movies.GetByIdAsync(id);

        if (movie == null)
            return NotFound();

        return View(movie);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var movie = await _unitOfWork.Movies.GetByIdAsync(id);

        if (movie == null)
            return NotFound();

        _unitOfWork.Movies.Delete(movie);
        await _unitOfWork.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    private async Task<string> SavePosterAsync(IFormFile posterFile)
    {
        var webRoot = _environment.WebRootPath
            ?? Path.Combine(_environment.ContentRootPath, "wwwroot");

        var uploadsFolder = Path.Combine(webRoot, "uploads");

        Directory.CreateDirectory(uploadsFolder);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(posterFile.FileName)}";
        var filePath = Path.Combine(uploadsFolder, fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await posterFile.CopyToAsync(stream);

        return $"/uploads/{fileName}";
    }
}