using BibliotecaAPI.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace BibliotecaAPI.Repositories
{
    public class LivroRepository
    {
        private readonly string _connectionString;

        public LivroRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        private IDbConnection Connection =>
            new MySqlConnection(_connectionString);

        // listar os livros
        public async Task<IEnumerable<Livro>> ListarLivrosDB()
        {
            using (var conn = Connection)
            {
                var sql = "SELECT * FROM Livros";
                return await conn.QueryAsync<Livro>(sql);
            }
        }

        //buscar por genero
        public async Task<Livro> BuscarPorGenero(string genero)
        {
            var sql = "SELECT * FROM Livros WHERE Genero = @Genero";

            using (var conn = Connection)
            {
                return await conn.QueryFirstOrDefaultAsync<Livro>(sql, new { Genero = genero });
            }
        }

        //buscar por autor
        public async Task<Livro> BuscarPorAutor(string autor)
        {
            var sql = "SELECT * FROM Livros WHERE Autor = @Autor";

            using (var conn = Connection)
            {
                return await conn.QueryFirstOrDefaultAsync<Livro>(sql, new { Autor = autor });
            }
        }

        //buscar por ano de publicacao
        public async Task<Livro> BuscarPorAno(int ano)
        {
            var sql = "SELECT * FROM Livros WHERE AnoPublicacao = @AnoPublicacao";

            using (var conn = Connection)
            {
                return await conn.QueryFirstOrDefaultAsync<Livro>(sql, new { AnoPublicacao = ano });
            }
        }

        public async Task<int> CadastrarLivroDB(Livro livro)
        {
            using (var conn = Connection)
            {
                var sqlCadastrarLivro = "INSERT INTO Livros ( Id, Titulo, Autor, AnoPublicacao, Genero, Disponivel) " +
                                        "VALUES (@Id, @Titulo, @Autor, @AnoPublicacao, @Genero, @Disponivel);" +
                                        "SELECT LAST_INSERT_ID();";

                return await conn.ExecuteScalarAsync<int>(sqlCadastrarLivro, livro);
            }
        }

        // atualizar informações de um livro
        public async Task<int> AtualizarLivroDB(int id, Livro livroAtualizado)
        {
            using (var conn = Connection)
            {
                var sqlAtualizarLivro = "UPDATE Livros SET Id = @Id,Titulo = @Titulo, Autor = @Autor, AnoPublicacao = @AnoPublicacao ,Genero = @Genero, Disponivel = @Disponivel " +
                                        "WHERE Id = @Id";

                return await conn.ExecuteAsync(sqlAtualizarLivro, new { Id = id, livroAtualizado.Titulo, livroAtualizado.Autor, livroAtualizado.AnoPublicacao, livroAtualizado.Genero, livroAtualizado.Disponivel });
            }
        }

        // excluir um livro
        public async Task<bool> ExcluirLivroDB(int id)
        {
            using (var conn = Connection)
            {
                var sqlExcluirLivro = "DELETE FROM Livros WHERE Id = @Id";
                var rowsAffected = await conn.ExecuteAsync(sqlExcluirLivro, new { Id = id });
                return rowsAffected > 0;
            }
        }

        
    }
}
