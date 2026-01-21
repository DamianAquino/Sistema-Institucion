using API_Institucion.Persistencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using API_Institucion;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using API_Institucion.Interfaces;
using API_Institucion.Services;

var builder = WebApplication.CreateBuilder(args);

// Agregar la clase 'Conexion_Db' como dependecia de los controladores (Sin uso a la hora de escribir esto).
//builder.Services.AddSingleton<Conexion_Db>();
builder.Services.AddSingleton<Logger>();

// Registro de servcios
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IFileService, FileService>();

// Agregar conexto de base de datos (Todas las clases que heredan de DbContext pueden acceder a la cadena de conexion si existe la siguiente linea)
builder.Services.AddDbContext<Conexion_Db>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar autenticación con JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        };
    });

// Agregar autenticacion basada en roles.
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AdminPolicy", policy => policy.RequireClaim("UsuarioType", "Admin"))
    .AddPolicy("ProfesorPolicy", policy => policy.RequireClaim("UsuarioType", "Profesor"))
    .AddPolicy("AlumnoPolicy", policy => policy.RequireClaim("UsuarioType", "Alumno"));


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File(
        path: "logs/api-.log",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30
    )
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Se agrega esta linea cuando se agrega autenticacion JWT.
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
