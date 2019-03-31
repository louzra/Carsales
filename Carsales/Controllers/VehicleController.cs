using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Carsales.Models;

namespace Carsales.Controllers
{
    public class VehicleController : Controller
    {
        // GET: Redirect to Home
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        // GET: Load the Create View
        public ActionResult Create()
        {
            if (TempData["VehicleType"] != null)
            {
                //Set the VehicleType that was selected from the dropdown
                string vehicleType = TempData["VehicleType"].ToString();

                //Set Partial View to render
                SetPartialView(vehicleType, "Create");
                //Set View header
                ViewBag.Header = "Create " + vehicleType;
                //Set Action button
                ViewBag.ActionButton = "Create";

                VehicleProperty vehicleProperty = new VehicleProperty();
                vehicleProperty.CommonProperty = new CommonProperty { VehicleType = vehicleType };

                return View(vehicleProperty);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Execute the CreateVehicle method to save properties in the database
        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")] VehicleProperty vehicleProperty)
        {
            CreateVehicle(vehicleProperty);

            return RedirectToAction("Index");
        }

        // GET: Edit Vehicle details
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = GetVehicle(id);
            string vehicleType = model.CommonProperty.VehicleType;

            SetPartialView(vehicleType, "Create");
            ViewBag.Header = "Edit " + vehicleType;
            ViewBag.ActionButton = "Update";

            return View("Create", model);
        }

        // GET: Show Vehicle Details
        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = GetVehicle(id);
            string vehicleType = model.CommonProperty.VehicleType;

            SetPartialView(vehicleType, "Details");
            ViewBag.Header = vehicleType + " Details";

            return View(model);
        }

        // GET: Delete
        [HttpGet]
        public ActionResult Delete(int id)
        {
            DataAccess dataAccess = new DataAccess();

            dataAccess.DeleteVehicle(id);

            return RedirectToAction("Index");
        }

        //Set the Partial Views to be used by the main Views
        private void SetPartialView(string vehicleType, string action)
        {
            ViewBag.CommonPartialView = string.Format("~/Views/Shared/Common/_{0}.cshtml", action);
            ViewBag.VehiclePartialView = string.Format("~/Views/Shared/{0}/_{1}.cshtml", vehicleType, action);
        }

        // Create Vehicle's properties and save to database
        private void CreateVehicle(VehicleProperty vehicleProperty)
        {
            DataAccess dataAccess = new DataAccess();

            dataAccess.InsertUpdateVehicle(vehicleProperty);

            switch (vehicleProperty.CommonProperty.VehicleType)
            {
                case "Car":
                    vehicleProperty.CarProperty.VehicleId = vehicleProperty.CommonProperty.Id;
                    dataAccess.InsertUpdateCar(vehicleProperty.CarProperty);
                    break;

                case "Boat":
                    vehicleProperty.BoatProperty.VehicleId = vehicleProperty.CommonProperty.Id;
                    dataAccess.InsertUpdateBoat(vehicleProperty.BoatProperty);
                    break;

                default:
                    break;
            }
        }

        // Get specific vehicle's property
        private VehicleProperty GetVehicle(int id)
        {
            DataAccess dataAccess = new DataAccess();
            VehicleProperty vehicleProperty = new VehicleProperty();

            vehicleProperty.CommonProperty = dataAccess.GetVehicleDetails(id);

            string vehicleType = vehicleProperty.CommonProperty.VehicleType;

            switch (vehicleType)
            {
                case "Car":
                    vehicleProperty.CarProperty = dataAccess.GetCarDetails(id);
                    break;

                case "Boat":
                    vehicleProperty.BoatProperty = dataAccess.GetBoatDetails(id);
                    break;
            }

            return vehicleProperty;
        }
    }
}
