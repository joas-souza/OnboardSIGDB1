using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardSIGDB1.Dominio.Dtos.Funcionario
{
    public class Filtro
    {
        public string Nome { get; set; }

        public string Cpf { get; set; }

        public DateTime DataContratacao { get; set; }
    }
}
