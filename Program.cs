using AA2.Data;
using Microsoft.EntityFrameworkCore;
using AA2.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
        };
    });

var connectionString = builder.Configuration.GetConnectionString("AA2Db");

// Registrar servicios
builder.Services.AddScoped<IUsuarioRepository, UsuarioEfRepository>();
builder.Services.AddScoped<IMedicoRepository, MedicoEfRepository>();
builder.Services.AddScoped<ICitaRepository, CitaEfRepository>();

builder.Services.AddDbContext<AA2DbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUsuarioServices, UsuarioServices>();
builder.Services.AddScoped<IMedicoServices, MedicoServices>();
builder.Services.AddScoped<ICitaServices, CitaServices>();
builder.Services.AddScoped<IAuthServices, AuthServices>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioEfRepository>();

// Controladores + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "AA2 API", 
        Description = "API para gestión de citas médicas",
        Version = "v1"
    });

    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor, introduce el token JWT con Bearer",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[]{}
        }
    });
});


var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AA2 API v1"));
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
