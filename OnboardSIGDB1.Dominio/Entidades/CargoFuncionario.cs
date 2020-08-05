using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace OnboardSIGDB1.Dominio.Entidades
{
    public class CargoFuncionario//: Base<CargoFuncionario>
    {

        protected CargoFuncionario() { }

        public CargoFuncionario(Cargo cargo,Funcionario funcionario)
        {
            Cargo = cargo;
            Funcionario = funcionario;
        }

        public int CargoId { get; set; }
        public virtual Cargo Cargo { get; set; }
        public int FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }

        //public override bool Validar()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
