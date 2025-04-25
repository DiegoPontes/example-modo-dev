using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace modelo_net_autenticacao.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AreaAberta()
        {
            return View();
        }

        [Authorize]
        public ActionResult AreaSegura()
        {
            return View();
        }
    }
}