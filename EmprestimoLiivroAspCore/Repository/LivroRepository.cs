using EmprestimoLiivroAspCore.Models;
using EmprestimoLiivroAspCore.Repository.Contrato;
using MySql.Data.MySqlClient;
using System.Data;

namespace EmprestimoLiivroAspCore.Repository
{
    public class LivroRepository : ILivroRepository
    {
        private readonly string _conexaoMySQL;

        public LivroRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }
        public void Cadastrar(Livro livro)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("insert into tbLivro values(default, @NomeLivro, @ImagemLivro)", conexao);

                cmd.Parameters.Add("@NomeLivro", MySqlDbType.VarChar).Value = livro.nomeLivro;
                cmd.Parameters.Add("@ImagemLivro", MySqlDbType.VarChar).Value = livro.imagemLivro;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
        public Livro ObterLivros(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbLivro where codLivro=@cod", conexao);
                cmd.Parameters.Add("@cod", MySqlDbType.VarChar).Value = Id;

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Livro livro = new Livro();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    livro.codLivro = Convert.ToInt32(dr["codLivro"]);
                    livro.nomeLivro = (String)(dr["nomeLivro"]);
                    livro.imagemLivro = (String)(dr["imagemLivro"]);
                }
                return livro;
            }
        }
        public IEnumerable<Livro> ObterTodosLivros()
        {
            List<Livro> Livrolist = new List<Livro>();
            
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbLivro", conexao);
                MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                sd.Fill(dt);
                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    Livrolist.Add(
                        new Livro
                        {
                            codLivro = Convert.ToInt32(dr["codLivro"]),
                            nomeLivro = (String)(dr["nomeLivro"]),
                            imagemLivro = (String)(dr["imagemLivro"]),
                        });
                }
                return Livrolist;
            }
        }
        public void Atualizar(Livro livro)
        {
            throw new NotImplementedException();
        }

        

        public void Excluir(int Id)
        {
            throw new NotImplementedException();
        }

        


        
    }
}
