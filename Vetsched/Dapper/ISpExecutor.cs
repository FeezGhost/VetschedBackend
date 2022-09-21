using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loader.infrastructure.Dapper
{
    public interface ISpExecutor
    {
        Task<IEnumerable<TResponseModel>> ExecuteStoreProcedure<TResponseModel>(string spName, object parameters);
        Task<T> ExecuteStoreProcedureforCreateUpdate<T>(string spName, object parameters);
    }
}
