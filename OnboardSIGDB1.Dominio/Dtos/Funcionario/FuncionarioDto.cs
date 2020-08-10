using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardSIGDB1.Dominio.Dtos.Funcionario
{
    public class FuncionarioDto
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Cpf { get; set; }

        public DateTime DataContratacao { get; set; }

        public int?  EmpresaId { get; set; }

        public int? CargoId { get; set; }
    }
}
