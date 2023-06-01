using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using API.NET.Context;
using API.NET.Models;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.NET.Controllers;

[ApiVersion("1.0", Deprecated = true)]
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
    private String query = "SELECT `IdUsuario`, `Nome`, `Senha` FROM `Usuario`";
    #endregion

    [AllowAnonymous]
    private List<Usuario> listaUsuarios()
    {

        List<Usuario> usuarios = new List<Usuario>();
        try
        {

            usuarios = _context.Database.GetDbConnection().Query<Usuario>(query).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine("Erro Buscar Usuraios" + e.InnerException);
        }

        return usuarios;

    }


    // POST: api/Token
    [AllowAnonymous]
    [HttpPost]
    public IActionResult RequestToken([FromBody] Usuario request)
    {
        var usuarios = listaUsuarios();

        foreach (var user in usuarios)
        {

            #region Token
            if (user.Nome == request.Nome && user.Senha == request.Senha)
            {
                //regras de claim
                var claims = new[] {
                    new Claim(ClaimTypes.Name, request.Nome)
                };
                #nullable disable
                //armazena a chave de token
                var key = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_configuration["SecurityKey"])
                    );
                #nullable enable
                //gera o token
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                        issuer: "API6",
                        audience: "API6",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: creds
                    );
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
                #endregion
            }
        }

        return Unauthorized("Credenciais Inválidas....");

    }
}