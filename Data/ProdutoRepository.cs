using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace EcommerceAPI.Data
{
    public class ProdutoRepository
    {
        private readonly string _connectionString;

        public ProdutoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("EstoqueConnection");
        }

        public async Task<IEnumerable<Produto>> GetAllProdutos()
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = sql.CreateCommand())
                    {
                        cmd.CommandText = $@"
                        SELECT
                            id_produto,
                            nm_produto,
                            cod_codigo,
                            tp_categoria,
                            txt_descricao,
                            vl_qtdEstoque,
                            vl_preco
                        FROM
                            TB_ECOMMERCE_PRODUTO
                        ";

                        cmd.CommandType = System.Data.CommandType.Text;
                        var produtoList = new List<Produto>();
                        await sql.OpenAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                produtoList.Add(
                                    new Produto()
                                    {
                                        Id = (int)reader["id_produto"],
                                        Nome = (string)reader["nm_produto"],
                                        Codigo = (string)reader["cod_codigo"],
                                        Categoria = (string)reader["tp_categoria"],
                                        Descricao = (string)reader["txt_descricao"],
                                        QtdEstoque = (int)reader["vl_qtdEstoque"],
                                        Valor = (decimal)reader["vl_preco"]
                                    }
                                );
                            }
                        }
                        return produtoList;
                    }
                }
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }

        public async Task<Produto> GetProdutoById(int Id)
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = sql.CreateCommand())
                    {
                        cmd.CommandText = $@"
                        SELECT
                            id_produto,
                            nm_produto,
                            cod_codigo,
                            tp_categoria,
                            txt_descricao,
                            vl_qtdEstoque,
                            vl_preco
                        FROM
                            TB_ECOMMERCE_PRODUTO
                        where
                            id_produto = @Id
                        ";

                        cmd.Parameters.AddWithValue("@Id", Id);

                        cmd.CommandType = System.Data.CommandType.Text;

                        Produto produto = null;
                        
                        await sql.OpenAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                produto = new Produto() {
                                    Id = (int)reader["id_produto"],
                                    Nome = (string)reader["nm_produto"],
                                    Codigo = (string)reader["cod_codigo"],
                                    Categoria = (string)reader["tp_categoria"],
                                    Descricao = (string)reader["txt_descricao"],
                                    QtdEstoque = (int)reader["vl_qtdEstoque"],
                                    Valor = (decimal)reader["vl_preco"]
                                };
                            }
                        }
                        return produto;
                    }
                }
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }

    }
}