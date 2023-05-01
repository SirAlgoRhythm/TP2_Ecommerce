using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc(option => option.EnableEndpointRouting = false);

builder.Services.AddSwaggerGen(opt =>
{
    //jeton authentification
    opt.AddSecurityDefinition("Jeton", new OpenApiSecurityScheme
    {
        Name = "Bearer",
        BearerFormat = "JWT",
        Scheme = "bearer",
        Description = "Specify the authorization token.",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Jeton"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => 
{
    options.Authority = "http://localhost:5000";
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime=true
    };
});
builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser().Build());
});

var app = builder.Build();

app.UseMvc();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Jeton authentification
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
