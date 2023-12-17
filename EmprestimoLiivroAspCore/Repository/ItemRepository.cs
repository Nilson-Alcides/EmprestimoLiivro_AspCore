﻿using EmprestimoLiivroAspCore.Models;
using EmprestimoLiivroAspCore.Repository.Contrato;
using MySql.Data.MySqlClient;

namespace EmprestimoLiivroAspCore.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly string _conexaoMySQL;
        public ItemRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }
        public void Cadastrar(Item item)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("insert into itensEmp values(default, @codEmp, @codLivro)", conexao);

                cmd.Parameters.Add("@codEmp", MySqlDbType.VarChar).Value = item.codEmp;
                cmd.Parameters.Add("@codLivro", MySqlDbType.VarChar).Value = item.codLivro;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
        public void Atualizar(Item item)
        {
            throw new NotImplementedException();
        }

      

        public void Excluir(int Id)
        {
            throw new NotImplementedException();
        }

        public Item ObterItens(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> ObterTodosItens()
        {
            throw new NotImplementedException();
        }
    }
}
