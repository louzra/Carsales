using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Carsales.Models;
using PagedList;

namespace Carsales.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int? page)
        {
            DataAccess dataAccess = new DataAccess();

            int pageNo = (page ?? 1);
            int pageSize = 10;

            VehicleProperty vehicleProperty = new VehicleProperty();
            vehicleProperty.VehicleType = dataAccess.GetVehicleType();
            vehicleProperty.ListCommonProperty = new PagedList<CommonProperty>(dataAccess.GetAllVehicleDetails().ToList(), pageNo, pageSize);

            return View(vehicleProperty);
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            string vehicleType = form.GetValues("VehicleType")[0].ToString();

            if (!string.IsNullOrEmpty(vehicleType))
            {
                TempData["VehicleType"] = vehicleType;
                return RedirectToAction("Create", "Vehicle");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}