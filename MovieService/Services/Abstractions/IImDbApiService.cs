using System;
using System.Threading.Tasks;
using MovieService.Services.ImdbApi.Models.Poster;
using MovieService.Services.ImdbApi.Models.Search;
using MovieService.Services.ImdbApi.Models.Title;
using MovieService.Services.ImdbApi.Models.Wikipedia;

namespace MovieService.Services.Abstractions
{
    public interface IImDbApiService
    {
        Task<ImdbSearchResult[]> SearchAsync(string expression);
        Task<ImDbMovieInfo> GetInfo(string movieId);
        Task<ImDbPoster[]> GetPosters(string movieId);
        Task<ImDbPlotShort> GetShortPlot(string movieId);

        #region ExceptionDefinitions

        public class ServiceException : Exception
        {
            public ServiceException(string message) : base(message)
            {
            }
        }

        #endregion
    }
}