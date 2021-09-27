using System;

namespace MovieService.Api.Models.Response
{
    [Serializable]
    public class SavedMovieResponse
    {
        public bool Watched { get; set; }
        public MovieResponse Movie { get; set; }
    }
}