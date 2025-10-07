using MinhasTarefasAPI.Database;
using MinhasTarefasAPI.Repositories.V1.Contracts;
using MinhasTarefasAPI.V1.Models;
using System.Linq;

namespace MinhasTarefasAPI.V1.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly MinhasTarefasContext _banco;

        public TokenRepository(MinhasTarefasContext banco)
        {
            _banco = banco;
        }

        public Token Obter(string refreshToken)
        {
            return _banco.Token.FirstOrDefault(a => a.RefreshToken == refreshToken && a.Utilizado == false);
        }

        public void Cadastrar(Token token)
        {
            _banco.Add(token);
            _banco.SaveChanges();
        }

        public void Atualizar(Token token)
        {
            _banco.Update(token);
            _banco.SaveChanges();
        }
    }
}
