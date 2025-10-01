using MinhasTarefasAPI.Models;

namespace MinhasTarefasAPI.Repositories.Contracts
{
    public interface ITokenRepository
    {
        void Cadastrar(Token token);
        Token Obter(string refreshToken);
        void Atualizar(Token token);
    }
}
