using MinhasTarefasAPI.V1.Models;

namespace MinhasTarefasAPI.Repositories.V1.Contracts
{
    public interface ITokenRepository
    {
        void Cadastrar(Token token);
        Token Obter(string refreshToken);
        void Atualizar(Token token);
    }
}
