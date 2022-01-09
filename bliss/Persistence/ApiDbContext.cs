using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class ApiDbContext : SqlDbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
        }
    }
}
