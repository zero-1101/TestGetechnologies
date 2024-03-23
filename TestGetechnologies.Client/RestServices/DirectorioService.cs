using System.Net.Http.Json;
using TestGetechnologies.Shared;

namespace TestGetechnologies.Client.RestServices
{
    public class DirectorioService
    {
        private readonly HttpClient _httpClient;

        public DirectorioService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<ResponseApi<PersonaDetail>?> CreatePersona(CreatePersona createPersona)
        {
             HttpResponseMessage rawResult = await _httpClient.PostAsJsonAsync("api/DirectorioRestService/CreatePersona", createPersona);
            return await rawResult.Content.ReadFromJsonAsync<ResponseApi<PersonaDetail>>();
        }
    }
}
