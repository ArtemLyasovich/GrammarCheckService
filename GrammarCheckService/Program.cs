namespace GrammarCheckService;

class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddGrpc();
        builder.Services.AddLogging();

        var app = builder.Build();

        app.MapGrpcService<Services.GrammarCheckService>();

        app.Run();
    }
}