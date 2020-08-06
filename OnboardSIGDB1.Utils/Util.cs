
namespace OnboardSIGDB1.Utils
{
    public class Util
    {

        public static string RemoverMascara(string valor)
        {
            return valor.Replace(".", "").Replace("-", "").Replace("/", "");
        }
    }
}
