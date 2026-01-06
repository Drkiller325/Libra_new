using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Libra.Controllers
{
    [Authorize]
    public class PosController : Controller
    {
        // GET: Pos
        public ActionResult Index()
        {
            return View();
        }
    }
}