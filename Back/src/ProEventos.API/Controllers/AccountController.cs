using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProEventos.Application.Dtos;
using ProEventos.Domain.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ProEventos.Application.Contratos;
using ProEventos.API.Extensions;

namespace ProEventos.API.Controllers
{
    [Authorize]
    [Route("api/[Controller]")]
    [ApiController]// O atributo ApiController permite acionar automaticamente erros de validação para uma reposta HTTP 400. O envio de uma requisição com dados inválidos trará como retorno um erro do tipo 400.
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountService accountService,
                                ITokenService tokenService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }

        [HttpGet("GetUser")]
        //[AllowAnonymous]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var userName = User.GetUserName();//Esse "User" é um objeto da classe "ClaimsPrincipal" que pertence a classe nativa "ControllerBase". Dessa forma é possível criar métodos de extensão.
                var user = await _accountService.getUserByUserNameAsync(userName);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar Usuário. Erro: {ex.Message}");
            }
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            try
            {
                if (await _accountService.UserExists(userDto.UserName))
                    return BadRequest("Usuário já cadastrado");

                var user = await _accountService.createAccountAsync(userDto);
                if (user != null)
                    return Ok(new {
                        userName = user.UserName,
                        PrimeiroNome = user.PrimeiroNome,
                        TokenContext = _tokenService.CreateToken(user).Result
                    });

                return BadRequest("Usuário não cadastrado. Erro desconhecido");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao registrar usuário. Erro: {ex.Message}");
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto userLogin)
        {
            try
            {
                var user = await _accountService.getUserByUserNameAsync(userLogin.UserName);
                if (user == null) return Unauthorized("Usuário ou senha está incorreta.");

                var result = await _accountService.CheckUserPasswordAsync(user, userLogin.Password);
                if (!result.Succeeded) return Unauthorized();

                return Ok(new
                {
                    userName = user.UserName,
                    PrimeiroNome = user.PrimeiroNome,
                    TokenContext = _tokenService.CreateToken(user).Result
                });
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar realizar login. Erro: {ex.Message}");
            }
        }

        [HttpPut("UpdateUser")]        
        public async Task<IActionResult> UpdateUser(UserUpdateDto userUpdateDto)
        {
            try
            {
                var user = await _accountService.getUserByUserNameAsync(User.GetUserName());//Esse "User" é um objeto da classe "ClaimsPrincipal" que pertence a classe nativa "ControllerBase". Dessa forma é possível criar métodos de extensão.
                if (user == null) return Unauthorized("Usuário inválido.");

                var userReturn = _accountService.UpdateAccountAsync(userUpdateDto);
                if (userReturn == null) return NoContent();

                return Ok(userReturn);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,$"Erro ao tentar atualizar Usuário. Erro: {ex.Message}");
            }
        }
    }
}
