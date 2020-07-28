using OnboardSIGDB1.Dominio.Dtos.Cargo;
using System.Collections.Generic;

namespace OnboardSIGDB1.Dominio.Interfaces
{
    public interface IServicoDeCargo
    {
        CargoDto Salvar(CargoDto cargoDto);

        CargoDto Alterar(int id, CargoDto cargoDto);

        void Excluir(int id);

        CargoDto RecuperarPorId(int id);
      
    }
}
