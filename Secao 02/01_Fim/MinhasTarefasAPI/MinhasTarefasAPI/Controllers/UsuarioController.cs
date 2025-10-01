using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MinhasTarefasAPI.Models;
using MinhasTarefasAPI.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MinhasTarefasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsuarioController(
            IUsuarioRepository usuarioRepository,
            ITokenRepository tokenRepository,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _usuarioRepository = usuarioRepository;
            _tokenRepository = tokenRepository;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] UsuarioDTO usuarioDTO)
        {
            ModelState.Remove("ConfirmacaoSenha");
            ModelState.Remove("Nome");

            if (ModelState.IsValid)
            {
                ApplicationUser usuario = _usuarioRepository.Obter(usuarioDTO.Email, usuarioDTO.Senha);
                if (usuario != null)
                {
                    // retorna o  Token (JWT)
                    return GerarToken(usuario);
                }
                else
                {
                    return NotFound("Usuário não localizado!");
                }
            }
            else
            {
                return StatusCode(422, ModelState);
            }
        }

        [HttpPost("renovar")]
        public ActionResult Renovar([FromBody] TokenDTO tokenDTO)
        {
            var refreshTokenDB = _tokenRepository.Obter(tokenDTO.RefreshToken);

            if (refreshTokenDB == null)
                return NotFound();

            // RefreshToken antigo - Atualizar - Desativar esse refreshToken
            refreshTokenDB.Atualizado = DateTime.Now;
            refreshTokenDB.Utilizado = true;
            _tokenRepository.Atualizar(refreshTokenDB);

            // Gerar um novo token/Refresh Token - Salvar
            var usuario = _usuarioRepository.Obter(refreshTokenDB.UsuarioId);
            return GerarToken(usuario);
        }

        [HttpPost("")]
        public ActionResult Cadastrar([FromBody] UsuarioDTO usuarioDTO)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser usuario = new ApplicationUser();
                usuario.FullName = usuarioDTO.Nome;
                usuario.UserName = usuarioDTO.Email;
                usuario.Email = usuarioDTO.Email;

                var resultado = _userManager.CreateAsync(usuario, usuarioDTO.Senha).Result;
                if (!resultado.Succeeded)
                {
                    List<string> erros = new List<string>();
                    foreach (var erro in resultado.Errors)
                    {
                        erros.Add(erro.Description);
                    }
                    return StatusCode(422, erros);
                }
                else
                {
                    return Ok(usuario);
                }
            }
            else
            {
                return StatusCode(422, ModelState);
            }
        }

        private TokenDTO BuildToken(ApplicationUser usuario)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("chave-api-jwt-minhas-tarefas")); // Recomendo -> appSettings.json
            var sign = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var exp = DateTime.UtcNow.AddHours(1);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: exp,
                signingCredentials: sign
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            var refreshToken = Guid.NewGuid().ToString();
            var expRefreshToken = DateTime.UtcNow.AddHours(2);
            
            var tokenDTO = new TokenDTO { Token = tokenString, Expirtaion = exp, ExpirationRefreshToken = expRefreshToken, RefreshToken = refreshToken};
            return tokenDTO;
        }

        private ActionResult GerarToken(ApplicationUser usuario)
        {
            var token = BuildToken(usuario);

            // Salvar o Token no Banco
            var tokenModel = new Token()
            {
                RefreshToken = token.RefreshToken,
                ExpirtaionToken = token.Expirtaion,
                ExpirtaionRefreshToken = token.ExpirationRefreshToken,
                Usuario = usuario,
                Criado = DateTime.Now,
                Utilizado = false
            };

            _tokenRepository.Cadastrar(tokenModel);
            return Ok(token);
        }
    }
}
