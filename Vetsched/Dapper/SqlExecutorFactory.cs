using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Loader.infrastructure.Dapper
{
    public class SqlExecutorFactory : IDbExecutorFactory
    {
        private readonly IConfiguration _configuration;
        public SqlExecutorFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection CreateConnection()
            => new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
    }

    
}
