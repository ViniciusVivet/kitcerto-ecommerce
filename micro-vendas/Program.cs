using micro_vendas.Data;
using micro_vendas.ServicosMensageria;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

// Adicionamos o contexto do banco de dados
builder.Services.AddDbContext<KitCertoContexto>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicionamos a configuração para os controllers
builder.Services.AddControllers();

// Adicionamos a configuração para o Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configura o HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting(); // Linha adicionada para corrigir o erro 405

app.UseAuthorization();

app.MapControllers();

// Configuração para iniciar o consumidor de eventos do RabbitMQ
var consumidor = new RabbitMqConsumidor();
Task.Run(() => consumidor.IniciarConsumo());

app.Run();