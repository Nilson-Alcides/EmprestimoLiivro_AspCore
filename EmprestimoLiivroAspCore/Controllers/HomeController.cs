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
        public HomeController(ILogger<HomeController> logger, ILivroRepository livroRepository)
        {
            _logger = logger;
            _livroRepository = livroRepository;
        }

        public IActionResult Index()
        {
            return View(_livroRepository.ObterTodosLivros()); 
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