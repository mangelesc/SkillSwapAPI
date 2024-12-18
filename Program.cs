using Microsoft.EntityFrameworkCore;
using SkillSwapAPI.Interfaces;
using SkillSwapAPI.Data;
using SkillSwapAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configura la cadena de conexión a SQLite desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("SkillSwapConnection");

// Registra el DbContext con SQLite
builder.Services.AddDbContext<SkillSwapContext>(options =>
    options.UseSqlite(connectionString));

// Injección de dependencias
builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
