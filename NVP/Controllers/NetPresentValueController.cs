using NVP.Requests;
using NVP.Services.Interface;
using System;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;

namespace NVP.Controllers
{
    public class NetPresentValueController : Controller
    {
        private readonly INetPresentValueService netPresentValueService;

        public NetPresentValueController(INetPresentValueService netPresentValueService)
        {
            this.netPresentValueService = netPresentValueService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddToHistory(NetPresentValueRequest netPresentValueRequest)
        {
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    netPresentValueService.AddToHistory(netPresentValueRequest);
                    tran.Complete();
                }
            }
            catch (Exception ex) { return Json(false); }
            return Json(true, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public PartialViewResult ComputeNetPresentValue(NetPresentValueRequest netPresentValueRequest)
        {
            return PartialView(netPresentValueService.ComputeNetPresentValues(netPresentValueRequest).ToList());
        }

        [HttpGet]
        public PartialViewResult GetHistory()
        {
            return PartialView(netPresentValueService.GetNPVDTOHistory().OrderByDescending(x => x.createdDate).ToList());
        }
    }
}