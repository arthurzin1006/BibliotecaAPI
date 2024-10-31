using BibliotecaAPI.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace BibliotecaAPI.Repositories
{
    public class UsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        private IDbConnection Connection =>
            new MySqlConnection(_connectionString);

        // cadastrar um novo usuario
        public async Task<int> CadastrarUsuarioDB(Usuario usuario)
        {
            using (var conn = Connection)
            {
                var sqlCadastrarUsuario = "INSERT INTO Usuarios ( Id, Nome, Email) " +
                                        "VALUES (@Id, @Nome, @Email);" +
                                        "SELECT LAST_INSERT_ID();";

                return await conn.ExecuteScalarAsync<int>(sqlCadastrarUsuario, usuario);
            }
        }


        //buscar por nome
        public async Task<Usuario> BuscarPorNome(string nome)
        {
            var sql = "SELECT * FROM Usuarios WHERE Nome = @Nome";

            using (var conn = Connection)
            {
                return await conn.QueryFirstOrDefaultAsync<Usuario>(sql, new { Nome = nome });
            }
        }

        //buscar por email
        public async Task<Usuario> BuscarPorEmail(string email)
        {
            var sql = "SELECT * FROM Usuarios WHERE Email = @Email";

            using (var conn = Connection)
            {
                return await conn.QueryFirstOrDefaultAsync<Usuario>(sql, new {Email = email});
            }
        }

    }
}
