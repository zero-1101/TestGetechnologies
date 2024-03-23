using TestGetechnologies.Shared;

namespace TestGetechnologies.Client.Helpers
{
    public static class HttpResponseMessageExtensions
    {
        public async static Task<ResponseApi<T>?> GetResponseJsonFromRawResult<T>(HttpResponseMessage rawResult)
        {
            return await rawResult.Content.ReadFromJsonAsync<ResponseApi<T>>();
        }
    }
}
