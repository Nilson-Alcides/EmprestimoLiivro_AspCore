using EmprestimoLiivroAspCore.CarrinhoCompra;
using EmprestimoLiivroAspCore.Models;
using EmprestimoLiivroAspCore.Repository.Contrato;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmprestimoLiivroAspCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ILivroRepository _livroRepository;
        private CookieCarrinhoCompra _cookieCarrinhoCompra;
        public HomeController(ILogger<HomeController> logger, ILivroRepository livroRepository, CookieCarrinhoCompra cookieCarrinhoCompra)
        {
            _logger = logger;
            _livroRepository = livroRepository;
            _cookieCarrinhoCompra = cookieCarrinhoCompra;
        }

        public IActionResult Index()
        {
            return View(_livroRepository.ObterTodosLivros()); 
        }
        //Item ID = ID Produto
        public IActionResult AdicionarItem(int id)
        {
            Livro produto = _livroRepository.ObterLivros(id);

            if (produto == null)
            {
                return View("NaoExisteItem");
            }
            else
            {
                var item = new Livro()
                {
                    codLivro = id,
                    quantidade = 1,
                    imagemLivro = produto.imagemLivro,
                    nomeLivro = produto.nomeLivro
                };
                _cookieCarrinhoCompra.Cadastrar(item);

                return RedirectToAction(nameof(Index));
            }

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}