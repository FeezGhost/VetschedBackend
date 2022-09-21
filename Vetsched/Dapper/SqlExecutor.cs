using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Loader.infrastructure.Dapper
{
    public class SqlExecutor : IDbExecutor
    {
        readonly IDbConnection _dbConnection;

        public SqlExecutor(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        // for return none insert, update, delete
        public async Task<int> ExecuteAsync(
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = default(int?),
            CommandType? commandType = default(CommandType?))
        {
            return await _dbConnection.ExecuteAsync(
                sql,
                param,
                transaction,
                commandTimeout,
                commandType);
        }

        public async Task<IEnumerable<dynamic>> QueryAsync(
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            bool buffered = true,
            int? commandTimeout = default(int?),
            CommandType? commandType = default(CommandType?))
        {
            return await _dbConnection.QueryAsync(
                sql,
                param,
                transaction,
                // buffered,
                commandTimeout,
                commandType);
        }
        // single or multiple record
        public async Task<IEnumerable<T>> QueryAsync<T>(
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            bool buffered = true,
            int? commandTimeout = default(int?),
            CommandType? commandType = default(CommandType?))
        {
            return await _dbConnection.QueryAsync<T>(
                sql,
                param,
                transaction,
                // buffered,
                commandTimeout,
                commandType);
        }
        // Return Multiple Lists
        public async Task<SqlMapper.GridReader> QueryMultipleAsync(
           string sql,
           object param = null,
           IDbTransaction transaction = null,
           int? commandTimeout = default(int?),
           CommandType? commandType = default(CommandType?))
        {
            return await _dbConnection.QueryMultipleAsync(
                sql,
                param,
                transaction,
                commandTimeout,
                commandType);
        }
        // for return single value like bool,string and int
        public async Task<T> ExecuteScalarAsync<T>(
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            bool buffered = true,
            int? commandTimeout = default(int?),
            CommandType? commandType = default(CommandType?))
        {
            return await _dbConnection.ExecuteScalarAsync<T>(
                sql,
                param,
                transaction,
                // buffered,
                commandTimeout,
                commandType);
        }

        public void Dispose()
        {
            _dbConnection.Dispose();
        }
    }
}
