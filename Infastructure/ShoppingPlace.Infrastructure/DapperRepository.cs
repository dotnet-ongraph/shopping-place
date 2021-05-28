using Core.Interfaces;
using Core.Utilities;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class DapperRepository : IWriteEntityRepository, IReadEntityRepository
    {
        //private readonly IMapper _mapper;
        private string _connectionString;

        public DapperRepository()
        {

            //var resolver = new TableResolver();

            //SimpleCRUD.SetTableNameResolver(resolver);

            // _mapper = new MapperConfiguration(cfg => { }).CreateMapper();

        }

        #region Write Repository Implementation
        /// <summary>
        /// Inserts if id = 0, Updates if id > 0
        /// </summary>
        /// <param name="baseEntity"></param>
        /// <returns>
        /// id of entity if inserted successfully
        /// -1 if updated successfully
        /// null if failed
        /// </returns>

        /// <summary>
        /// Execute summary update and bulk inserts. Only for admin tasks
        /// </summary>
        /// <param name="sql">dapper supported sql</param>
        /// <param name="parameters">sql parameters</param>
        /// <returns></returns>
        public async Task<int> ExecuteAsync(string sql, object parameters = null)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                return await db.ExecuteAsync(sql, parameters);
            }
        }

        /// <summary>
        /// Execute summary update and bulk inserts. Only for admin tasks
        /// </summary>
        /// <param name="sql">dapper supported sql</param>
        /// <param name="parameters">sql parameters</param>
        /// <returns></returns>
        public int Execute(string sql, object parameters = null, IDbConnection connection = null, IDbTransaction transaction = null, CommandType commandType = CommandType.Text)
        {
            if (connection != null)
            {
                return connection.Execute(sql, parameters, transaction, null, commandType);
            }
            using (var _db = new SqlConnection(_connectionString))
            {

                return _db.Execute(sql, parameters, null, null, commandType);
            }
        }

        public IEnumerable<T> ExecuteProcedureWithResult<T>(string sql, object parameters = null)
        {
            using (var _db = new SqlConnection(_connectionString))
            {
                return _db.Query<T>(sql, parameters, null, true, null, CommandType.StoredProcedure);
            }
        }

        public IEnumerable<dynamic> ExecuteProcedureWithDynamicResult(string sql, object parameters = null)
        {
            using (var _db = new SqlConnection(_connectionString))
            {
                return _db.Query(sql, parameters, null, true, null, CommandType.StoredProcedure);
            }
        }

        #endregion

        #region Read Repository Implementation

        private DapperParameterizedModel GetFilteredSql(string sql, DynamicParameters parameters, Dictionary<string, string> filters)
        {
            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    if (!sql.Contains(" where "))
                        sql += $" where {filter.Key} = @{filter.Key}";
                    else
                        sql += $" and {filter.Key} = @{filter.Key}";
                    parameters.Add(filter.Key, filter.Value);
                }
            }


            return new DapperParameterizedModel { Sql = sql, Parameters = parameters };
        }

        private DapperParameterizedModel GetSortedFilteredSql(string sql, DynamicParameters parameters, Dictionary<string, string> filters, KeyValuePair<string, bool>? sortAscending)
        {
            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    if (!sql.Contains(" where "))
                        sql += $" where {filter.Key} = @{filter.Key}";
                    else
                        sql += $" and {filter.Key} = @{filter.Key}";
                    parameters.Add(filter.Key, filter.Value);
                }
            }

            if (sortAscending != null)
            {
                sql += $" order by {sortAscending.Value.Key} ";
                if (sortAscending.Value.Value == false)
                {
                    sql += "desc";
                }
            }

            return new DapperParameterizedModel { Sql = sql, Parameters = parameters };
        }

        public async Task<T> ExecuteScalarTextQuery<T>(string sql)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                await db.OpenAsync();
                var command = db.CreateCommand();
                command.CommandText = sql;
                command.CommandType = CommandType.Text;
                var output = (T)(await command.ExecuteScalarAsync());
                db.Close();
                return output;
            }
        }
        public async Task<IEnumerable<T>> ExecuteScalarStoreProcQueryAsync<T>(string sql, object param = null)
        {
            IEnumerable<T> list;
            using (var db = new SqlConnection(_connectionString))
            {
                list = await db.QueryAsync<T>(sql, param, commandType: CommandType.StoredProcedure);
            }
            return list;
        }


        public IEnumerable<T> GetUsingExistingConnection<T>(string sql, IDbConnection dbConnection, object param = null)
        {
            return dbConnection.Query<T>(sql, param);
        }
        public IEnumerable<T> Get<T>(string sql, object param = null, Dictionary<string, string> filters = null,
            KeyValuePair<string, bool>? sortAscending = null, Type[] types = null, Func<object[], T> mapFunction = null,
            Func<List<T>, List<T>> reduceFunction = null, int? pageSize = null, int? pageNumber = null)
        {
            DapperParameterizedModel dapperParameterizedModel;
            DynamicParameters parameters = new DynamicParameters(param);

            dapperParameterizedModel = GetSortedFilteredSql(sql, parameters, filters, sortAscending);

            dapperParameterizedModel = GetPaginatedSql(dapperParameterizedModel.Sql, dapperParameterizedModel.Parameters, pageSize, pageNumber);

            using (var db = new SqlConnection(_connectionString))
            {
                if (mapFunction == null)
                    return db.Query<T>(dapperParameterizedModel.Sql, dapperParameterizedModel.Parameters);
                else
                {
                    var list = db.Query<T>(dapperParameterizedModel.Sql, types, mapFunction, dapperParameterizedModel.Parameters).ToList();
                    if (reduceFunction != null)
                        list = reduceFunction(list);
                    return list;
                }
            }


        }



        private DapperParameterizedModel GetPaginatedSql(string sql, DynamicParameters parameters, int? pageSize, int? pageNumber)
        {
            if (pageSize.HasValue && pageNumber.HasValue)
            {
                sql += $" OFFSET @pageSize * (@pageNumber - 1) ROWS FETCH NEXT @pageSize ROWS ONLY";
                parameters.Add("pageSize", pageSize.Value);
                parameters.Add("pageNumber", pageNumber.Value);
            }
            return new DapperParameterizedModel { Sql = sql, Parameters = parameters };
        }

        public Dictionary<Type, List<object>> GetBatch(string sql, object param, Type[] typesToRead)
        {
            Dictionary<Type, List<object>> output = new Dictionary<Type, List<object>>();
            using (var db = new SqlConnection(_connectionString))
            {
                var data = db.QueryMultiple(sql, param);
                foreach (var type in typesToRead)
                {
                    output.Add(type, data.Read(type).AsList());
                }
            }

            return output;
        }

        public async Task<Dictionary<Type, List<object>>> GetBatchAsync(string sql, object param, Type[] typesToRead)
        {
            Dictionary<Type, List<object>> output = new Dictionary<Type, List<object>>();
            using (var db = new SqlConnection(_connectionString))
            {
                var data = await db.QueryMultipleAsync(sql, param);
                foreach (var type in typesToRead)
                {
                    output.Add(type, data.Read(type).AsList());
                }
            }
            return output;
        }


        public async Task<IEnumerable<T>> GetAsync<T>(string sql, object param = null, Dictionary<string, string> filters = null, KeyValuePair<string, bool>? sortAscending = null, Type[] types = null, Func<object[], T> mapFunction = null, Func<List<T>, List<T>> reduceFunction = null, int? pageSize = null, int? pageNumber = null)
        {
            DapperParameterizedModel dapperParameterizedModel;
            DynamicParameters parameters = new DynamicParameters(param);
            dapperParameterizedModel = GetSortedFilteredSql(sql, parameters, filters, sortAscending);

            dapperParameterizedModel = GetPaginatedSql(dapperParameterizedModel.Sql, dapperParameterizedModel.Parameters, pageSize, pageNumber);

            using (var db = new SqlConnection(_connectionString))
            {
                if (mapFunction == null)
                    return await db.QueryAsync<T>(dapperParameterizedModel.Sql, dapperParameterizedModel.Parameters);
                else
                {
                    var list = await db.QueryAsync<T>(dapperParameterizedModel.Sql, types, mapFunction, dapperParameterizedModel.Parameters);
                    if (reduceFunction != null)
                        list = reduceFunction(list.ToList());
                    return list;
                }
            }
        }

        public async Task<CollectionResult<T>> GetCollectionAsync<T>(string sql, string countsql, CollectionQueryParams queryParams, object param = null)
        {
            DapperParameterizedModel dapperParameterizedModel;
            DapperParameterizedModel dapperParameterizedCountModel;

            DynamicParameters parameters = new DynamicParameters(param);
            dapperParameterizedModel = GetSortedFilteredSql(sql, parameters, queryParams.Filters, queryParams.Sort);
            dapperParameterizedCountModel = GetFilteredSql(countsql, parameters, queryParams.Filters);


            dapperParameterizedModel = GetPaginatedSql(dapperParameterizedModel.Sql, dapperParameterizedModel.Parameters, queryParams.pageSize, queryParams.pageNumber);

            var finalSql = dapperParameterizedCountModel.Sql + Environment.NewLine + dapperParameterizedModel.Sql;

            CollectionResult<T> collectionResult = new CollectionResult<T>();
            using (var db = new SqlConnection(_connectionString))
            {
                using (var result = await db.QueryMultipleAsync(finalSql, dapperParameterizedModel.Parameters))
                {
                    collectionResult.Count = result.Read<int>().Single();
                    collectionResult.Result = result.Read<T>();
                }
            }
            return collectionResult;

        }
        #endregion

        public void Dispose()
        {

        }
        public void SetWriteConnectionString(ContextName name)
        {
            _connectionString = ConfigurationService.Configuration["ConnectionStrings:"+name.ToString()];
        }

        public void SetReadConnectionString(ContextName name)
        {
            _connectionString = ConfigurationService.Configuration["ConnectionStrings:"+name.ToString()];
        }

    }
    internal class DapperParameterizedModel
    {
        public string Sql { get; set; }
        public DynamicParameters Parameters { get; set; }
    }
}
