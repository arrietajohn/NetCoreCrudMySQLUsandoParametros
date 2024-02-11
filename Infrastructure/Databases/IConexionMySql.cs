namespace App2_ConnBdMySQLParameter.Infrastructure.Databases;
public interface IConexionMySql
{

    public T ConsultarById<T>(object entity);
    public Task<T> ConsultarByIdAync<T>(object entity);
    public List<T> Listar<T>( );
    public Task<List<T>> ListarAsync<T>();
    public T Insertar<T>(object entity);
    public Task<T> InsertarAsync<T>(object entity);
    public T Actualizar<T>(T entity);
    public Task<T> ActualizarAsync<T>(T entity);
    public T Eliminar<T>(T entity);
    public Task<T> EliminarAsync<T>(T entity);
}