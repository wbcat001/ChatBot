using ChatBot.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static System.Environment;
using OpenAI_API;
using OpenAI_API.Chat;
namespace ChatBot.Controllers
{

    //[ApiController]
    //[Route("api/[controller")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        static string? _apiKey = "";

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
        public async Task<IActionResult> Send([FromBody] ReceiveMessage receiveMessage)
        {
            
            
            
            _messages.Add(receiveMessage);

            ReplyMessage replyMessage = new ReplyMessage
            {
                Message = $"{receiveMessage.Message}",
                SendMessage = new string[] { receiveMessage.Message },
            };

            Task<string> task = GetReply(receiveMessage.Message);
            string reply = await task;
            Debug.WriteLine(reply);
            //Thread.Sleep(500);


            foreach (var m in _messages)
            {
                Debug.WriteLine(m.Message);
            }


            return PartialView("~/Views/partial/ChatHistory.cshtml", _messages);
        }

        static async Task<string> GetReply(string text)
        {
            //string? apiKey = GetEnvironmentVariable("OPEN_API_KEY");
            string? apiKey = null;
            Debug.WriteLine(apiKey);
            string? result = "this is default reply.";
            if (apiKey != null)
            {
                OpenAIAPI api = new(apiKey);

                Conversation chat = api.Chat.CreateConversation();

                string? prompt = text;

                chat.AppendUserInput(prompt);
                result = await chat.GetResponseFromChatbotAsync();
                await Console.Out.WriteLineAsync((result.Trim()));


            }
          


            return result;
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
