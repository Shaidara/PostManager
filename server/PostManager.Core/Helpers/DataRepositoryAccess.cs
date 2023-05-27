using Microsoft.Extensions.Logging;
using PostManager.Core.Interfaces;
using PostManager.Core.Models;
using System.Net.Http.Json;

namespace PostManager.Core.Helpers
{
    public class DataRepositoryAccess : IDataRepositoryAccess
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<DataRepositoryAccess> _logger;

        public DataRepositoryAccess(IHttpClientFactory httpClientFactory, ILogger<DataRepositoryAccess> logger) 
        {
            _httpClient = httpClientFactory.CreateClient("DataRepositoryAccess");
            _logger = logger;
        }

        public async Task<IEnumerable<Post>> GetPosts(string tag)
        {
            string endpointPath = "posts";

            try
            {
                Uri requestUri = new Uri($"{_httpClient.BaseAddress}/{endpointPath}?tag={tag}");
                var response = await _httpClient.GetFromJsonAsync<PostApiResponse>(requestUri);

                if( response == null )
                {
                    _logger.LogInformation($"Unable to get posts for [tag={tag}]");
                    throw new InvalidOperationException();
                }

                return response.Posts;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception - Unable to get posts for [tag={tag}]", ex);
                throw new InvalidOperationException();
            }

        }
    }
}
