namespace Dto;

[GraphQLName("LoginResultSet")]
[GraphQLDescription("ログイン結果")]
public class LoginResponse : AbstractResponseBase
{
    [GraphQLName("token")]
    [GraphQLDescription("認証用トークン")]
    public string? token { get; set; }

}


