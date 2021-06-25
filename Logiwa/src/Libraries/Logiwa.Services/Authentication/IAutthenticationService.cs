using Logiwa.Core.Domain.ApplicationUsers;

namespace Logiwa.Services.Authentication
{
    public interface IAuthenticationService
    {
        void SignIn(ApplicationUser applicationUser, bool rememberMe);

        void SignOut();

        ApplicationUser GetAuthenticatedApplicationUser();
    }
}
