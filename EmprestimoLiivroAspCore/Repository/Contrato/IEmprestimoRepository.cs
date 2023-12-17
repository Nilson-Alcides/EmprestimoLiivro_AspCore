using EmprestimoLiivroAspCore.Models;

namespace EmprestimoLiivroAspCore.Repository.Contrato
{
    public interface IEmprestimoRepository
    { 
        //CRUD
        IEnumerable<Emprestimo> ObterTodosEmprestimos();
        
        void Cadastrar(Emprestimo emprestimo);
       
        void Atualizar(Emprestimo emprestimo);
        
        Emprestimo ObterEmprestimos(int Id);
        
        void buscaIdEmp(Emprestimo emprestimo);
        
        void Excluir(int Id);
    }
}
