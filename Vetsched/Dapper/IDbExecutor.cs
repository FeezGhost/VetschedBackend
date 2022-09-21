using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loader.infrastructure.Dapper
{
    public interface IDbExecutor : IDisposable
    {
        Task<int> ExecuteAsync(
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = default(int?),
            CommandType? commandType = default(CommandType?));

        Task<IEnumerable<dynamic>> QueryAsync(
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            bool buffered = true,
            int? commandTimeout = default(int?),
            CommandType? commandType = default(CommandType?));

        Task<IEnumerable<T>> QueryAsync<T>(
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            bool buffered = true,
            int? commandTimeout = default(int?),
            CommandType? commandType = default(CommandType?));
        Task<T> ExecuteScalarAsync<T>(
           string sql,
           object param = null,
           IDbTransaction transaction = null,
           bool buffered = true,
           int? commandTimeout = default(int?),
           CommandType? commandType = default(CommandType?));

        public Task<SqlMapper.GridReader> QueryMultipleAsync(
          string sql,
          object param = null,
          IDbTransaction transaction = null,
          int? commandTimeout = default(int?),
          CommandType? commandType = default(CommandType?));
    }
}
