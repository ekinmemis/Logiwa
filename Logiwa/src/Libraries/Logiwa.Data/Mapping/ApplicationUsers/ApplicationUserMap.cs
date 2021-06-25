using Logiwa.Core.Domain.ApplicationUsers;

namespace Logiwa.Data.Mapping.ApplicationUsers
{
    public class ApplicationUserMap : LogiwaEntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserMap()
        {
            this.ToTable(nameof(ApplicationUser));
        }
    }
}
