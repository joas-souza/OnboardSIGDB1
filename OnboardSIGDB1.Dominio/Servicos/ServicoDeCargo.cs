using OnboardSIGDB1.Dominio.Dtos.Cargo;
using OnboardSIGDB1.Dominio.Entidades;
using OnboardSIGDB1.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnboardSIGDB1.Dominio.Servicos
{
    public class ServicoDeCargo : IServicoDeCargo
    {
        private readonly IRepositorioDeCargo _repositorioDeCargo;

        public ServicoDeCargo(IRepositorioDeCargo repositorioDeCargo)
        {
            _repositorioDeCargo = repositorioDeCargo;
        }

        public CargoDto Salvar(CargoDto cargoDto)
        {
            var cargo = ConverterParaEntidade(cargoDto);

            _repositorioDeCargo.Salvar(cargo);

            return ConverterParaDto(cargo);
        }

        public CargoDto Alterar(int id, CargoDto cargoDto)
        {
            if (Validar(id, cargoDto))
            {
                var cargo = ConverterParaEntidade(cargoDto);

                _repositorioDeCargo.Alterar(cargo);

                return ConverterParaDto(cargo);
            }

            return default;
        }

        public void Excluir(int id)
        {
            if (id > 0)
                _repositorioDeCargo.Excluir(id);
        }

        public CargoDto RecuperarPorId(int id)
        {
            var cargo = _repositorioDeCargo.RecuperarPorId(id);
            return ConverterParaDto(cargo);
        }

        private bool Validar(int id, CargoDto cargoDto)
        {
            if (id != cargoDto.Id)
                throw new Exception("Cargo não identificado.");

            return true;
        }

        private Cargo ConverterParaEntidade(CargoDto cargoDto)
        {
            return new Cargo (cargoDto.Id, cargoDto.Descricao);
        }

        private CargoDto ConverterParaDto(Cargo cargo)
        {
            return new CargoDto { Id = cargo.Id, Descricao = cargo.Descricao};
        }
    }
}
