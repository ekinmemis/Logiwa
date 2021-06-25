using Logiwa.Data;
using Logiwa.Core.Domain.ApplicationUsers;

using System;
using System.Linq;
using Logiwa.Core;

namespace Logiwa.Services.ApplicationUserServices
{
    public class ApplicationUserService : IApplicationUserService
    {
        private IRepository<ApplicationUser> _applicationUserRepository;

        public ApplicationUserService(IRepository<ApplicationUser> applicationUserRepository)
        {
            _applicationUserRepository = applicationUserRepository;
        }

        public IPagedList<ApplicationUser> GetAllApplicationUsers(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _applicationUserRepository.Table.Where(f => f.Deleted != true);

            query = query.Skip(pageSize * pageIndex).Take(pageSize);

            query = query.OrderBy(o => o.Id);

            var applicaionUsers = new PagedList<ApplicationUser>(query, pageIndex, pageSize);

            return applicaionUsers;
        }

        public ApplicationUser GetApplicationUserById(int id)
        {
            if (id == 0)
                throw (new ArgumentNullException("parameter missing"));

            var applicationUser = _applicationUserRepository.GetById(id);

            return applicationUser;
        }

        public ApplicationUser GetApplicationUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw (new ArgumentNullException("parameter missing"));

            var applicationUser = _applicationUserRepository.Table.FirstOrDefault(f => f.Email == email);

            return applicationUser;
        }

        public void InsertApplicationUser(ApplicationUser applicationUser)
        {
            if (applicationUser == null)
                throw (new ArgumentNullException("parameter missing"));

            _applicationUserRepository.Insert(applicationUser);
        }

        public void UpdateApplicationUser(ApplicationUser applicationUser)
        {
            if (applicationUser == null)
                throw (new ArgumentNullException("parameter missing"));

            _applicationUserRepository.Update(applicationUser);
        }

        public void DeleteApplicationUser(ApplicationUser applicationUser)
        {
            if (applicationUser == null)
                throw (new ArgumentNullException("parameter missing"));

            _applicationUserRepository.Delete(applicationUser);
        }
    }
}
