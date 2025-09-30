using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MinhasTarefasAPI.Models;
using MinhasTarefasAPI.Repositories.Contracts;
using System.Collections.Generic;
using System.Text;

namespace MinhasTarefasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsuarioController(
            IUsuarioRepository usuarioRepository,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _usuarioRepository = usuarioRepository;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public ActionResult Login([FromBody] UsuarioDTO usuarioDTO)
        {
            ModelState.Remove("ConfirmacaoSenha");
            ModelState.Remove("Nome");

            if (ModelState.IsValid)
            {
                ApplicationUser usuario = _usuarioRepository.Obter(usuarioDTO.Email, usuarioDTO.Senha);
                if (usuario != null)
                {
                    // Login Identity
                    _signInManager.SignInAsync(usuario, false);

                    // retorna o  Token (JWT)
                    return Ok();
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

        public ActionResult Cadastrar([FromBody] UsuarioDTO usuarioDTO)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser usuario = new ApplicationUser();
                usuario.FullName = usuarioDTO.Nome;
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
    }
}
