using System.Net.Http.Json;

namespace ERozaniec.Data
{
    public class PartsHttpService
    {
        private readonly HttpClient _client;

        public PartsHttpService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IList<Models.PartModel>?> GetPartsAsync()
        {
            return await _client.GetFromJsonAsync<IList<Models.PartModel>>("https://erozaniec.azurewebsites.net/api/InfoFn")
                .ConfigureAwait(false);
        }
    }
}