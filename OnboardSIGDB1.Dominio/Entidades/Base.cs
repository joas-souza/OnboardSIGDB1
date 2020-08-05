using FluentValidation;
using FluentValidation.Results;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnboardSIGDB1.Dominio.Entidades
{

    public abstract class Base<TEntity> : AbstractValidator<TEntity> where TEntity : Base<TEntity>
    {
        protected Base() { }

        public int Id { get; protected set; }

        [NotMapped]
        public ValidationResult Result { get; protected set; }

        public abstract bool Validar();
    }
}
