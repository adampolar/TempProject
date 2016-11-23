using BAURotaService;
using EmployeeService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace BAUWheelOfFortune.Controllers
{
    public class HomeController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllEmployees()
        {
            using (var employeeService = new Util.WebServiceProxy<IEmployeeService>("EmployeeService"))
            {
                return Json(employeeService.Service.GetAllEmployees(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetEmployeesForTodaysWheelOfFortune(bool morning)
        {
            using (var rotaService = new Util.WebServiceProxy<IBAURotaService>("BAURotaService"))
            {
                return Json(rotaService.Service.GetEligibleEmployees(morning), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult AssignWinnerForToday(int employeeNumber, bool morning)
        {
            using (var rotaService = new Util.WebServiceProxy<IBAURotaService>("BAURotaService"))
            {
                return Json(rotaService.Service.AssignEmployee(employeeNumber, morning));
            }
        }

        [HttpGet]
        public JsonResult GetRotaForToday()
        {
            using (var rotaService = new Util.WebServiceProxy<IBAURotaService>("BAURotaService"))
            {
                List<int?> rota = rotaService.Service.GetTodaysRota();
                return Json(new
                {
                    Morning = rotaService.Service.GetTodaysRota().First(),
                    Afternoon = rotaService.Service.GetTodaysRota().Last()
                },
                JsonRequestBehavior.AllowGet);
            }
        }
    }
}