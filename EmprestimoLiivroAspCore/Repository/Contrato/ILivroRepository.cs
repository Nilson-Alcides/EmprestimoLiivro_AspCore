using EmprestimoLiivroAspCore.Models;

namespace EmprestimoLiivroAspCore.Repository.Contrato
{
    public interface ILivroRepository
    {
        //CRUD
        IEnumerable<Livro> ObterTodosLivros();
        void Cadastrar(Livro livro);
        void Atualizar(Livro livro);
        Livro ObterLivros(int Id);
        void Excluir(int Id);
    }
}
