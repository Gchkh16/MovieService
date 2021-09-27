using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieService.Api.Models.Response;
using MovieService.Database;
using MovieService.Database.Models;
using MovieService.Services.Abstractions;
using MovieService.Services.ImdbApi.Models.Title;

namespace MovieService.Services.MovieService
{
    public class MovieService : IMovieService
    {
        private readonly IImDbApiService _imDbApiService;
        private readonly MoviesDb _moviesDb;

        public MovieService(IImDbApiService imDbApiService, MoviesDb moviesDb)
        {
            _imDbApiService = imDbApiService;
            _moviesDb = moviesDb;
        }

        public async Task<ActionResult<MovieResponse[]>> SearchMovieAsync(string title)
        {
            var searchData = await _imDbApiService.SearchAsync(title);

            return searchData.Select(x => new MovieResponse
            {
                Id = x.Id,
                Title = x.Title,
                ImageUrl = x.Image
            }).ToArray();
        }

        public async Task<ActionResult> SaveMovieAsync(int userId, string movieId)
        {
            ImDbMovieInfo movieInfo;

            try
            {
                movieInfo = await _imDbApiService.GetInfo(movieId);
            }
            catch (IImDbApiService.ServiceException e)
            {
                return InternalServerError(e.Message);
            }

            if (_moviesDb.SavedMovies.Any(m => m.UserId == userId && m.MovieId == movieId)) return new StatusCodeResult(StatusCodes.Status409Conflict);

            _moviesDb.SavedMovies.Add(new SavedMovie
            {
                UserId = userId,
                MovieId = movieInfo.Id,
                Watched = false
            });

            if (!_moviesDb.Set<Movie>().Any(m => m.Id == movieId))
                _moviesDb.Set<Movie>().Add(new Movie
                {
                    Id = movieId,
                    Title = movieInfo.Title,
                    ImdbRating = decimal.Parse(movieInfo.ImDbRating, CultureInfo.InvariantCulture),
                    Image = movieInfo.Image
                });

            await _moviesDb.SaveChangesAsync();
            return new OkResult();
        }

        public async Task<ActionResult<SavedMovieResponse[]>> GetSavedMoviesAsync(int userId)
        {
            var savedMovies = _moviesDb.SavedMovies.Where(m => m.UserId == userId).Include(m => m.Movie);
            return await savedMovies.Select(m => new SavedMovieResponse
            {
                Watched = m.Watched,
                Movie = new MovieResponse
                {
                    Id = m.MovieId,
                    Title = m.Movie.Title,
                    ImageUrl = m.Movie.Image
                }
            }).ToArrayAsync();
        }

        public async Task<ActionResult> MarkMovieAsWatchedAsync(int userId, string movieId)
        {
            var savedMovie = await _moviesDb.SavedMovies.FirstOrDefaultAsync(m => m.MovieId == movieId && m.UserId == userId);
            if (savedMovie == null) return new NotFoundResult();

            savedMovie.Watched = true;
            await _moviesDb.SaveChangesAsync();
            return new OkResult();
        }

        private static ActionResult InternalServerError(string errorMessage)
        {
            return new ObjectResult(StatusCodes.Status500InternalServerError)
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Value = new
                {
                    ErrorMessage = $"ImDb api error: ${errorMessage}"
                }
            };
        }
    }
}