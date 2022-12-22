using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Common;
using Serilog;

namespace Data
{
    public partial class AppDbContext : DbContext
    {
        //private readonly ILogger _logger;
        public AppDbContext(DbContextOptions options ) : base(options)
        {
            Log.Information("#const AppDbContext-------------------------------");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.UseCollation("utf8mb4_bin").HasCharSet("utf8mb4");
            var ids = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            var implementedConfigTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => !t.IsAbstract
                    && !t.IsGenericTypeDefinition
                    && t.GetTypeInfo().ImplementedInterfaces.Any(i =>
                        i.GetTypeInfo().IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));
            foreach (var configType in implementedConfigTypes)
            {
                dynamic? config = Activator.CreateInstance(configType,new object[] { ids });
                builder.ApplyConfiguration(config);
            }
        }

    }
}