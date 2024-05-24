using ChatBot.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChatBot.Controllers
{

    //[ApiController]
    //[Route("api/[controller")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private static List<ReceiveMessage> _messages = new List<ReceiveMessage>()
        {
            new ReceiveMessage(){Message="sample"}
        }
        ;
        private static ReceiveMessage tmp;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(_messages);
        }


        [HttpPost]
        public async Task<IActionResult> Send(string message)
        {
            Debug.WriteLine("post");
            ReceiveMessage msg = new ReceiveMessage { Message = message };
            _messages.Add(msg);
            _messages.Add(msg);

            ReplyMessage replyMessage = new ReplyMessage
            {
                Message = $"{msg.Message}",
                SendMessage = new string[] { msg.Message },
            };

            Debug.WriteLine(replyMessage.Message);
            //Thread.Sleep(500);
        

            return RedirectToAction("Index");
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
