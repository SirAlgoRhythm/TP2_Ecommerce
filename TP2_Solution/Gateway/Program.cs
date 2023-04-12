using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// D�claration du dossier Routes
var routes = "Routes";
builder.Configuration.AddOcelotWithSwaggerSupport(options =>
{
    options.Folder = routes;
});

// D�claration des services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddSwaggerForOcelot(builder.Configuration);


// Add services to the container.

var app = builder.Build();

//Utilisation des services d�clar�s
app.UseSwagger();
app.UseSwaggerUI(config =>
{
    config.SwaggerEndpoint("/swagger/swagger.json", "Facture Rest API");
});
app.UseSwaggerForOcelotUI(options =>
{
    options.PathToSwaggerGenerator = "/swagger/docs";
});
app.UseOcelot().Wait();

app.Run();