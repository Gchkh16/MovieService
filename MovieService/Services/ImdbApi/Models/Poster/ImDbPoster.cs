using System;
using System.Text.Json.Serialization;

namespace MovieService.Services.ImdbApi.Models.Poster
{
    [Serializable]
    public class ImDbPoster
    {
        [JsonPropertyName("id")] public string Id { get; set; }

        [JsonPropertyName("link")] public string Link { get; set; }

        [JsonPropertyName("aspectRatio")] public double AspectRatio { get; set; }

        [JsonPropertyName("language")] public string Language { get; set; }

        [JsonPropertyName("width")] public int Width { get; set; }

        [JsonPropertyName("height")] public int Height { get; set; }
    }
}