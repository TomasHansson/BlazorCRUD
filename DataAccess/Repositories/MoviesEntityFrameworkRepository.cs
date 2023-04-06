using BlazorCrud.DataAccess.Repositories.Interfaces;
using BlazorCRUD.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCrud.DataAccess.Repositories
{
    public class MoviesEntityFrameworkRepository : IMoviesRepository
    {
        private readonly MoviesDbContext _context;

        public MoviesEntityFrameworkRepository(MoviesDbContext context)
        {
            _context = context;
        }

        public async Task<Movie> Create(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task<bool> Delete(int id)
        {
            return await _context.Movies.Where(x => x.Id == id).ExecuteDeleteAsync() > 0;
        }

        public async Task<List<Movie>> Get()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<Movie> Get(int id)
        {
            return await _context.Movies.FindAsync(id);
        }

        public async Task<bool> Update(Movie movie)
        {
            return await _context.Movies.Where(x => x.Id == movie.Id).ExecuteUpdateAsync(x => x.SetProperty(p => p.Title, movie.Title)) > 0;
        }
    }
}
