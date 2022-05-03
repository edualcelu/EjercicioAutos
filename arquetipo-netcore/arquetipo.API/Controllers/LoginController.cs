using arquetipo.Entity.Dto;
using arquetipo.Entity.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace arquetipo.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<Respuesta> Post(Usuario oUsuario)
        {
            string strUsuario = "Eduardo";
            string strClave = "clave";
            if (oUsuario == null)
            {
                //return NotFound(ErrorHelper.Response(404, "Usuario no encontrado."));
            }
            Respuesta respuesta = new Respuesta();
            try
            {
                if (oUsuario.Login == strUsuario && oUsuario.Password == strClave)
                {
                    var secretKey = "asdfghjkl@.9133alñjas.dsf.r.33";
                    var key = Encoding.ASCII.GetBytes(secretKey);

                    var claims = new ClaimsIdentity();
                    claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, oUsuario.Login));

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = claims,
                        Expires = DateTime.UtcNow.AddHours(4),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var createdToken = tokenHandler.CreateToken(tokenDescriptor);

                    string bearer_token = tokenHandler.WriteToken(createdToken);
                    //return Ok(bearer_token);
                    respuesta.Message = bearer_token;
                    respuesta.IsSuccess = true;
                }
                
            }
            catch (Exception e)
            {
                respuesta.Message = "Ocurrio un error: " + e.StackTrace;
            }
            return respuesta;
            
        }
    }
}
