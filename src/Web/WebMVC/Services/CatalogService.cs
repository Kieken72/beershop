using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Resilience.Http;
using WebMVC.ViewModels;

namespace WebMVC.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IOptionsSnapshot<AppSettings> _settings;
        private IHttpClient _apiClient;
        private readonly string _remoteServiceBaseUrl;
  
        public CatalogService(IOptionsSnapshot<AppSettings> settings, ILoggerFactory loggerFactory, IHttpClient httpClient) {
            _settings = settings;
            _remoteServiceBaseUrl = $"{_settings.Value.CatalogUrl}/api/v1/catalog/";
            _apiClient = httpClient;
            var log = loggerFactory.CreateLogger("catalog service");
            log.LogDebug(settings.Value.CatalogUrl);
        }
         
        public async Task<Catalog> GetCatalogItems(int page,int take)
        {
            var catalogUrl = $"{_remoteServiceBaseUrl}items?pageIndex={page}&pageSize={take}";

            var dataString = "";

            //
            // Using a HttpClient wrapper with Retry and Exponential Backoff
            //
            dataString = await _apiClient.GetStringAsync(catalogUrl);
            var response = JsonConvert.DeserializeObject<Catalog>(dataString);
            return response;
        }
    }
}
