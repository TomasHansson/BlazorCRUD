using BlazorCRUD.Shared.Models;
using BlazorCrud.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCrud.DataAccess.Repositories
{
    public class MoviesInMemoryRepository : IMoviesRepository
    {
        private readonly List<Movie> _movies;

        public MoviesInMemoryRepository()
        {
            _movies = new List<Movie>
            {
                new Movie { Id = 1, Title = "First"},
                new Movie { Id = 2, Title = "Second"},
                new Movie { Id = 3, Title = "Third"}
            };
        }

        public Task<Movie> Create(Movie movie)
        {
            if (movie.Id != 0)
            {
                throw new ArgumentException("Id must be 0 for a new Movie.");
            }

            movie.Id = _movies.Max(x => x.Id) + 1;
            _movies.Add(movie);

            return Task.FromResult(movie);
        }

        public Task<bool> Delete(int id)
        {
            var movie = _movies.FirstOrDefault(x => x.Id == id);
            if (movie is null)
            {
                return Task.FromResult(false);
            }

            _movies.Remove(movie);
            return Task.FromResult(true);
        }

        public Task<List<Movie>> Get()
        {
            return Task.FromResult(_movies.GetRange(0, _movies.Count));
        }

        public Task<Movie> Get(int id)
        {
            return Task.FromResult(_movies.FirstOrDefault(x => x.Id == id));
        }

        public Task<bool> Update(Movie movie)
        {
            var movieInMemory = _movies.FirstOrDefault(x => x.Id == movie.Id);
            if (movieInMemory is null)
            {
                return Task.FromResult(false);
            }

            movieInMemory.Title = movie.Title;
            return Task.FromResult(true);
        }
    }
}
