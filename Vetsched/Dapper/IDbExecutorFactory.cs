using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loader.infrastructure.Dapper
{
    public interface IDbExecutorFactory
    {
        IDbConnection CreateConnection();
    }
}
