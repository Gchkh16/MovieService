using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieService.Api.Models.Response;

namespace MovieService.Services.Abstractions
{
    public interface IMovieService
    {
        Task<ActionResult<MovieResponse[]>> SearchMovieAsync(string title);
        Task<ActionResult> SaveMovieAsync(int userId, string movieId);
        Task<ActionResult<SavedMovieResponse[]>> GetSavedMoviesAsync(int userId);
        Task<ActionResult> MarkMovieAsWatchedAsync(int userId, string movieId);
    }
}