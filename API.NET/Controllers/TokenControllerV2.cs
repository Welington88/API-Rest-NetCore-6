using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.NET.Context;
using API.NET.Models;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.NET.Controllers.V2;

[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[AllowAnonymous]
public class TokenController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly DataContext _context;

    public TokenController(IConfiguration configuration, DataContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    #region Query
    private string query = @"SELECT `IdUsuario`, `Nome` FROM `Usuario`";
    private string queryUser = @"SELECT `IdUsuario`, `Nome`, `Role` FROM `Usuario` WHERE `Nome` = @Name and `Senha`= @Password ;";
    #endregion



    [AllowAnonymous]
    private Usuario GetUser(string name, string password)
    {
        var parameters = new { 
            Name = name,
            Password = password
        };

        try
        {
            var usuario = new Usuario();
            var command = new CommandDefinition(queryUser, parameters);
            usuario = _context.Database.GetDbConnection().Query<Usuario?>(command).ToList().FirstOrDefault();

            if (usuario != null)
                return usuario;
        }
        catch (Exception e)
        {
            throw new Exception("Erro Buscar Usuarios" + e.InnerException);
        }

        throw new Exception("Usuário Não Encontrado ou Não Cadastrado na Base de dados");

    }

    #region List User
    [AllowAnonymous]
    private List<Usuario> GetUser()
    {

        List<Usuario> usuarios = new List<Usuario>();
        try
        {

            usuarios = _context.Database.GetDbConnection().Query<Usuario>(query).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine("Erro Buscar Usuarios" + e.InnerException);
        }

        return usuarios;

    }
    #endregion

    // POST: api/Token
    [AllowAnonymous]
    [HttpPost]
    public IActionResult RequestTokenAsync([FromBody] Usuario request)
    {
        try
        {
           
            var usuario = GetUser(request.Nome, request.Senha);

            #region Token
            if (usuario.Nome == request.Nome && usuario.Role == request.Role)
            {
                //regras de claim
                var claims = new ClaimsIdentity(new Claim[]{
                new Claim(ClaimTypes.Name, request.Nome),
                new Claim(ClaimTypes.Role, request.Role)
            });

            #nullable disable
                //armazena a chave de token
                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["SecurityKey"])
                );
            #nullable enable
            //gera o token
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    issuer: "API",
                    audience: "API",
                    claims: claims.Claims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: creds
                );
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
            #endregion

            }

            return Unauthorized("Credenciais Inválidas....");
        }
        catch (Exception)
        {

            return Unauthorized("Credenciais Inválidas....");
        }
    }
}