using Grpc;
using API_Gateway.Controllers;
using System.Security.Cryptography.Xml;
using API_Gateway.Grpcs.grpcs;
public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.WebHost.ConfigureKestrel(options =>
        {
            options.ListenAnyIP(5003);
        });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddGrpcClient<KundenVerwaltungsService.KundenVerwaltungsServiceClient>(o =>
        {
            o.Address = new Uri("https://localhost:5002");
        });

        builder.Services.AddGrpcClient<TicketVerwaltungsService.TicketVerwaltungsServiceClient>(o =>
        {
            o.Address = new Uri("https://localhost:5002");
        });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAllHelpDeskFrontend", policy =>
            {
                policy.WithOrigins("http://localhost:60881")
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
        });

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {   
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //app.UseHttpsRedirection();
        app.UseCors("AllowAllHelpDeskFrontend");
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}