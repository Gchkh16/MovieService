using System;

namespace MovieService.Api.Models.Response
{
    [Serializable]
    public class MovieResponse
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
    }
}