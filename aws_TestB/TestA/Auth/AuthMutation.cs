
using Dto;
using Services;
//##using Models;
using Microsoft.EntityFrameworkCore;
using Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mutations;


public partial class Mutation
{

    [GraphQLName("dummy_login")]
    [GraphQLDescription("ダミーログイン（JWT認証認可調査用）")]
    public LoginResponse Login(
        [Service] AuthService service,

        [GraphQLName("id")]
        [GraphQLDescription("ユーザID(ダミー)")]
        //[GraphQLNonNullType]
        string? Id,

        [GraphQLName("password")]
        [GraphQLDescription("パスワード(ダミー)")]
        //[GraphQLNonNullType]
        string? Password
    )
    {
        return new LoginResponse() { ErrorMessage = "OK:Lonin", token = service.GenerateToken(Id,Password) };
    }

}
