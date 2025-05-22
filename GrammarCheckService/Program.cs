namespace GrammarCheckService
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });

            builder.WebHost.UseKestrel(options =>
            {
                options.ConfigureHttpsDefaults(httpsOptions =>
                {
                    httpsOptions.SslProtocols = System.Security.Authentication.SslProtocols.Tls13;
                });
            });

            builder.Services.AddGrpc();

            builder.Services.AddGrpcClient<GrammarCheck.GrammarCheckClient>(o =>
            {
                o.Address = new Uri("https://localhost:5001");
            });

            builder.Services.AddControllers();
            builder.Services.AddLogging();

            var app = builder.Build();

            app.UseCors("AllowAll");

            app.MapGrpcService<Services.GrammarCheckService>();
            app.MapControllers();

            app.Run();
        }
    }
}
