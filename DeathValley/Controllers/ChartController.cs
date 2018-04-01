using DeathValley.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeathValley.Controllers
{
    public class ChartController : Controller
    {
        // GET: Chart
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Chart(Parameters parameters)
        {
            List<Point> getPoints = new List<Point>();
            Point point = new Point();

            if (ModelState.IsValid && ValidateParameters(parameters) == true)
            {
                for (int x = parameters.Range1; x < parameters.Range2; x += parameters.Step)
                {
                    point.PointY = parameters.ParameterA * Math.Pow(x, 2) + parameters.ParameterB * x + parameters.ParameterC;
                    getPoints.Add(new Point { PointX = x, PointY = point.PointY });
                }

                return Json(getPoints, JsonRequestBehavior.AllowGet);
            }

            return null;
        }

        public bool ValidateParameters(Parameters parameters)
        {
            if (parameters.ParameterA == 0)
            {
                return false;
            }
            if (parameters.Step <= 0)
            {
                return false;
            }
            if (parameters.Range1 >= parameters.Range2)
            {
                return false;
            }

            return true;
        }
    }
}