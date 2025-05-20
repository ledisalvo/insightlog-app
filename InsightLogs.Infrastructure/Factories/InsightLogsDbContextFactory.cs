using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using InsightLogs.Infrastructure.Persistence;

namespace InsightLogs.Infrastructure.Factories;

public class InsightLogsDbContextFactory : IDesignTimeDbContextFactory<InsightLogsDbContext>
{
    public InsightLogsDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<InsightLogsDbContext>();

        // ⚠️ Cambiá esto por tu string real si usás otra base
        optionsBuilder.UseSqlite("Data Source=insightlogs.db");

        return new InsightLogsDbContext(optionsBuilder.Options);
    }
}
