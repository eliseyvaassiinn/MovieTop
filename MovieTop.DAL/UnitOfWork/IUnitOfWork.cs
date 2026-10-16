using MovieTop.DAL.Repositories;
using MovieTop.Models;

namespace MovieTop.DAL.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IRepository<Movie> Movies { get; }

    Task<int> SaveChangesAsync();
}