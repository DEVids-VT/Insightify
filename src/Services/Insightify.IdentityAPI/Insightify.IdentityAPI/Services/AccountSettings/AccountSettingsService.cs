﻿using Insightify.IdentityAPI.Models;
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
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Client-ID", "41519381aee37da");
        }

        public async Task EditProfile(ApplicationUserEditModel user, string uId)
        {
            var foundUser = await _userManager.FindByIdAsync(uId);

            if (foundUser != null)
            {
                if (user.ProfilePicture != null)
                {
                    try
                    {
                        var url = await UploadToImgur(user.ProfilePicture, _httpClient);

                        foundUser.ProfilePicture = url;
                    }
                    catch(Exception ex) { }
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

        public async Task<UserProfileModel> Profile(string uId)
        {
            var profile = await _userManager.FindByIdAsync(uId);

            if (profile != null)
            {
                return new UserProfileModel
                {
                    Username = profile.UserName!,
                    Img = profile.ProfilePicture!
                };
            }

            return new UserProfileModel
            {
                Username = "s1lence"
            };
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
