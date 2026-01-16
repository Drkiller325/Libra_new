using Application.Poses.Queries;
using Application.Users.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Libra.Controllers
{
    [Authorize]
    public class PosController : Controller
    {
        private readonly IMediator _mediator;

        public PosController(IMediator mediator)
        {
            _mediator = mediator;
        }


        // GET: Pos
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetPos(CancellationToken cancellationToken)
        {

            try
            {
                var Poses = await _mediator.Send(new GetAllPosQuery() { });

                return Json(Poses, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot send request via mediator" + ex);
            }
            
        }
    }
}