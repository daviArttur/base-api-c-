using Testcontainers.MsSql;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testes.src.infra.config.db;

namespace Testes.test.integration.config
{

  [ExcludeFromCodeCoverage]
    internal class ApplicationSetup : WebApplicationFactory<Program>
    {
        private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder().WithEnvironment("ACCEPT_EULA", "Y").WithPassword("#dadospro3131").WithImage("mcr.microsoft.com/mssql/server:latest").Build();

        protected override IHost CreateHost(IHostBuilder builder)
        {
            //var root = new InMemoryDatabaseRoot();
            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<AppDbContext>)); // remove api db context
                services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(_msSqlContainer.GetConnectionString()));
                //options.UseInMemoryDatabase("db_test", root));
            });
            builder.UseContentRoot(Directory.GetCurrentDirectory());
            return base.CreateHost(builder);
        }

        public Task InitializeAsync()
        => _msSqlContainer.StartAsync();

        // public override Task DisposeAsync()
        // {
        //     return _msSqlContainer.StopAsync();
        // }
    }
}