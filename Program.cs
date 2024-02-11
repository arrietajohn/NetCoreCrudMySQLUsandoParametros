
using App2_ConnBdMySQLParameter.Infrastructure.Databases;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using App2_ConnBdMySQLParameter.Tests;

var config = new ConfigurationBuilder()
.AddJsonFile("appsettings.json")
.AddEnvironmentVariables()
.Build();

var app = Host.CreateDefaultBuilder(args)
.ConfigureServices(service =>
{
    service.AddTransient<IConexionMySql, ConexionMySql>();
}
).Build();
TestConexion.PruebaInsertar(config);
TestConexion.PruebaInsersionAsync(config);
TestConexion.PruebaListar(config);
TestConexion.PruebaListarAsycn(config);
TestConexion.PruebaBuscar(config);



await app.StartAsync();