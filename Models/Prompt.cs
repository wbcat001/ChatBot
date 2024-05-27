namespace ChatBot.Models
{
    public class Prompt
    {
        public string UserText { get; set; }


        public string FinalPrompt()
        {
            var finalPrompt = $"""
                Q please answer by json format.
                ---
                Example:
                User: hello
                AI: answer: hello. can I help you?
                ---
                
                User: {UserText}
                A:
                """;
            return finalPrompt;
        }

        // make final prompt
    }
}
