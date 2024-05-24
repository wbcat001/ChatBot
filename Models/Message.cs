namespace ChatBot.Models
{
    public class ReceiveMessage
    {

        public required string Message { get; set; }
    }

    public class ReplyMessage
    {
        public required string Message { get; set; } 
        public required string[] SendMessage { get; set; }
    }
}
