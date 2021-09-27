using System;
using System.Text.Json.Serialization;

namespace MovieService.Services.ImdbApi.Models.Wikipedia
{
    [Serializable]
    public class ImDbPlotShort
    {
        [JsonPropertyName("plainText")] public string PlainText { get; set; }

        [JsonPropertyName("html")] public string Html { get; set; }
    }
}