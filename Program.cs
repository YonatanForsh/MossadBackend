using Microsoft.EntityFrameworkCore;
using MossadBackend.DB;
using MossadBackend.Tools;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DbServer>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<AgentService>();
builder.Services.AddScoped<TargetService>();
builder.Services.AddScoped<MissionService>();
builder.Services.AddScoped<SetMission>();


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
