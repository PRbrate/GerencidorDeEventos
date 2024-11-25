using GerencidorDeEventos;
using GerencidorDeEventos.ApiConfig;
using GerencidorDeEventos.Repository;
using GerencidorDeEventos.Repository.Interface;
using GerencidorDeEventos.Service;
using GerencidorDeEventos.Service.inteface;
using GerencidorDeEventos.Service.Validations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });

builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
    options.MapType<DateTime>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "date", // Define apenas a data no Swagger
        Example = new Microsoft.OpenApi.Any.OpenApiString("2024-11-20")
    });
});

var key = Encoding.ASCII.GetBytes(SenhaToken.Secret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
    };
});

builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("autorizado", policy =>
    policy.RequireClaim("administrador", "true"));
});

builder.AddSwaggerConfig();
builder.Services.AddScoped<DataBaseContext>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IvalidaUsuarioAtualizacao, ValidaUsuarioAtualizacao>();

builder.Services.AddScoped<IEventoService, EventoService>();
builder.Services.AddScoped<IEventoRepository, EventoRepository>();

builder.Services.AddScoped<IInscricoesRepository, InscricoesRepository>();
builder.Services.AddScoped<IInscricaoService, InscricoesService>();

builder.Services.AddScoped<IMinicursoRepository, MinicursoRepository>();
builder.Services.AddScoped<IMinicursoService, MinicursoService>();

builder.Services.AddScoped<IPalestraRepository, PalestraRepository>();
builder.Services.AddScoped<IPalestraService, PalestraService>();


builder.Services.AddDbContext<DataBaseContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("Connection")));



var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(x =>
    x.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
