using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MovieService.Services.Abstractions;
using MovieService.Services.ImdbApi.Models;
using MovieService.Services.ImdbApi.Models.Poster;
using MovieService.Services.ImdbApi.Models.Search;
using MovieService.Services.ImdbApi.Models.Title;
using MovieService.Services.ImdbApi.Models.Wikipedia;

namespace MovieService.Services.ImdbApi
{
    public class ImDbApiService : IImDbApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ImDbClientOptions _imDbClientOptions;

        public ImDbApiService(IOptionsSnapshot<ImDbClientOptions> options, HttpClient httpClient)
        {
            _imDbClientOptions = options.Value;
            _httpClient = httpClient;
        }

        public async Task<ImdbSearchResult[]> SearchAsync(string expression)
        {
            var resp = await ProcessRequestAsync<ImdbSearchResponse>("Search", expression);
            return resp.Results;
        }

        public Task<ImDbMovieInfo> GetInfo(string movieId)
        {
            return ProcessRequestAsync<ImDbMovieInfo>("Title", movieId);
        }

        public async Task<ImDbPoster[]> GetPosters(string movieId)
        {
            var resp = await ProcessRequestAsync<ImDbPostersResponse>("Posters", movieId);
            return resp.Posters;
        }

        public async Task<ImDbPlotShort> GetShortPlot(string movieId)
        {
            var resp = await ProcessRequestAsync<ImiDbWikiResponse>("Wikipedia", movieId);
            return resp.PlotShort;
        }


        private async Task<TResponse> ProcessRequestAsync<TResponse>(string operationName, params string[] parameters)
            where TResponse : IImDbResponse
        {
            var baseUrl = new Uri(_imDbClientOptions.ApiBaseUrl);
            var relativeUrl = $"{_imDbClientOptions.Lang}/API/{operationName}/{_imDbClientOptions.ApiKey}/{string.Join('/', parameters)}";
            var url = new Uri(baseUrl, relativeUrl);

            HttpResponseMessage resp;

            try
            {
                resp = await _httpClient.GetAsync(url);
            }
            catch (Exception e)
            {
                throw new IImDbApiService.ServiceException($"can't get response: {e.Message}");
            }

            if (resp.StatusCode != HttpStatusCode.OK) throw new IImDbApiService.ServiceException($"Bad ImDb http status code: {(int)resp.StatusCode}");

            TResponse data;

            try
            {
                data = await JsonSerializer.DeserializeAsync<TResponse>(await resp.Content.ReadAsStreamAsync());
            }
            catch (Exception e)
            {
                throw new IImDbApiService.ServiceException($"got bad-formatted response: {e.Message}");
            }

            if (data == null) throw new IImDbApiService.ServiceException("got null response");

            if (data.ErrorMessage != null) throw new IImDbApiService.ServiceException($"got error message: {data.ErrorMessage}");

            return data;
        }
    }
}