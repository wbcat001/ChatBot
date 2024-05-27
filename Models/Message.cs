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

    public class Message
    {
        public required string Content{ get; set; }
        public required Role Role { get; set; }
        public required DateTime CreatedAt { get; set; }
    }

    public enum Role
    {
        User,
        Bot,

    }
}
