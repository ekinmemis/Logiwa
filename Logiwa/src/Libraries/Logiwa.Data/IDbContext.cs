using Logiwa.Core;

using System.Collections.Generic;
using System.Data.Entity;

namespace Logiwa.Data
{
    public interface IDbContext
    {
        IDbSet<T> Set<T>() where T : BaseEntity;

        int SaveChanges();

        IList<T> ExecuteStoredProcedureList<T>(string commandText, params object[] parameters)
            where T : BaseEntity, new();

        IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters);

        int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters);

        void Detach(object entity);

        bool ProxyCreationEnabled { get; set; }

        bool AutoDetectChangesEnabled { get; set; }
    }
}
