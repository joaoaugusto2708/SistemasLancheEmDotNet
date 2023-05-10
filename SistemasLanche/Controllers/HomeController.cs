using Microsoft.AspNetCore.Mvc;
using SistemasLanche.Models;
using SistemasLanche.Repositories.Interfaces;
using SistemasLanche.ViewModels;
using System.Diagnostics;

namespace SistemasLanche.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILancheRepository _lancheRepository;

		public HomeController(ILancheRepository lancheRepository)
		{
			_lancheRepository = lancheRepository;
		}

		public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                LanchesPreferidos = _lancheRepository.LanchesPreferiodos
            };

			return View(homeViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}