using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Loader.infrastructure.Dapper
{
    public class SpExecutor : ISpExecutor
    {
        private readonly IDbExecutorFactory _dbExecutorFactory;
        public SpExecutor(IDbExecutorFactory dbExecutorFactory)
        {
            _dbExecutorFactory = dbExecutorFactory;
        }
        public async Task<IEnumerable<TResponseModel>> ExecuteStoreProcedure<TResponseModel>(string spName, object parameters)
        {
            using (var executor = _dbExecutorFactory.CreateConnection())
            {
                executor.Open();
                using var transaction = executor.BeginTransaction();
                string resultSetReferenceCommand = "fetch all in \"" + (await executor.ExecuteScalarAsync<string>(spName, parameters, commandType: CommandType.StoredProcedure)) + "\"";
                var response = (await executor.QueryAsync<TResponseModel>(resultSetReferenceCommand, null, commandType: CommandType.Text, transaction: transaction)).ToList();
                transaction.Commit();

                return response;
            }
        }

        public async Task<T> ExecuteStoreProcedureforCreateUpdate<T>(string spName, object parameters)
        {
            using (var executor = _dbExecutorFactory.CreateConnection())
            {
                executor.Open();
                var response =  await executor.ExecuteScalarAsync<T>(spName, parameters, commandType: CommandType.StoredProcedure);
                return response;
            }
        }
    }
}
