using Microsoft.EntityFrameworkCore;
using ProvaPub.Repository;

namespace ProvaPub.Test
{
    public class TestDatabaseFixture
    {
        private const string ConnectionString = @"Data Source=INSPIRON5458;Initial Catalog=ProvaBonifq;Persist Security Info=True;User ID=sa;Password=88092229;TrustServerCertificate=True";
        public static TestDbContext CreateContext()
          => new(
              new DbContextOptionsBuilder<TestDbContext>()
                  .UseSqlServer(ConnectionString)
                  .Options);
    }
}
