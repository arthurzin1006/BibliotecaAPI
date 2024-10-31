using BibliotecaAPI.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace BibliotecaAPI.Repositories
{
    
        public class EmprestimoRepository
        {
            private readonly string _connectionString;

            public EmprestimoRepository(string connectionString)
            {
                _connectionString = connectionString;
            }
            private IDbConnection Connection =>
                new MySqlConnection(_connectionString);

        // Verificar se o livro esta disponivel
        
        public async Task<int> ValidaDisponibilidadeLivroDB(int Id)
        {
            using (var conn = Connection)
            {
                var sql = "SELECT COUNT(*) FROM Livros WHERE Disponivel = 1 AND Id = @Id";

                return await conn.QueryFirstOrDefaultAsync<int>(sql, new {Id});
            }
        }


        // Cadastrar um novo emprestimo
        public async Task<int> CadastrarEmprestimoDB(Emprestimo emprestimo)
            {
                using (var conn = Connection)
                {
                    var sqlCadastrarEmprestimo = "INSERT INTO Emprestimos ( Id, LivroId, UsuarioId, DataEmprestimo, DataDevolucao) " +
                                            "VALUES (@Id, @LivroId, @UsuarioId, @DataEmprestimo,@DataDevolucao);" +
                                            "SELECT LAST_INSERT_ID();";

                    return await conn.ExecuteScalarAsync<int>(sqlCadastrarEmprestimo, emprestimo);
                }
            }


        //historico
        public async Task<IEnumerable<Emprestimo>> ListarHistoricoEmprestimosDB()
        {
            using (var conn = Connection)
            {
                var sql = "SELECT * FROM Emprestimos emp WHERE emp.DataEmprestimo IS NOT NULL";

                return await conn.QueryAsync<Emprestimo>(sql);
            }
        }
       }
    }



