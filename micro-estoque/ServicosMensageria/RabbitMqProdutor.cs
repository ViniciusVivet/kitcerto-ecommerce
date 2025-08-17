using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using shared_kit.Eventos;

namespace micro_estoque.ServicosMensageria
{
    public class RabbitMqProdutor
    {
        private readonly string _hostname = "localhost";
        private readonly string _queueName = "fila_estoque_vendas";
        private IConnection _connection;

        public RabbitMqProdutor()
        {
            var factory = new ConnectionFactory() { HostName = _hostname };
            _connection = factory.CreateConnection();
        }

        public void EnviarMensagem(BaseEvento evento)
        {
            using var canal = _connection.CreateModel();
            canal.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var json = JsonSerializer.Serialize(evento);
            var corpo = Encoding.UTF8.GetBytes(json);

            canal.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: corpo);
            Console.WriteLine($"[KITCERTO-LOG] Evento enviado → {evento.GetType().Name} | {evento.Origem} | {evento.DataCriacao}");
        }
    }
}