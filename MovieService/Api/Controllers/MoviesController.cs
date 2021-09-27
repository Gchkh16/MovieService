using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieService.Api.Models.Response;
using MovieService.Services.Abstractions;

namespace MovieService.Api.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("search")]
        public Task<ActionResult<MovieResponse[]>> SearchMovieAsync([Required(AllowEmptyStrings = false)] string title)
        {
            return _movieService.SearchMovieAsync(title);
        }

        [HttpPost("save")]
        public Task<ActionResult> SaveMovieAsync([Required] int userId, [Required(AllowEmptyStrings = false)] string movieId)
        {
            return _movieService.SaveMovieAsync(userId, movieId);
        }

        [HttpGet("saved-movies")]
        public Task<ActionResult<SavedMovieResponse[]>> GetSavedMoviesAsync([Required(AllowEmptyStrings = false)] int userId)
        {
            return _movieService.GetSavedMoviesAsync(userId);
        }

        [HttpPost("mark-as-watched")]
        public Task MarkMovieAsWatchedAsync([Required] int userId, [Required(AllowEmptyStrings = false)] string movieId)
        {
            return _movieService.MarkMovieAsWatchedAsync(userId, movieId);
        }
    }
}