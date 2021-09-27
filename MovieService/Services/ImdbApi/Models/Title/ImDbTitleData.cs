using System;
using System.Text.Json.Serialization;

namespace MovieService.Services.ImdbApi.Models.Title
{
    [Serializable]
    public class ImDbMovieInfo : ImDbResponseBase
    {
        [JsonPropertyName("id")] public string Id { get; set; }

        [JsonPropertyName("title")] public string Title { set; get; }

        [JsonPropertyName("image")] public string Image { get; set; }

        [JsonPropertyName("imDbRating")] public string ImDbRating { get; set; }
    }
}