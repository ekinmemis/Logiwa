using System.Data.Entity.ModelConfiguration;

namespace Logiwa.Data.Mapping
{
    public abstract class LogiwaEntityTypeConfiguration<T> : EntityTypeConfiguration<T> where T : class
    {
        protected LogiwaEntityTypeConfiguration()
        {
            PostInitialize();
        }

        protected virtual void PostInitialize()
        {
        }
    }
}
