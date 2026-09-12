using Microsoft.EntityFrameworkCore;
using MovieTop.Models;
using MovieTop.Services.Interfaces;

namespace MovieTop.Services;

public class MovieService : IMovieService
{
    private readonly MovieContext _context;
    private readonly IWebHostEnvironment _environment;

    public MovieService(MovieContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    public async Task<IEnumerable<Movie>> GetAllAsync()
    {
        return await _context.Movies.ToListAsync();
    }

    public async Task<Movie?> GetByIdAsync(int id)
    {
        return await _context.Movies.FindAsync(id);
    }

    public async Task CreateAsync(Movie movie, IFormFile? posterFile)
    {
        if (posterFile != null && posterFile.Length > 0)
        {
            movie.Poster = await SavePosterAsync(posterFile);
        }

        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Movie movie, IFormFile? posterFile)
    {
        var existingMovie = await _context.Movies.FindAsync(movie.Id);

        if (existingMovie == null)
            return;

        existingMovie.Title = movie.Title;
        existingMovie.Director = movie.Director;
        existingMovie.Genre = movie.Genre;
        existingMovie.Year = movie.Year;
        existingMovie.Description = movie.Description;

        if (posterFile != null && posterFile.Length > 0)
        {
            existingMovie.Poster = await SavePosterAsync(posterFile);
        }

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var movie = await _context.Movies.FindAsync(id);

        if (movie == null)
            return;

        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();
    }

    private async Task<string> SavePosterAsync(IFormFile posterFile)
    {
        var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");

        Directory.CreateDirectory(uploadsFolder);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(posterFile.FileName)}";
        var filePath = Path.Combine(uploadsFolder, fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await posterFile.CopyToAsync(stream);

        return $"/uploads/{fileName}";
    }
}