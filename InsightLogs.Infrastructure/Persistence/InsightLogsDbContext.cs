using InsightLogs.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InsightLogs.Infrastructure.Persistence;

public class InsightLogsDbContext : DbContext
{
    public InsightLogsDbContext(DbContextOptions<InsightLogsDbContext> options) : base(options) { }

    public DbSet<ErrorLog> ErrorLogs => Set<ErrorLog>();
}
