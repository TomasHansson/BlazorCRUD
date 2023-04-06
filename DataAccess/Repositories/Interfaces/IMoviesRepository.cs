using BlazorCRUD.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCrud.DataAccess.Repositories.Interfaces
{
    public interface IMoviesRepository
    {
        Task<List<Movie>> Get();
        Task<Movie> Get(int id);
        Task<Movie> Create(Movie movie);
        Task<bool> Update(Movie movie);
        Task<bool> Delete(int id);
    }
}
