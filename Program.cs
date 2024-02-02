using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Backend165.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// MongoDB Client Dienstkonfiguration
builder.Services.AddSingleton<IMongoClient>(s =>
    new MongoClient(builder.Configuration["MongoDB:ConnectionString"]));

// Benutzerdefinierte Dienste registrieren
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<OrderService>();

// JWT-Authentifizierung konfigurieren
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// Standarddienste hinzufügen
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Bitte das Wort 'Bearer' gefolgt von einem Leerzeichen und dem JWT eingeben.",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// HTTP-Anforderungspipeline konfigurieren
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Stellen Sie sicher, dass UseAuthentication vor UseAuthorization aufgerufen wird
app.UseAuthorization();

app.MapControllers();

app.Run();
