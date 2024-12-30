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
            var textRequest = new TextRequest { Text = "Ths is a tst text", Language = "en" };
            var spellingResponse = await client.CheckSpellingAsync(textRequest);
            Console.WriteLine("Spelling check completed!");

            var grammarResponse = await client.CheckGrammarAsync(textRequest);
            Console.WriteLine("Grammar check completed!");

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