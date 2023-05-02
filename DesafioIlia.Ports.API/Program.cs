using Microsoft.OpenApi.Models;
using System.Reflection;
using DesafioIlia.Infra.IoC;

var builder = WebApplication.CreateBuilder(args);

//Add
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Controle de Ponto API",
        Description = "Desafio técnico da Ília",
        TermsOfService = new Uri("https://github.com/IAmHopp/desafio-ilia"),
    });

    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Configuration.AddJsonFile("appsettings.json");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();