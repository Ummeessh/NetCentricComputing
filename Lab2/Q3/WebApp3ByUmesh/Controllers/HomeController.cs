using Microsoft.AspNetCore.Mvc;
using WebApp3ByUmesh.Services;

namespace WebApp3ByUmesh.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITransientService _transient1;
        private readonly ITransientService _transient2;
        private readonly IScopedService _scoped1;
        private readonly IScopedService _scoped2;
        private readonly ISingletonService _singleton1;
        private readonly ISingletonService _singleton2;

        public HomeController(
            ITransientService transient1, ITransientService transient2,
            IScopedService scoped1, IScopedService scoped2,
            ISingletonService singleton1, ISingletonService singleton2)
        {
            _transient1 = transient1;
            _transient2 = transient2;
            _scoped1 = scoped1;
            _scoped2 = scoped2;
            _singleton1 = singleton1;
            _singleton2 = singleton2;
        }

        public IActionResult Index()
        {
            ViewBag.Transient1 = _transient1.GetOperationId();
            ViewBag.Transient2 = _transient2.GetOperationId();
            ViewBag.Scoped1 = _scoped1.GetOperationId();
            ViewBag.Scoped2 = _scoped2.GetOperationId();
            ViewBag.Singleton1 = _singleton1.GetOperationId();
            ViewBag.Singleton2 = _singleton2.GetOperationId();

            return View();
        }
    }
}