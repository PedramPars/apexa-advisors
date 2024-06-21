using ApexaAdvisors.Domain.Contracts;
using ApexaAdvisors.Ef;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ApexaAdvisors.Pipelines;

public static class DatabasePipeline
{
    public static WebApplicationBuilder AddCustomEntityFrameworkCore(this WebApplicationBuilder builder)
    {
        var dbConn = new SqliteConnection("DataSource=:memory:");
        dbConn.Open();

        builder.Services.AddDbContext<AppDbContext>(dbBuilder => dbBuilder
            .UseLoggerFactory(LoggerFactory.Create(f => f.AddConsole()))
            .UseSqlite(dbConn));

        builder.Services.AddScoped<IUnitOfWork>(c => c.GetRequiredService<AppDbContext>());


        return builder;
    }
}
