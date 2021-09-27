using System;
using System.Text.Json.Serialization;

namespace MovieService.Services.ImdbApi.Models.Search
{
    [Serializable]
    public class ImdbSearchResponse : ImDbResponseBase
    {
        [JsonPropertyName("results")] public ImdbSearchResult[] Results { get; set; }
    }
}