using Application.Services;
using Application.UseCases.Users;
using Domain.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    // Configurar para manejar archivos grandes
    options.MaxModelBindingCollectionSize = 1024;
});

// Configurar para manejar archivos
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 10 * 1024 * 1024; // 10MB
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS configuration
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? new[] { "http://localhost:4200" };
Console.WriteLine("AllowedOrigins: " + string.Join(", ", allowedOrigins));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        // SOLO PARA PRUEBAS: Permitir cualquier origen
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
        // Si quieres volver a restringir, comenta las líneas de arriba y descomenta las de abajo:
        // policy.WithOrigins(allowedOrigins)
        //       .AllowAnyHeader()
        //       .AllowAnyMethod()
        //       .AllowCredentials();
    });
});

// Database configuration
builder.Services.AddScoped<IDatabaseConnection>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new SqlServerConnection(connectionString!);
});

// Service registrations
builder.Services.AddScoped<IFileService, FileService>();

// Repository registrations
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Use case registrations
builder.Services.AddScoped<IGetUserUseCase, GetUserUseCase>();
builder.Services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
builder.Services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
builder.Services.AddScoped<IUpdateUserPhotoUseCase, UpdateUserPhotoUseCase>();
builder.Services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use CORS
app.UseCors("AllowAngularApp");

// Configure static files for serving images
app.UseStaticFiles(); // Para archivos estáticos por defecto (wwwroot)

// Configurar archivos estáticos para la carpeta img con CORS
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "img")),
    RequestPath = "/img",
    OnPrepareResponse = ctx =>
    {
        var origin = ctx.Context.Request.Headers["Origin"].ToString();
        var allowedOrigins = new[] {
            "http://localhost:4200",
            "https://localhost:4200",
            "http://192.168.100.3:4200",
            "https://192.168.100.3:4200",
            "https://c888-2800-bf0-8027-1311-2021-64b1-441c-576b.ngrok-free.app"
        };
        if (allowedOrigins.Contains(origin))
        {
            ctx.Context.Response.Headers.Append("Access-Control-Allow-Origin", origin);
            ctx.Context.Response.Headers.Append("Access-Control-Allow-Credentials", "true");
        }
    }
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run(); 