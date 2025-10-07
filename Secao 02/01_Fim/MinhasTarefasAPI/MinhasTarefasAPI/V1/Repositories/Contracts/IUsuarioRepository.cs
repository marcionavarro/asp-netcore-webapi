using MinhasTarefasAPI.V1.Models;

namespace MinhasTarefasAPI.Repositories.V1.Contracts
{
    public interface IUsuarioRepository
    {
        void Cadastrar(ApplicationUser usuario, string senha);
        ApplicationUser Obter(string email, string senha);
        ApplicationUser Obter(string id);
    }
}
