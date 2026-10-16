using MovieTop.DAL.Repositories;
using MovieTop.Models;

namespace MovieTop.DAL.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly MovieContext _context;

    public IRepository<Movie> Movies { get; }

    public UnitOfWork(MovieContext context)
    {
        _context = context;
        Movies = new Repository<Movie>(context);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}