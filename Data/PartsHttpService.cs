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
            return await _client.GetFromJsonAsync<IList<Models.PartModel>>("https://erozaniec.azurewebsites.net/api/InfoFn?code=KCP31CFg6G3wMcDt2hNZs6zMcrC59-5mBx3OV0hA9t4yAzFu1bXybQ%3D%3D")
                .ConfigureAwait(false);
        }
    }
}