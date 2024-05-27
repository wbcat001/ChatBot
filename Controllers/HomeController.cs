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

        private static List<Message> _messages = new List<Message>()
        {
            new Message(){Content="sample", Role=Role.User, CreatedAt=DateTime.Now}
        };
        //private static Message tmp;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(_messages);
        }


        [HttpPost]
        public async Task<IActionResult> Send([FromBody] string content)
        {
            
            Message tmp = new Message() { Content=content, Role=Role.User, CreatedAt=DateTime.Now};
            
            _messages.Add(tmp);

            // Previsous Message -> reply
            Task<string> task = GetReply(content);
            string reply = await task;
            Debug.WriteLine(reply);
            //Thread.Sleep(500);

            Message replyMessage = new Message()
            {
                Content = reply,
                Role = Role.Bot,
                CreatedAt = DateTime.Now
            };

            _messages.Add(replyMessage);


            // (Debug) Show content
            foreach (var m in _messages)
            {
                Debug.WriteLine(m.Content);
            }


            return PartialView("~/Views/partial/ChatHistory.cshtml", _messages);
        }

        static async Task<string> GetReply(string text)
        {
            //string? apiKey = GetEnvironmentVariable("OPEN_API_KEY");
            string? apiKey = null;
            apiKey = new ApiKey().Key;
            Debug.WriteLine(apiKey);
            string? result = "this is default reply.";
            if (apiKey != null)
            {
                OpenAIAPI api = new(apiKey);
                
                Conversation chat = api.Chat.CreateConversation();

                
                Prompt prompt = new Prompt()
                {
                    UserText = text
                };

                chat.AppendUserInput(prompt.FinalPrompt());
                result = await chat.GetResponseFromChatbotAsync();
                await Console.Out.WriteLineAsync((result.Trim()));
                

                
                /*
                chat.Model = Model.GPT4-Turbo
                chat.RequestParameters.Temprature=0;
                chat.AppendSystemMessage("");
                chat.Append


                # More simple
                var result = await api.Chat.CreateChatCompletionAsync("prompt");
                Console.WriteLine(result);   
                
                # prompt

                 */


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
