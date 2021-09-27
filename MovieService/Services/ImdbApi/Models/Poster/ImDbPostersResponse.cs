using System;
using System.Text.Json.Serialization;

namespace MovieService.Services.ImdbApi.Models.Poster
{
    [Serializable]
    public class ImDbPostersResponse : ImDbResponseBase
    {
        [JsonPropertyName("posters")] public ImDbPoster[] Posters { get; set; }
    }
}