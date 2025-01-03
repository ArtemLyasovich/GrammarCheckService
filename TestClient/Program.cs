using Grpc.Net.Client;
using GrammarCheckService;

namespace TestClient;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("gRPC Client is starting...");

        var httpHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };

        var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions
        {
            HttpHandler = httpHandler
        });

        var client = new GrammarCheck.GrammarCheckClient(channel);

        try
        {
            var textRequest = new TextRequest { Text = "Тствовый прмер.", Language = "ru" };
            var spellingResponse = await client.CheckSpellingAsync(textRequest);
            Console.WriteLine("Spelling check completed!");
            Console.WriteLine("Words with errors and suggestions:");

            foreach (var error in spellingResponse.Errors)
            {
                Console.WriteLine($"Word: {error.Word}");
                Console.WriteLine("Suggestions:");
                foreach (var suggestion in error.Suggestions)
                {
                    Console.WriteLine($"- {suggestion}");
                }
            }

            textRequest = new TextRequest { Text = "Солнце светит ярко сегодня утром просто потрясающая погода чтобы прогуляться и насладиться свежим воздухом", Language = "ru" };
            var grammarResponse = await client.CheckGrammarAsync(textRequest);
            foreach (var error in grammarResponse.Errors)
            {
                Console.WriteLine($"Sentence: {error.Sentence}");
                Console.WriteLine($"Message: {error.Message}");
            }

            var synonymRequest = new SynonymRequest { Words = { "example" }, Language = "en" };
            var synonymResponse = await client.GetSynonymsAsync(synonymRequest);
            Console.WriteLine("Synonyms retrieved!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("Client finished.");
    }
}