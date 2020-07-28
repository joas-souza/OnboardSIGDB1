using OnboardSIGDB1.Dominio.Dtos.Funcionario;


namespace OnboardSIGDB1.Dominio.Interfaces
{
    public interface IServicoDeFuncionario
    {
        FuncionarioDto Salvar(FuncionarioDto FuncionarioDto);

        FuncionarioDto Alterar(int id, FuncionarioDto FuncionarioDto);

        FuncionarioDto VincularEmpresa(FuncionarioDto funcionarioDto);

        FuncionarioDto VincularCargo(FuncionarioDto funcionarioDto);

        void Excluir(int id);

        FuncionarioDto RecuperarPorId(int id);

    }
}
