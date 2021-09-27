using System;
using System.Text.Json.Serialization;

namespace MovieService.Services.ImdbApi.Models.Search
{
    [Serializable]
    public class ImdbSearchResult : ImDbResponseBase
    {
        [JsonPropertyName("id")] public string Id { get; set; }

        [JsonPropertyName("image")] public string Image { get; set; }

        [JsonPropertyName("title")] public string Title { get; set; }
    }
}