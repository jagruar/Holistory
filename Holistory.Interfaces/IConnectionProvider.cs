using System.Data;

namespace Holistory.Interfaces
{
    public interface IConnectionProvider
    {
        IDbConnection GetConnection();
    }
}
