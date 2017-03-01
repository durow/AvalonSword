using System.Data;

namespace Ayx.AvalonSword.Data
{
    public interface IDbConGenerator
    {
        IDbConnection DbConnection { get; }
    }
}
