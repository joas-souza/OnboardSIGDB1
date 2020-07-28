using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardSIGDB1.Dominio.Dtos.Empresa
{
    public class Filtro
    {
        public string Nome { get; set; }

        public string Cnpj { get; set; }

        public DateTime DataFundacao { get; set; }
    }
}
