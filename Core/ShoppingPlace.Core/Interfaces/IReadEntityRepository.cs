using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IReadEntityRepository
    {

        IEnumerable<T> Get<T>(string sql, object param = null, Dictionary<string, string> filters = null,
            KeyValuePair<string, bool>? sortAscending = null, Type[] types = null, Func<object[], T> mapFunction = null,
            Func<List<T>, List<T>> reduceFunction = null, int? pageSize = null, int? pageNumber = null);

        Task<IEnumerable<T>> GetAsync<T>(string sql, object param = null, Dictionary<string, string> filters = null,
            KeyValuePair<string, bool>? sortAscending = null, Type[] types = null, Func<object[], T> mapFunction = null,
            Func<List<T>, List<T>> reduceFunction = null, int? pageSize = null, int? pageNumber = null);

        Task<CollectionResult<T>> GetCollectionAsync<T>(string sql, string countsql, CollectionQueryParams queryParams, object param = null);

        Dictionary<Type, List<object>> GetBatch(string sql, object param, Type[] typesToRead);

        IEnumerable<T> GetUsingExistingConnection<T>(string sql, IDbConnection dbConnection, object param = null);

        Task<IEnumerable<T>> ExecuteScalarStoreProcQueryAsync<T>(string sql, object param = null);
        void SetReadConnectionString(ContextName name);
    }
    public class CollectionQueryParams
    {
        public Dictionary<string, string> Filters = new Dictionary<string, string>();
        public KeyValuePair<string, bool>? Sort = null;
        public int pageSize = 1000;
        public int pageNumber = 1;
    }

    public class CollectionResult<T>
    {
        public int Count = 0;
        public IEnumerable<T> Result;
    }
}
