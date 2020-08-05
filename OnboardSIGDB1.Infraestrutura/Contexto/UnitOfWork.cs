using OnboardSIGDB1.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnboardSIGDB1.Infraestrutura.Contexto
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly OnboardDbContext _contexto;

        public UnitOfWork(OnboardDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task Commit()
        {
            await _contexto.SaveChangesAsync();
        }
    }
}
