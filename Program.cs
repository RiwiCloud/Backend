using Backend.Profiles;
using Backend.Services.UserRepositories;
using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Backend.Controllers.Folders;
using Backend.Services.Interfaces;
using Backend.Services.Implementations;
using System.Text.Json.Serialization;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Backend.Services.Email;

var builder = WebApplication.CreateBuilder(args);

// Clave para la autenticación JWT
var key = Encoding.UTF8.GetBytes("ncjdncjvurbuedxwn233nnedxee+dfr-");

// Configuración de la autenticación JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "https://localhost:5098",
        ValidAudience = "https://localhost:5098",
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// Configuración de la autorización
builder.Services.AddAuthorization();


// Clave para la autenticación JWT
var key = Encoding.UTF8.GetBytes("ncjdncjvurbuedxwn233nnedxee+dfr-");

// Configuración de la autenticación JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "https://localhost:5098",
        ValidAudience = "https://localhost:5098",
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// Configuración de la autorización
builder.Services.AddAuthorization();


// Agregar servicios al contenedor
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Configuración de JsonSerializer para manejar ciclos de referencia
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

// Configuración de la base de datos
builder.Services.AddDbContext<BaseContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("MySqlConnection"),
        Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.20-mysql")));

// Configuración de AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

// Registro de servicios
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFolderRepository, FolderRepository>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<MailerSendService>();


var app = builder.Build();

app.MapControllers();

// Configuración del pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
