using TalkToAPI.V1.Models;

namespace TalkToAPI.Repositories.V1.Contracts
{
    public interface ITokenRepository
    {
        void Cadastrar(Token token);
        Token Obter(string refreshToken);
        void Atualizar(Token token);
    }
}
