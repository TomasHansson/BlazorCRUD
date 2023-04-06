using BlazorCRUD.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorCRUD.Client.Pages
{
    public partial class Movies
    {
        [Inject]
        public HttpClient Client { get; set; }

        private List<Movie> _movies = new List<Movie>();
        private Movie _newMovie;
        private Movie _selectedMovie;

        protected override async Task OnInitializedAsync()
        {
            await LoadMovies();
        }

        private async Task LoadMovies()
        {
            var result = await Client.GetFromJsonAsync<List<Movie>>("api/Movies");
            if (result is not null)
            {
                _movies = result;
                _newMovie = new Movie();
                _selectedMovie = null;
            }
        }

        private void Select(Movie movie)
        {
            _selectedMovie = new Movie { Id = movie.Id, Title = movie.Title };
        }

        private async Task Create()
        {
            if (_newMovie is not null && _newMovie.Id == 0 && !string.IsNullOrWhiteSpace(_newMovie.Title))
            {
                var result = await Client.PostAsJsonAsync("api/Movies", _newMovie);
                if (result.IsSuccessStatusCode)
                {
                    await LoadMovies();
                }
            }
        }

        private async Task Delete(int id)
        {
            var result = await Client.DeleteAsync($"api/Movies/{id}");
            if (result.IsSuccessStatusCode)
            {
                await LoadMovies();
            }
        }

        private async Task Update()
        {
            if (_selectedMovie is not null && _selectedMovie.Id != 0 && !string.IsNullOrWhiteSpace(_selectedMovie.Title))
            {
                var result = await Client.PutAsJsonAsync("api/Movies", _selectedMovie);
                if (result.IsSuccessStatusCode)
                {
                    await LoadMovies();
                }
            }
        }
    }
}
