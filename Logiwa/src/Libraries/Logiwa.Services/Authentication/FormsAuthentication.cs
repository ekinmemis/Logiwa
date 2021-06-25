using Logiwa.Core.Domain.ApplicationUsers;
using Logiwa.Data;

using System;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Logiwa.Services.Authentication
{
    public class FormsAuthenticationService : IAuthenticationService
    {
        private readonly IRepository<ApplicationUser> _applicationUserRepository;
        private readonly HttpContext _httpContext;
        private ApplicationUser _appUser;

        public FormsAuthenticationService(IRepository<ApplicationUser> applicationUserRepository, ApplicationUser appUser)
        {
            _applicationUserRepository = applicationUserRepository;
            _httpContext = HttpContext.Current;
            _appUser = appUser;
        }

        protected virtual ApplicationUser GetAuthenticatedApplicationUserFromTicket(FormsAuthenticationTicket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException("ticket");

            var email = ticket.UserData;

            if (String.IsNullOrWhiteSpace(email))
                return null;

            var applicationUser = _applicationUserRepository.Table.FirstOrDefault(f => f.Email == email);

            if (applicationUser == null)
                return null;

            return applicationUser;
        }

        public virtual void SignIn(ApplicationUser applicationUser, bool rememberMe)
        {
            var now = DateTime.UtcNow.ToLocalTime();

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1 /*version*/, applicationUser.Email, now, now.AddMinutes(15), rememberMe, applicationUser.Email, FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            cookie.HttpOnly = true;

            if (ticket.IsPersistent)
                cookie.Expires = ticket.Expiration;

            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Path = FormsAuthentication.FormsCookiePath;

            if (FormsAuthentication.CookieDomain != null)
                cookie.Domain = FormsAuthentication.CookieDomain;

            _httpContext.Response.Cookies.Add(cookie);
            _appUser = applicationUser;
        }

        public virtual void SignOut()
        {
            _appUser = null;
            FormsAuthentication.SignOut();
        }

        public virtual ApplicationUser GetAuthenticatedApplicationUser()
        {
            if (_appUser != null)
                return _appUser;

            if (_httpContext == null || _httpContext.Request == null || !_httpContext.Request.IsAuthenticated || !(_httpContext.User.Identity is FormsIdentity))
                return null;

            var formsIdentity = (FormsIdentity)_httpContext.User.Identity;

            var applicationUser = GetAuthenticatedApplicationUserFromTicket(formsIdentity.Ticket);

            if (applicationUser != null)
                _appUser = applicationUser;

            return _appUser;
        }
    }
}
