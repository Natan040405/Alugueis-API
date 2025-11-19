using alugueis_api.Data;
using alugueis_api.Handlers;
using alugueis_api.Interfaces;
using alugueis_api.NovaPasta;
using alugueis_api.Repositories;
using alugueis_api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<DespesaRepository>();
builder.Services.AddScoped<AptoRepository>();
builder.Services.AddScoped<IDespesaService, DespesaService>();
builder.Services.AddScoped<RecalculationDespesaRateioHandler>();
builder.Services.AddScoped<AddDespesaAptoHandler>();
builder.Services.AddScoped<UpdateDespesaAptoHandler>();
builder.Services.AddScoped<DeleteDespesaAptoHandler>();
builder.Services.AddScoped<GetDespesaAptoHandler>();
builder.Services.AddControllers();

//  pega a string de conexão do appsettings.json
var connectionString = builder.Configuration.GetConnectionString("AppDbConnectionString");

//  registra o DbContext no DI
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

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
