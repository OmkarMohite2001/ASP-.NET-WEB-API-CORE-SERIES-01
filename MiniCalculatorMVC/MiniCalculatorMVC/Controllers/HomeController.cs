using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MiniCalculatorMVC.Models;

namespace MiniCalculatorMVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(CalculatorModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            switch (model.Operation)
            {
                case "+":
                    model.Result = model.FirstNumber + model.SecondNumber;
                    break;

                case "-":
                    model.Result = model.FirstNumber - model.SecondNumber;
                    break;

                case "*":
                    model.Result = model.FirstNumber * model.SecondNumber;
                    break;

                case "/":
                    if(model.SecondNumber == 0)
                    {
                        ModelState.AddModelError("Second Number", "Cannot Devided By Zero.");
                        return View(model);
                    }
                    model.Result = model.FirstNumber / model.SecondNumber;
                    break;

                default:
                    ModelState.AddModelError("Operation", "Invalid Operator.");
                    return View(model);
            }
            return View(model);
        }
    }
}
