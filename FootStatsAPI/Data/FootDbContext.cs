using Microsoft.EntityFrameworkCore;

namespace FootStatsAPI.Data;

public class FootDbContext : DbContext
{
    public FootDbContext(DbContextOptions<FootDbContext> options) : base(options) { }
}
