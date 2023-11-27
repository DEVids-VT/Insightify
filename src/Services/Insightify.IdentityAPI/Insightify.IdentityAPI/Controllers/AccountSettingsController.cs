﻿using Insightify.IdentityAPI.Models;
using Insightify.IdentityAPI.Services.AccountSettings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Insightify.IdentityAPI.Controllers
{
    [ApiController]
    [AllowAnonymous]
    public class AccountSettingsController : Controller
    {
        private readonly IAccountSettingsService _accountSettingsService;

        public AccountSettingsController(IAccountSettingsService accountSettingsService)
        {
            _accountSettingsService = accountSettingsService;
        }

        [HttpPut]
        [Route("/editProfile")]
        public async Task<IActionResult> EditProfile(ApplicationUserEditModel user)
        {
            var result = await _accountSettingsService.EditProfile(user);

            return Ok(result);
        }
    }
}