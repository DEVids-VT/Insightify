using Insightify.IdentityAPI.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace Insightify.IdentityAPI.Services.AccountSettings
{
    public class AccountSettingsService : IAccountSettingsService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly HttpClient _httpClient;

        public AccountSettingsService(UserManager<ApplicationUser> userManager, HttpClient httpClient)
        {
            _userManager = userManager;
            _httpClient = httpClient;
        }

        public async Task EditProfile(ApplicationUserEditModel user, string uId)
        {
            var foundUser = await _userManager.FindByIdAsync(uId);

            if (foundUser != null)
            {
                if (user.ProfilePicture != null)
                {
                    var url = await UploadToImgur(user.ProfilePicture, _httpClient);
                    foundUser.ProfilePicture = url;
                }
                else
                {
                    if(user.UserName != null)
                    {
                        foundUser.UserName = user.UserName;
                    }
                    if (user.Email != null && user.Email.Contains('@'))
                    {
                        foundUser.Email = user.Email;
                    }
                }

                await _userManager.UpdateAsync(foundUser);
            }
        }
        private async Task<string> UploadToImgur(IFormFile imageFile, HttpClient _httpClient)
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
