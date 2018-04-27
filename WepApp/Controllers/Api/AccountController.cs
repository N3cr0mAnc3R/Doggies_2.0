using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WepApp.Controllers.Abstract;
using WepApp.Models;

namespace WepApp.Controllers.Api
{
    [RoutePrefix("api/account")]
    public class AccountController : BaseApiController
    {
        //[NotRedirectWebApiAuthorize]
        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterModel model)
        {
            User user = new User() { UserName = model.Login, Email = model.Email };

            var result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await SignInAsync(user, false);
                return WrapSuccess(await CreateUserProfile(user));
            }
            else return WrapError(String.Join(". ", result.Errors));
        }
        public async Task SignInAsync(User user, bool isPersistent)
        {
            AuthManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthManager.SignIn(identity);
        }
        private async Task<UserProfile> CreateUserProfile(User user)
        {
            if (user != null)
            {
                UserProfile profile = new UserProfile(user);
                return profile;
            }

            return null;
        }

        [HttpPost]
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            AuthManager.SignOut();
            return WrapSuccess(null);
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("GetCurrentUser")]
        public async Task<IHttpActionResult> GetCurrentUser()
        {
            return WrapSuccess(await CreateUserProfile(await UserManager.FindByNameAsync(AuthManager.User.Identity.Name)));
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<IHttpActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return WrapError("Логин / пароль не введены.");
            }
            if (model.Login.Contains("@"))
            {
                User emailUser = await UserManager.FindByEmailAsync(model.Login);
                if (emailUser != null)
                {
                    model.Login = emailUser.UserName;
                }
            }
            User user = await UserManager.FindAsync(model.Login, model.Password);
            if (user == null)
            {
                return WrapError("Логин / пароль введены не верно.");
            }
            ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            AuthManager.SignOut();
            AuthManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, ident);
            return WrapSuccess(await CreateUserProfile(user));
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("external/vk")]
        public async Task<IHttpActionResult> Login(string access_token, int expires_in, int user_id, string email)
        {
            User user = null;
            if (email != "")
            {
                user = await UserManager.FindByEmailAsync(email);
                if (user == null)
                {
                    var result = UserManager.Create(new User() {Email = email, UserName = email });
                    if (result.Succeeded)
                    {
                        user = UserManager.FindByEmail(email);
                    }
                    else return WrapError(String.Join(" ", result.Errors));
                }
            }
            if(user == null)
            {
                return WrapError("Для корректной авторизации необходима электронная почта.");
            }
            ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            AuthManager.SignOut();
            AuthManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, ident);
            return WrapSuccess(await CreateUserProfile(user));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetVkInfo")]
        public async Task<IHttpActionResult> getExternalVK()
        {
            string[] scope = new string[] { "email" };
            string url = String.Format("https://oauth.vk.com/authorize?client_id={0}&redirect_uri={1}&display={2}&scope={3}&response_type=token&v={4}", ConfigurationManager.AppSettings["vk:clientId"], ConfigurationManager.AppSettings["vk:redirect_uri"], ConfigurationManager.AppSettings["vk:display"], String.Join(",", scope), ConfigurationManager.AppSettings["vk:version"]);
            return WrapSuccess(url);
        }
    }
}