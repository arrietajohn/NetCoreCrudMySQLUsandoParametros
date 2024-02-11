
using System.Data.Common;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace App2_ConnBdMySQLParameter.Infrastructure.Databases;

public class ConexionMySql : IConexionMySql
{
    private IConfiguration _configuracion;

    private DbConnection _conexion;

    public ConexionMySql(IConfiguration configuration)
    {
        _configuracion = configuration;
    }

    public void Conectar()
    {
        string cadenaConBd = _configuracion.GetConnectionString("ConexionMySQL");
        if (cadenaConBd == null || cadenaConBd.Trim().Count() == 0)
        {
            throw new Exception("Parametros de conexion incorrectos");
        }
        try
        {
            _conexion = new MySqlConnection(cadenaConBd);
            _conexion.Open();
        }
        catch (System.Exception er)
        {
            System.Console.WriteLine("ERROR DE CONEXION: - " + er.Message);
            throw new Exception("ERROR DE CONEXION");
        }
    }

    public T Actualizar<T>(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<T> ActualizarAsync<T>(T entity)
    {
        throw new NotImplementedException();
    }

    public T ConsultarById<T>(object entity)
    {
        try
        {
            int id = (int)Convert.ChangeType(entity, typeof(int));
            Conectar();
            string sql = "SELECT * FROM Usuarios Where id = ?id";
            var sqlCommnad = new MySqlCommand();
            sqlCommnad.Connection = (MySqlConnection)_conexion;
            sqlCommnad.CommandText = sql;
            sqlCommnad.Parameters.Add("?id", MySqlDbType.Int16).Value = id;
            var resultado = sqlCommnad.ExecuteReader();
            Usuario u = null;
            if (!resultado.Read())
            {
                throw new Exception("El Usuario no existe");
            }
            u = new Usuario
            {
                Id = resultado.GetInt32("Id")
                ,
                Clave = resultado["clave"].ToString()
                ,
                Nombre = (string)resultado["nombre"]
                ,
                Email = resultado.GetString("email")
            };
            return (T)Convert.ChangeType(u, typeof(T));
        }
        catch (System.Exception er)
        {
            System.Console.WriteLine("ERRRO: - Consultar Usuario");
            throw new Exception("ERROR: " + er.Message); ;
        }
    }

    public Task<T> ConsultarByIdAync<T>(object entity)
    {
        throw new NotImplementedException();
    }

    public T Eliminar<T>(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<T> EliminarAsync<T>(T entity)
    {
        throw new NotImplementedException();
    }

    public T Insertar<T>(object entity)
    {
        try
        {
            Conectar();
            Usuario u = entity as Usuario;
            string sql = "INSERT INTO Usuarios VALUES(?id, ?pass, ?nombre, ?email)";
            var sqlCommnad = new MySqlCommand();
            sqlCommnad.CommandText = sql;
            sqlCommnad.Connection = (MySqlConnection)_conexion;
            sqlCommnad.Parameters.Add("?id", MySqlDbType.VarChar).Value = u.Id;
            sqlCommnad.Parameters.Add("?pass", MySqlDbType.VarChar).Value = u.Clave;
            sqlCommnad.Parameters.Add("?nombre", MySqlDbType.VarChar).Value = u.Nombre;
            sqlCommnad.Parameters.Add("?email", MySqlDbType.VarChar).Value = u.Email;
            int total = sqlCommnad.ExecuteNonQuery();
            _conexion.Close();
            return (T)Convert.ChangeType(total, typeof(T));
        }
        catch (System.Exception er)
        {
            System.Console.WriteLine("Error al insertar en la BD");
            throw new Exception("ERROR: " + er.Message);
        }
    }

    public async Task<T> InsertarAsync<T>(object entity)
    {
        try
        {
            Conectar();
            Usuario u = entity as Usuario;
            string sql = "INSERT INTO Usuarios VALUES(?id, ?pass, ?nombre, ?email)";
            var sqlCommnad = new MySqlCommand();
            sqlCommnad.CommandText = sql;
            sqlCommnad.Connection = (MySqlConnection)_conexion;
            sqlCommnad.Parameters.Add("?id", MySqlDbType.VarChar).Value = u.Id;
            sqlCommnad.Parameters.Add("?pass", MySqlDbType.VarChar).Value = u.Clave;
            sqlCommnad.Parameters.Add("?nombre", MySqlDbType.VarChar).Value = u.Nombre;
            sqlCommnad.Parameters.Add("?email", MySqlDbType.VarChar).Value = u.Email;
            int total = await sqlCommnad.ExecuteNonQueryAsync();
            _conexion.Close();
            return (T) Convert.ChangeType(total, typeof(T));
        }
        catch (System.Exception er)
        {
            System.Console.WriteLine("Error al insertar en la BD");
            throw new Exception("ERROR: " + er.Message);
        }
    }

    public List<T> Listar<T>()
    {
        try
        {
            Conectar();
            var sqlCommnad = new MySqlCommand();
            sqlCommnad.CommandText = "Select * FROM Usuarios";
            sqlCommnad.Connection = (MySqlConnection)_conexion;
            var resultado = sqlCommnad.ExecuteReader();
            List<Usuario> usuarios = new List<Usuario>();
            while (resultado.Read())
            {
                var u = new Usuario();
                u.Id = Convert.ToInt32(resultado["id"]);
                u.Clave = (string)resultado["clave"];
                u.Nombre = (string)resultado["nombre"];
                u.Email = (string)resultado["email"];
                usuarios.Add(u);
            }
            _conexion.Close();
            return (List<T>)Convert.ChangeType(usuarios, typeof(List<T>));

        }
        catch (System.Exception er)
        {
            System.Console.WriteLine("Error al Listar desde la BD");
            throw new Exception("ERROR: " + er.Message);
        }
    }

    public async Task<List<T>> ListarAsync<T>()
    {
        try
        {
            Conectar();
            var sqlCommnad = new MySqlCommand();
            sqlCommnad.CommandText = "Select * FROM Usuarios";
            sqlCommnad.Connection = (MySqlConnection)_conexion;
            var resultado = await sqlCommnad.ExecuteReaderAsync();
            List<Usuario> usuarios = new List<Usuario>();
            while (resultado.Read())
            {
                var u = new Usuario();
                u.Id = Convert.ToInt32(resultado["id"]);
                u.Clave = (string)resultado["clave"];
                u.Nombre = (string)resultado["nombre"];
                u.Email = (string)resultado["email"];
                usuarios.Add(u);
            }
            _conexion.Close();
            return (List<T>)Convert.ChangeType(usuarios, typeof(List<T>));

        }
        catch (System.Exception er)
        {
            System.Console.WriteLine("Error al Listar desde la BD");
            throw new Exception("ERROR: " + er.Message);
        }
    }
}