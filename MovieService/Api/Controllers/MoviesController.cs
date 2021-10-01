using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieService.Api.Models.Request;
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
        public Task<ActionResult<MovieResponse[]>> SearchMovieAsync([FromQuery] SearchRequest request)
        {
            return _movieService.SearchMovieAsync(request.Title);
        }

        [HttpPost("save")]
        public Task<ActionResult> SaveMovieAsync([FromQuery] MovieRequest request)
        {
            return _movieService.SaveMovieAsync(request.UserId, request.MovieId);
        }

        [HttpGet("saved-movies")]
        public Task<ActionResult<SavedMovieResponse[]>> GetSavedMoviesAsync([FromQuery] GetSavedMoviesRequest request)
        {
            return _movieService.GetSavedMoviesAsync(request.UserId);
        }

        [HttpPost("mark-as-watched")]
        public Task MarkMovieAsWatchedAsync([FromQuery] MovieRequest request)
        {
            return _movieService.MarkMovieAsWatchedAsync(request.UserId, request.MovieId);
        }
    }
}