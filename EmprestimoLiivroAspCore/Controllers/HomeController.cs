using EmprestimoLiivroAspCore.CarrinhoCompra;
using EmprestimoLiivroAspCore.Models;
using EmprestimoLiivroAspCore.Repository;
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

        private IEmprestimoRepository _emprestimoRepository;
        private IItemRepository _itemRepository;
        public HomeController(ILogger<HomeController> logger, ILivroRepository livroRepository, CookieCarrinhoCompra cookieCarrinhoCompra, IEmprestimoRepository emprestimoRepository, IItemRepository itemRepository)
        {
            _logger = logger;
            _livroRepository = livroRepository;
            _cookieCarrinhoCompra = cookieCarrinhoCompra;
            _emprestimoRepository = emprestimoRepository;
            _itemRepository = itemRepository;
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

                return RedirectToAction(nameof(Carrinho));
            }

        }
        public IActionResult Carrinho()
        {
            return View(_cookieCarrinhoCompra.Consultar());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult RemoverItem(int id)
        {
            _cookieCarrinhoCompra.Remover(new Livro() { codLivro = id });
            return RedirectToAction(nameof(Carrinho));
        }

        DateTime data;
        public IActionResult SalvarCarrinho(Emprestimo emprestimo)
        {
            List<Livro> carrinho = _cookieCarrinhoCompra.Consultar();


            Emprestimo mdE = new Emprestimo();
            Item mdI = new Item();

            data = DateTime.Now.ToLocalTime();

            mdE.dtEmpre = data.ToString("dd/MM/yyyy");
            mdE.dtDev = data.AddDays(7).ToString();
            mdE.codUsu = "1";
            _emprestimoRepository.Cadastrar(mdE);

            _emprestimoRepository.buscaIdEmp(emprestimo);

            for (int i = 0; i < carrinho.Count; i++)
            {

                mdI.codEmp = Convert.ToInt32(emprestimo.codEmp);
                mdI.codLivro = Convert.ToString(carrinho[i].codLivro);

                _itemRepository.Cadastrar(mdI);
            }

            _cookieCarrinhoCompra.RemoverTodos();
            return RedirectToAction("confEmp");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}