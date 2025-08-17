using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using shared_kit.Eventos;

namespace micro_vendas.ServicosMensageria
{
    public class RabbitMqConsumidor
    {
        private readonly string _hostname = "localhost";
        private readonly string _queueName = "fila_estoque_vendas";
        private IConnection _connection;

        public RabbitMqConsumidor()
        {
            var factory = new ConnectionFactory() { HostName = _hostname };
            _connection = factory.CreateConnection();
        }

        public void IniciarConsumo()
        {
            using var canal = _connection.CreateModel();
            canal.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumidor = new EventingBasicConsumer(canal);
            consumidor.Received += (model, ea) =>
            {
                var corpo = ea.Body.ToArray();
                var mensagem = Encoding.UTF8.GetString(corpo);
                var evento = JsonSerializer.Deserialize<ProdutoBaixadoEstoqueEvento>(mensagem);
                Console.WriteLine($"[ZL-NOTIFY] Recebido evento → Produto: {evento?.ProdutoId} | Quantidade: {evento?.QuantidadeRetirada}");
            };

            canal.BasicConsume(queue: _queueName, autoAck: true, consumer: consumidor);
            Console.WriteLine("[ZL-NOTIFY] Consumidor iniciado. Aguardando mensagens...");
            Console.ReadLine();
        }
    }
}