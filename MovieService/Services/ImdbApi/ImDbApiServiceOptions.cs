using System;

namespace MovieService.Services.ImdbApi
{
    [Serializable]
    public class ImDbClientOptions
    {
        public string ApiBaseUrl { get; set; }
        public string ApiKey { get; set; }
        public string Lang { get; set; }
    }
}