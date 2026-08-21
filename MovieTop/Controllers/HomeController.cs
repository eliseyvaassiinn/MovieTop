using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTop.Models;

namespace MovieTop.Controllers;

public class HomeController : Controller
{
    private readonly MovieContext _context;

    public HomeController(MovieContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var movies = await _context.Movies.ToListAsync();

        return View(movies);
    }

    public IActionResult NotFound()
    {
        return View();
    }
}