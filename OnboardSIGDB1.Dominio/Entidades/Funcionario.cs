using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnboardSIGDB1.Dominio.Entidades
{
    public class Funcionario
    {

        public Funcionario(int id, string nome, string cpf, DateTime dataContratacao)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            DataContratacao = dataContratacao;
        }

        public int Id { get;  private set; }

        [Required]
        [StringLength(150)]
        public string Nome { get; private set; }

        [Required]
        [StringLength(11)]
        public string Cpf { get; private set; }

        public DateTime DataContratacao { get; private set; }

        public Empresa Empresa { get; private set; }

        public int? EmpresaId { get; private set; }

        public Cargo Cargo { get; private set; }

        public int? CargoId { get; private set; }

        public void AlterarNome(string nome)
        {
            Nome = nome;
        }

        public void AlterarEmpresa (Empresa empresa)
        {
            Empresa = empresa;
        }

        public void AlterarCargo(Cargo cargo)
        {
            Cargo = cargo;
        }
    }
}
