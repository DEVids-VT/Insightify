using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace Insightify.MVC.Services
{
    public static class UploadImage
    {
        public static async Task<string> ToImgur(IFormFile imageFile, HttpClient _httpClient)
        {
            using var formContent = new MultipartFormDataContent();
            using var imageStream = imageFile.OpenReadStream();
            using var streamContent = new StreamContent(imageStream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(imageFile.ContentType);

            formContent.Add(streamContent, "image", imageFile.FileName);

            var response = await _httpClient.PostAsync("https://api.imgur.com/3/upload", formContent);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Image upload failed with status code: {response.StatusCode}");
            }

            var jsonResponse = JObject.Parse(responseContent);
            return jsonResponse["data"]["link"].ToString();
        }
    }
}
