using eStore.BL.BO;
using eStore.DBO;
using eStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eStore.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        // GET: /Checkout/AddressAndPayment
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection values)
        {
            return View();
        }

    }
}