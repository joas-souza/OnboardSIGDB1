using AutoMapper;
using OnboardSIGDB1.Dominio.Dtos.Cargo;
using OnboardSIGDB1.Dominio.Dtos.Empresa;
using OnboardSIGDB1.Dominio.Dtos.Funcionario;
using OnboardSIGDB1.Dominio.Entidades;
using OnboardSIGDB1.Dominio.Utils;
using System.Linq;

namespace OnboardSIGDB1.Infraestrutura.MapperProjeto
{
    public class OnboardMapper : Profile
    {
        public OnboardMapper()
        {
            CreateMap<Cargo, CargoDto>();
            CreateMap<CargoDto, Cargo>();

            CreateMap<Empresa, EmpresaDto>();
            CreateMap<EmpresaDto, Empresa>();

            CreateMap<Funcionario, FuncionarioDto>()
                    .ForMember(dest => dest.Cpf, o => o.MapFrom(src => MetodosUteis.RemoverMascara(src.Cpf)))
                    .ForMember(dest => dest.CargoId, o => o.MapFrom(src => src.CargosFuncionario.FirstOrDefault().CargoId));

            CreateMap<FuncionarioDto, Funcionario>()
                    .ForMember(dest => dest.Cpf, o => o.MapFrom(src => MetodosUteis.RemoverMascara(src.Cpf)));
        }
    }
}
