using AA2.Data;
using Microsoft.EntityFrameworkCore;
using AA2.Services;


var builder = WebApplication.CreateBuilder(args);
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

// Controladores + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
