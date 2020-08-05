
namespace OnboardSIGDB1.Dominio.Entidades
{
    public class Notificacao
    {
		protected Notificacao() { }

		public Notificacao(string key, string message)
		{
			Key = key;
			Message = message;
		}

		public string Key { get; }
		public string Message { get; }
	}
}
