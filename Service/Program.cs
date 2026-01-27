using Service.Models;
using Service.Protos;
using Service.Services;

internal class Program
{

    private static (int httpPort, int grpcPort) GetPorts(IConfiguration config)
    {
        return (Convert.ToInt32(config["HttpPort"]),Convert.ToInt32(config["GrpcPort"]));
    }
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddGrpc();

        builder.Services.AddDbContext<HelpDeskContext>();

        // Configure the HTTP request pipeline.
        
        var ports = GetPorts(builder.Configuration);
        int grpcPort = ports.grpcPort;
        int httpPort = ports.httpPort;
        builder.WebHost.ConfigureKestrel(options =>
        {
            options.ListenAnyIP(httpPort);
            options.ListenAnyIP(grpcPort, o => o.UseHttps());
        });

        var app = builder.Build();

        app.MapGrpcService<KundenVerwaltungsServiceImpl>();
        app.MapGrpcService<TicketVerwaltungsServiceImpl>();
        app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
        app.Run();
    }
}