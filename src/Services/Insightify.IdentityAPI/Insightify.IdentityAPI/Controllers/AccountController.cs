using System.Text;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;

using FluentValidation;
using Insightify.IdentityAPI.EmailSending;
using Insightify.IdentityAPI.Models;
using Insightify.IdentityAPI.Options;
using Insightify.IdentityAPI.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace Insightify.IdentityAPI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IEventService _events;
        private readonly ILogger _logger;
        private readonly IMailSender _mailSender;
        private readonly IValidator<RegisterInputViewModel> _registerValidator;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IIdentityServerInteractionService interaction,
            ILogger<AccountController> logger,
            IEventService events,
            IValidator<RegisterInputViewModel> registerValidator,
            IMailSender mailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _interaction = interaction;
            _events = events;
            _logger = logger;
            _registerValidator = registerValidator;
            _mailSender = mailSender;
        }

        /// <summary>
        /// Entry point into the login workflow
        /// </summary>
        [HttpGet]
        [Route("login")]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        /// <summary>
        /// Handle postback from username/password login
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("login")]
        public async Task<IActionResult> Login(LoginInputViewModel model, string button)
        {
            var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);

            if (button != "login")
            {
                if (context != null)
                {
                    await _interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);

                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    return Redirect("~/");
                }
            }

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberLogin, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.Username);

                    _logger.LogInformation("user login success for {0}", model.Username);

                    if (context != null)
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    if (Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else if (string.IsNullOrEmpty(model.ReturnUrl))
                    {
                        return Redirect("~/");
                    }
                    else
                    {
                        throw new Exception("invalid return URL");
                    }
                }

                _logger.LogInformation("invalid credentials for {0}", model.Username);

                ModelState.AddModelError(string.Empty, AccountOptions.InvalidCredentialsErrorMessage);
            }

            ViewData["ReturnUrl"] = model.ReturnUrl;

            return View();
        }

        [HttpGet]
        [Authorize]
        [Route("logout")]

        public async Task<IActionResult> Logout()
        {
            _logger.LogInformation($"Logging out user.");

            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        [Route("register")]

        public IActionResult Register(string returnUrl)
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("~/");
            }

            var model = new RegisterInputViewModel() {ReturnUrl = returnUrl};

            return View(model);
        }

        [HttpPost]
        [Route("register")]

        public async Task<IActionResult> Register(RegisterInputViewModel model)
        {
            var resultValidation = await _registerValidator.ValidateAsync(model);
            if (!resultValidation.IsValid || !ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.Username
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.UpdateSecurityStampAsync(user);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var token =  WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                string callbackUrl = Url.Action("ConfirmEmail", "Account", new { user.Id, token, returnUrl = model.ReturnUrl }, Request.Scheme)!;

                await _mailSender.SendEmailAsync(new EmailMessage(){ To = model.Email, Subject = "Email Confirmation for Insightify", Content = callbackUrl });

                _logger.LogInformation("Succesfully registered new user with username: {0}", model.Username);
                return RedirectToAction(nameof(AccountController.Login), "Account", new{ returnUrl = model.ReturnUrl } );
            }

            foreach (var item in resultValidation.Errors)
            {
                ModelState.AddModelError("", item.ErrorMessage);
                _logger.LogInformation(item.ErrorMessage);
            }

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string? id, string? token, string returnUrl)
        {
            if (id == null || token == null)
            {
                return RedirectToAction(nameof(AccountController.Login), "Account", new { returnUrl = returnUrl});
            }
            var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

            IdentityResult result = await _userManager.ConfirmEmailAsync(await _userManager.FindByIdAsync(id), code);

            TempData["StatusMessage"] = result.Succeeded ? "Thank you for confirming your email." : "An error occurred while trying to confirm your email";

            return RedirectToAction("Login", "Account", new { returnUrl = returnUrl});
        }

            
    }
}
