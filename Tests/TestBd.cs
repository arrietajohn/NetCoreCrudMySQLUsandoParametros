using App2_ConnBdMySQLParameter.Infrastructure.Databases;
using Microsoft.Extensions.Configuration;
using System.Data.Common;

namespace App2_ConnBdMySQLParameter.Tests;

public class TestConexion
{
    public static void PruebaListar(IConfiguration conf)
    {
        var conBd = new ConexionMySql(conf);
        List<Usuario> usuarios = conBd.Listar<Usuario>();
        foreach (Usuario u in usuarios)
        {
            System.Console.WriteLine("----- SINCRONO -----------");
            System.Console.WriteLine($"USUARIO #{(usuarios.IndexOf(u) + 1)}");
            imprimir(u);
        }
    }

    public static async void PruebaListarAsycn(IConfiguration conf)
    {
        var conBd = new ConexionMySql(conf);
        List<Usuario> usuarios = await conBd.ListarAsync<Usuario>();
        foreach (Usuario u in usuarios)
        {
            System.Console.WriteLine("----- ASINCRONO -----------");
            System.Console.WriteLine($"USUARIO #{(usuarios.IndexOf(u) + 1)}");
            imprimir(u);
        }
    }


    public static async void PruebaBuscar(IConfiguration conf)
    {
        var conBd = new ConexionMySql(conf);
        int intId = 123;
        Usuario u =  conBd.ConsultarById<Usuario>(intId);
           System.Console.WriteLine("----- BUSCAR POR CEDULA -----------");
         imprimir( u);
     
    }



    public static  async void PruebaInsersionAsync(IConfiguration conf)
    {
        var conBd = new ConexionMySql(conf);
        var u = new Usuario{
            Id= 456
            ,Clave = "***"
            ,Nombre = "Fulanito 3 de tal"
            ,Email = "fulano3@gmail.com"
        };

        var res =  await conBd.InsertarAsync<int>(u);
        System.Console.WriteLine($"Total insertados {res}");
     
    }

    public static  void PruebaInsertar(IConfiguration conf)
    {
        var conBd = new ConexionMySql(conf);
        var u = new Usuario{
            Id= 321
            ,Clave = "***"
            ,Nombre = "Fulanito 2 de tal"
            ,Email = "fulano@gmail.com"
        };

        var res = conBd.Insertar<int>(u);
        System.Console.WriteLine($"Total insertados {res}");
     
    }


    public static void imprimir(Usuario u)
    {

        System.Console.WriteLine($"ID: {Convert.ToString(u.Id)}");
        System.Console.WriteLine($"CLAVE: {Convert.ToString(u.Clave)}");
        System.Console.WriteLine($"NOMBRE: {Convert.ToString(u.Nombre)}");
        System.Console.WriteLine($"EMAIL: {Convert.ToString(u.Email)}");
    }


}
