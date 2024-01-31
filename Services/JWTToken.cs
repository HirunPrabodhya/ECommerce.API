using Microsoft.IdentityModel.Tokens;
using Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class JWTToken
    {
        public static string CreateJWT(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryveryscreate...");
            var payload = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name,user.FirstName),
                        new Claim(ClaimTypes.Role,user.Role),
                        new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
                    }
                );
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = payload,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);


            return jwtTokenHandler.WriteToken(token);
        }
    }
}
