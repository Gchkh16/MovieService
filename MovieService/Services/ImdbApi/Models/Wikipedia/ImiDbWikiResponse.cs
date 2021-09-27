using System;
using System.Text.Json.Serialization;

namespace MovieService.Services.ImdbApi.Models.Wikipedia
{
    [Serializable]
    public class ImiDbWikiResponse : ImDbResponseBase
    {
        [JsonPropertyName("plotShort")] public ImDbPlotShort PlotShort { get; set; }
    }
}