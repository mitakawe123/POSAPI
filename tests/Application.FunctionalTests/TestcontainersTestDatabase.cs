using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using POSAPI.Infrastructure.Data;
using Respawn;
using Testcontainers.PostgreSql;

namespace POSAPI.Application.FunctionalTests;

public class TestcontainersTestDatabase : ITestDatabase
{

    private readonly PostgreSqlContainer _container = new PostgreSqlBuilder()
        .WithDatabase("POSDB")
        .WithUsername("pos_user")
        .WithPassword("pos_password")
        .WithCleanUp(true)
        .WithPortBinding(5432, true)
        .Build();

    private DbConnection _connection = null!;
    private string _connectionString = null!;
    private Respawner _respawner = null!;

    public async Task InitialiseAsync()
    {
        await _container.StartAsync();

        _connectionString = _container.GetConnectionString();

        _connection = new NpgsqlConnection(_connectionString);

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(_connectionString)
            .Options;

        var context = new ApplicationDbContext(options);

        await context.Database.MigrateAsync();
        //todo: here the respawner is giving error fix it 
        _respawner = await Respawner.CreateAsync(_connectionString, new RespawnerOptions
        {
            TablesToIgnore = ["__EFMigrationsHistory"]
        });
    }

    public DbConnection GetConnection()
    {
        return _connection;
    }

    public async Task ResetAsync()
    {
        await _respawner.ResetAsync(_connectionString);
    }

    public async Task DisposeAsync()
    {
        await _connection.DisposeAsync();
        await _container.DisposeAsync();
    }
}
