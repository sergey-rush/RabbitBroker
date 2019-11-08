using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RabbitBroker.Web.Code;
using RabbitMQ.Client;
using RabbitBroker.Web.Models;
using RabbitMQ.Client.Events;

namespace RabbitBroker.Web.Controllers
{
    public class HomeController : Controller
    {
        private IQueueService queueService;
        public HomeController(IQueueService queueService)
        {
            this.queueService = queueService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Producer()
        {
            DataModel dataModel = new DataModel();
            dataModel.SelectedMessage.Info = "Some important message";
            return View(dataModel);
        }

        [HttpPost]
        public async Task<ActionResult> Producer(DataModel dataModel)
        {
            queueService.SendMessage(dataModel.SelectedMessage);

            return View(dataModel);
        }

        public IActionResult Consumer()
        {
            DataModel dataModel = new DataModel();
            dataModel.Messages = queueService.GetMessages(100).ToList();
            return View(dataModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}