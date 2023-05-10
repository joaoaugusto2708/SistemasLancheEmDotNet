using Microsoft.AspNetCore.Mvc;
using SistemasLanche.Repositories;
using SistemasLanche.Repositories.Interfaces;
using SistemasLanche.ViewModels;

namespace SistemasLanche.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lancheRepository;

        public LancheController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        public IActionResult List()
        {
            //var lanches = _lancheRepository.Lanches; //Obtem uma lista de lanches
            //return View(lanches);
            var lancheListViewModel = new LancheListViewModel();//Instancia da ViewModel
            lancheListViewModel.Lanches = _lancheRepository.Lanches; //Obtem uma lista de lanches
            lancheListViewModel.CategoriaAtual = "Categoria Atual";

            return View(lancheListViewModel);
		}
    }
}
