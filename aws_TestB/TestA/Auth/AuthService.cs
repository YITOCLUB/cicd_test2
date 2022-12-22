
//##using Models;
using Dto;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Common;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.DataProtection;
using System.Security.Cryptography;

namespace Services;

public class AuthService
{
    [Obsolete]
    public string GenerateToken(string id, string password)
    {

        var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, id)
            };

        //var key = new SymmetricSecurityKey(AuthUtils.symmetricSecurityKeyBase);
        //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var key = new SymmetricSecurityKey(AuthUtils.symmetricSecurityKeyBase);
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);


        var token = new JwtSecurityToken(
                issuer: AuthUtils.validIssuer,
                audience: AuthUtils.validAudience,


                claims: claims,

                // 有効期限7日(仮)
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds
            );


        var resutlToken = new JwtSecurityTokenHandler().WriteToken(token);

        return resutlToken;

    }
}
