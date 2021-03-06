﻿using OnboardSIGDB1.Dominio.Dtos.Cargo;
using OnboardSIGDB1.Dominio.Interfaces;
using OnboardSIGDB1.Dominio.Servicos.Notificacoes;
using OnboardSIGDB1.Utils.Resources;
using System.Threading.Tasks;

namespace OnboardSIGDB1.Dominio.Servicos
{
    public class AlteradorDeCargo : IAlteradorDeCargo
    {
        private readonly NotificationContext _notificationContext;
        private readonly IRepositorioDeCargo _repositorioDeCargo;

        public AlteradorDeCargo(IRepositorioDeCargo repositorioDeCargo,
            NotificationContext notificationContext)
        {
            _repositorioDeCargo = repositorioDeCargo;
            _notificationContext = notificationContext;
        }

        public async Task Alterar(int id, CargoDto cargoDto)
        {
            if (ValidarId(id, cargoDto))
            {
                var cargo = await _repositorioDeCargo.RecuperarPorId(id);

                if (cargo != null)
                {
                    cargo.AlterarDescricao(cargoDto.Descricao);

                    if (!cargo.Validar())
                        _notificationContext.AddNotifications(cargo.Result);
                }
                else
                    _notificationContext.AddNotification("", Resource.CargoNaoLocalizado);
            }
        }

        private bool ValidarId(int id, CargoDto cargoDto)
        {
            if (id != cargoDto.Id)
            {
                _notificationContext.AddNotification("", Resource.CargoNaoIdentificado);
                return false;
            }

            return true;
        }
    }
}
