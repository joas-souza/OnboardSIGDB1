using OnboardSIGDB1.Dominio.Dtos.Funcionario;
using OnboardSIGDB1.Dominio.Entidades;
using OnboardSIGDB1.Dominio.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace OnboardSIGDB1.Dominio.Servicos
{
    public class ServicoDeFuncionario : IServicoDeFuncionario
    {
        private readonly IRepositorioDeFuncionario _repositorioDeFuncionario;
        private readonly IRepositorioDeEmpresa _repositorioDeEmpresa;
        private readonly IRepositorioDeCargo _repositorioDeCargo;

        public ServicoDeFuncionario(IRepositorioDeFuncionario repositorioDeFuncionario, IRepositorioDeEmpresa repositorioDeEmpresa, IRepositorioDeCargo repositorioDeCargo)
        {
            _repositorioDeFuncionario = repositorioDeFuncionario;
            _repositorioDeEmpresa = repositorioDeEmpresa;
            _repositorioDeCargo = repositorioDeCargo;
        }

        public FuncionarioDto Salvar(FuncionarioDto funcionarioDto)
        {
            var funcionario = ConverterParaEntidade(funcionarioDto);

            _repositorioDeFuncionario.Salvar(funcionario);

            return ConverterParaDto(funcionario);
        }

        public FuncionarioDto Alterar(int id, FuncionarioDto funcionarioDto)
        {
            if (Validar(id, funcionarioDto))
            {
                var funcionario = _repositorioDeFuncionario.RecuperarPorId(id);

                if (funcionario != null)
                {
                    funcionario.AlterarNome(funcionarioDto.Nome);

                    _repositorioDeFuncionario.Alterar(funcionario);

                    return ConverterParaDto(funcionario);
                }
            }
            return default;
        }

        public FuncionarioDto VincularEmpresa(FuncionarioDto funcionarioDto)
        {
            var funcionario = _repositorioDeFuncionario.RecuperarPorId(funcionarioDto.Id);

            if (funcionario != null)
            {
                var empresa = _repositorioDeEmpresa.RecuperarPorId((int)funcionarioDto.EmpresaId);

                if (empresa != null)
                {
                    funcionario.AlterarEmpresa(empresa);
                }

                _repositorioDeFuncionario.Alterar(funcionario);

                return ConverterParaDto(funcionario);
            }
            return funcionarioDto;
        }

        public FuncionarioDto VincularCargo(FuncionarioDto funcionarioDto)
        {
            var funcionario = _repositorioDeFuncionario.RecuperarPorId(funcionarioDto.Id);

            if (funcionario != null)
            {
                if (funcionario.EmpresaId != null)
                {
                    var cargo = _repositorioDeCargo.RecuperarPorId((int)funcionarioDto.CargoId);

                    if (cargo != null)
                    {
                        funcionario.AlterarCargo(cargo);
                    }
                }
                else if (funcionarioDto.CargoId > 0)
                    throw new Exception("Funcionario não vinculado a nenhuma empresa.");

                _repositorioDeFuncionario.Alterar(funcionario);

                return ConverterParaDto(funcionario);
            }
            return funcionarioDto;
        }

        public void Excluir(int id)
        {
            _repositorioDeFuncionario.Excluir(id);
        }

        public FuncionarioDto RecuperarPorId(int id)
        {
            var funcionario = _repositorioDeFuncionario.RecuperarPorId(id);
            return ConverterParaDto(funcionario);
        }

        private bool Validar(int id, FuncionarioDto funcionarioDto)
        {
            if (id != funcionarioDto.Id)
                throw new Exception("Funcionario não identificado.");

            if (string.IsNullOrEmpty(funcionarioDto.Nome))
                throw new Exception("Nome inválido.");

            return true;
        }

        private string RemoverMascaraCpf(string cpf)
        {
            return cpf.Replace(".", "").Replace("-", "");
        }

        private Funcionario ConverterParaEntidade(FuncionarioDto funcionarioDto)
        {
            return new Funcionario(funcionarioDto.Id, funcionarioDto.Nome, RemoverMascaraCpf(funcionarioDto.Cpf), funcionarioDto.DataContratacao);
        }

        private FuncionarioDto ConverterParaDto(Funcionario funcionario)
        {
            return new FuncionarioDto { Id = funcionario.Id, Nome = funcionario.Nome, Cpf = funcionario.Cpf, DataContratacao = funcionario.DataContratacao, CargoId = funcionario.CargoId, EmpresaId = funcionario.EmpresaId };
        }
    }
}
