using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IWriteEntityRepository
    {
        Task<int> ExecuteAsync(string sql, object parameters = null);

        int Execute(string sql, object parameters = null, IDbConnection connection = null, IDbTransaction transaction = null, CommandType commandType = CommandType.Text);

        IEnumerable<T> ExecuteProcedureWithResult<T>(string sql, object parameters = null);

        IEnumerable<dynamic> ExecuteProcedureWithDynamicResult(string sql, object parameters = null);
        void SetWriteConnectionString(ContextName name);
    }
}
