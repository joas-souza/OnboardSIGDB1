﻿using System.Threading.Tasks;

namespace OnboardSIGDB1.Dominio.Interfaces
{
    public interface IRemovedorDeFuncionario
    {
        Task Excluir(int id);
    }
}
