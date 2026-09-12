using MovieTop.Models;

namespace MovieTop.Services.Interfaces;

public interface IMovieService
{
    Task<IEnumerable<Movie>> GetAllAsync();
    Task<Movie?> GetByIdAsync(int id);
    Task CreateAsync(Movie movie, IFormFile? posterFile);
    Task UpdateAsync(Movie movie, IFormFile? posterFile);
    Task DeleteAsync(int id);
}