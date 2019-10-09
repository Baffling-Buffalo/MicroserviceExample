using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebMVC.Models;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IOptions<AppSettings> appSettings;
        private readonly IIdentityParser<ApplicationUser> identityParser;

        public AccountController(IOptions<AppSettings> appSettings, IIdentityParser<ApplicationUser> identityParser)
        {
            this.appSettings = appSettings;
            this.identityParser = identityParser;
        }

        public IActionResult Logout()
        {
            return SignOut(CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme);
        }

        public IActionResult ChangePassword(string returnUrl)
        {
            return Redirect(appSettings.Value.IdentityUrl + $"/account/changepassword?userId={identityParser.Parse(User).Id}&returnUrl={returnUrl}");
        }

        //public IActionResult ResetPassword(string returnUrl)
        //{
        //    return Redirect(appSettings.Value.IdentityUrl + $"/account/resetpassword?userId={identityParser.Parse(User).Id}&returnUrl={returnUrl}");
        //}
    }
}