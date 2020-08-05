using AutoMapper;
using OnboardSIGDB1.Dominio.Dtos.Cargo;
using OnboardSIGDB1.Dominio.Entidades;
using OnboardSIGDB1.Dominio.Interfaces;
using OnboardSIGDB1.Dominio.Servicos.Notificacoes;
using System.Threading.Tasks;

namespace OnboardSIGDB1.Dominio.Servicos
{
    public class ArmazenadorDeCargo : IArmazenadorDeCargo
    {
        private readonly IRepositorioDeCargo _repositorioDeCargo;
        private readonly NotificationContext _notificationContext;

        public ArmazenadorDeCargo(IRepositorioDeCargo repositorioDeCargo,
            NotificationContext notificationContext)
        {
            _repositorioDeCargo = repositorioDeCargo;
            _notificationContext = notificationContext;
        }

        public async Task Salvar(CargoDto cargoDto)
        {
            var cargo = Mapper.Map<Cargo>(cargoDto);

            if (cargo.Validar())
                await _repositorioDeCargo.Salvar(cargo); 
            else
                _notificationContext.AddNotifications(cargo.Result);
        }
    }
}
