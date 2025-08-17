using System;
namespace shared_kit.Eventos
{
    public abstract class BaseEvento
    {
        public DateTime DataCriacao { get; private set; } = DateTime.UtcNow;
        public string Origem { get; private set; }
        public BaseEvento(string origem)
        {
            Origem = origem;
        }
    }
}