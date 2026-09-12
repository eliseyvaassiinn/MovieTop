using Microsoft.AspNetCore.Mvc;
using MovieTop.Models;
using MovieTop.Services.Interfaces;

namespace MovieTop.Controllers;

public class MovieController : Controller
{
    private readonly IMovieService _movieService;

    public MovieController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    public async Task<IActionResult> Index()
    {
        var movies = await _movieService.GetAllAsync();
        return View(movies);
    }

    public async Task<IActionResult> Details(int id)
    {
        var movie = await _movieService.GetByIdAsync(id);

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

        await _movieService.CreateAsync(movie, posterFile);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var movie = await _movieService.GetByIdAsync(id);

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

        await _movieService.UpdateAsync(movie, posterFile);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var movie = await _movieService.GetByIdAsync(id);

        if (movie == null)
            return NotFound();

        return View(movie);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _movieService.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }
}