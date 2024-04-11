using EmprestimoLiivroAspCore.GerenciaArquivos;
using EmprestimoLiivroAspCore.Models;
using EmprestimoLiivroAspCore.Repository.Contrato;
using Microsoft.AspNetCore.Mvc;

namespace EmprestimoLiivroAspCore.Controllers
{
    public class LivrosController : Controller
    {
        private ILivroRepository _livroRepository;
        public LivrosController(ILivroRepository livroRepository)
        {
            
            _livroRepository = livroRepository;
        }
        //Cadastra Livro Get
        public IActionResult Index()
        {
            return View();
        }
        //Cadastra Livro Post
        [HttpPost]
        public IActionResult Index(Livro livro, IFormFile file)
        {
            var Caminho = GerenciadorArquivo.CadastrarImagemProduto(file);

            livro.imagemLivro = Caminho;
            _livroRepository.Cadastrar(livro);

            ViewBag.msg = "Cadastro realizado com sucesso";

            return View();
        }

        public IActionResult CadLivro()
        {
            return View();
        }
        //Cadastra Livro Post
        [HttpPost]
        public IActionResult CadLivro(Livro livro, IFormFile file)
        {
            var Caminho = GerenciadorArquivo.CadastrarImagemProduto(file);

            livro.imagemLivro = Caminho;
            _livroRepository.Cadastrar(livro);

            ViewBag.msg = "Cadastro realizado com sucesso";

            return View();
        }

    }
}
