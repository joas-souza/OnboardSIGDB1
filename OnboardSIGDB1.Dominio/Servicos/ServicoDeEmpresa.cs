using OnboardSIGDB1.Dominio.Dtos.Empresa;
using OnboardSIGDB1.Dominio.Entidades;
using OnboardSIGDB1.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnboardSIGDB1.Dominio.Servicos
{
    public class ServicoDeEmpresa : IServicoDeEmpresa
    {
        private readonly IRepositorioDeEmpresa _repositorioDeEmpresa;

        public ServicoDeEmpresa(IRepositorioDeEmpresa repositorioDeEmpresa)
        {
            _repositorioDeEmpresa = repositorioDeEmpresa;
        }

        public EmpresaDto Salvar(EmpresaDto empresaDto)
        {
            var empresa = ConverterParaEntidade(empresaDto);

            _repositorioDeEmpresa.Salvar(empresa);

            return ConverterParaDto(empresa);
        }

        public EmpresaDto Alterar(int id, EmpresaDto empresaDto)
        {
            if (Validar(id, empresaDto))
            {
                var empresa = _repositorioDeEmpresa.RecuperarPorId(id);

                if (empresa != null)
                {
                    empresa.AlterarNome(empresaDto.Nome);

                    _repositorioDeEmpresa.Alterar(empresa);

                    return ConverterParaDto(empresa);
                }
            }
            return default;
        }

        public void Excluir(int id)
        {
            if (id > 0)
                _repositorioDeEmpresa.Excluir(id);
        }

        public EmpresaDto RecuperarPorId(int id)
        {
            var empresa = _repositorioDeEmpresa.RecuperarPorId(id);
            return ConverterParaDto(empresa);
        }

        private bool Validar(int id, EmpresaDto empresaDto)
        {
            if (id != empresaDto.Id)
                throw new Exception("Empresa não identificada.");

            if (string.IsNullOrEmpty(empresaDto.Nome))
                throw new Exception("Nome inválido.");

            return true;
        }

        private string RemoverMascaraCnpj(string cnpj)
        {
            return cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
        }

        private Empresa ConverterParaEntidade(EmpresaDto empresaDto)
        {
            return new Empresa(empresaDto.Id, empresaDto.Nome, RemoverMascaraCnpj(empresaDto.Cnpj), empresaDto.DataFundacao);
        }

        private EmpresaDto ConverterParaDto(Empresa empresa)
        {
            return new EmpresaDto { Id = empresa.Id, Nome = empresa.Nome, Cnpj = empresa.Cnpj, DataFundacao = empresa.DataFundacao };
        }
    }
}
