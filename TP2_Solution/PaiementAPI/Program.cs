using PaiementAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc(option => option.EnableEndpointRouting = false)
    .AddNewtonsoftJson(option =>
    {
        option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

builder.Services.Configure<StripeSettings>(options =>
{
    options.PublicKey = builder.Configuration["Stripe:PublicKey"];
    options.SecretKey = builder.Configuration["Stripe:SecretKey"];
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
Stripe.StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
