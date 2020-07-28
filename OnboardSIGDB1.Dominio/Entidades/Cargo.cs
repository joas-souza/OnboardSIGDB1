using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnboardSIGDB1.Dominio.Entidades
{
    public class Cargo
    {
        public Cargo(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }

        public int Id { get; set; }
       
        [Required]
        [StringLength(250)]
        public string Descricao { get; set; }
    }
}
