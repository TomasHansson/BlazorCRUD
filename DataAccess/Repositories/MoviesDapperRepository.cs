using BlazorCrud.DataAccess.Repositories.Interfaces;
using BlazorCRUD.Shared.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCRUD.DataAccess.Repositories
{
    public class MoviesDapperRepository : IMoviesRepository
    {
        private readonly string _connectionString;

        public MoviesDapperRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "Server=<Insert Server Name Here>;Database=MoviesDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";
        }

        public async Task<Movie> Create(Movie movie)
        {
            var sql = "INSERT INTO Movies (Title) VALUES (@Title) SELECT SCOPE_IDENTITY()";
            var parameters = new { movie.Title };
            using var connection = new SqlConnection(_connectionString);
            var movieId = await connection.ExecuteAsync(sql, parameters);
            movie.Id = movieId;
            return movie;
        }

        public async Task<bool> Delete(int id)
        {
            var sql = "DELETE FROM Movies WHERE Id = @Id";
            var parameters = new { Id = id };
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteAsync(sql, parameters) > 0;
        }

        public async Task<List<Movie>> Get()
        {
            var sql = "SELECT * FROM Movies";
            using var connection = new SqlConnection(_connectionString);
            return (await connection.QueryAsync<Movie>(sql)).ToList();
        }

        public async Task<Movie> Get(int id)
        {
            var sql = "SELECT * FROM Movies WHERE Id = @Id";
            var parameters = new { Id = id };
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Movie>(sql, parameters);
        }

        public async Task<bool> Update(Movie movie)
        {
            var sql = "UPDATE Movies SET Title = @Title WHERE Id = @Id";
            var parameters = new { movie.Id, movie.Title };
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteAsync(sql, parameters) > 0;
        }
    }
}
