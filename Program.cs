using Backend.Mail;
using Backend.Profiles;
using Backend.Services.UserRepositories;
using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<BaseContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("MySqlConnection"),
        Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.20-mysql")));


builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<MailController>();


var app = builder.Build();

app.MapControllers();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();

