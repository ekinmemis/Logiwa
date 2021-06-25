using Logiwa.Core;
using Logiwa.Core.Domain.ApplicationUsers;

namespace Logiwa.Services.ApplicationUserServices
{
    public interface IApplicationUserService
    {
        IPagedList<ApplicationUser> GetAllApplicationUsers(int pageIndex = 0, int pageSize = int.MaxValue);

        ApplicationUser GetApplicationUserById(int id);

        ApplicationUser GetApplicationUserByEmail(string email);

        void InsertApplicationUser(ApplicationUser applicationUser);

        void UpdateApplicationUser(ApplicationUser applicationUser);

        void DeleteApplicationUser(ApplicationUser applicationUser);
    }
}
