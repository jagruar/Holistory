using Holistory.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Holistory.Common.Services
{
    public class SqlServerConnectionProvider : IConnectionProvider
    {
        private readonly IConfiguration _configuration;

        public SqlServerConnectionProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("HolistoryDatabase"));
        }
    }
}
