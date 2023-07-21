using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SistemasLanche.Areas.Admin.Controllers
{
    
    [Area("Admin")]
    [Authorize(Roles = "Admin")] //Defini que apenas usuários do perfil de admin pode acessar esta pagina
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
