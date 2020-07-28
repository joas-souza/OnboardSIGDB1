using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnboardSIGDB1.Dominio.Entidades
{
    public class Empresa
    {

        public Empresa(int id, string nome, string cnpj, DateTime dataFundacao)
        {
            Id = id;
            Nome = nome;
            Cnpj = cnpj;
            DataFundacao = dataFundacao;
        }

        public int Id { get; private set; }

        [Required]
        [StringLength(150)]
        public string Nome { get; private set; }

        [Required]
        [StringLength(14)]
        public string Cnpj { get; private set; }

        public DateTime DataFundacao { get; private set; }

        public void AlterarNome(string nome)
        {
            Nome = nome;
        }
    }
}
