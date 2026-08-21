using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTop.Models;

namespace MovieTop.Controllers;

public class MovieController : Controller
{
    private readonly MovieContext _context;
    private readonly IWebHostEnvironment _environment;

    public MovieController(
        MovieContext context,
        IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    public async Task<IActionResult> Index()
    {
        var movies = await _context.Movies.ToListAsync();
        return View(movies);
    }

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

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var movie = await _context.Movies.FindAsync(id);

        if (movie == null)
            return NotFound();

        return View(movie);
    }

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