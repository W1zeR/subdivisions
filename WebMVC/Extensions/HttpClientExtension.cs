using Newtonsoft.Json;

namespace WebMVC.Extensions
{
    public static class HttpClientExtension
    {
        public static async Task<T> ReadContentAsync<T>(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode == false)
            {
                throw new ApplicationException($"Что-то пошло не так при вызове API: {response.ReasonPhrase}");
            }

            var dataAsString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(dataAsString);

            return result!;
        }
    }
}
