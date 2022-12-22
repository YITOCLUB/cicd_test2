
using System.Collections;
using System.Linq.Expressions;

namespace Dto
{
    public class ResponseDtoBase
    {

        [GraphQLName("is_error")]
        [GraphQLDescription("エラー有無")]
        public bool IsError { get; set; } = false;

        [GraphQLName("error_code")]
        [GraphQLDescription("エラーコード")]
        public string? ErrorCode { get; set; } = string.Empty;

        [GraphQLName("error_message")]
        [GraphQLDescription("エラーメッセージ")]
        public string? ErrorMessage { get; set; } = string.Empty;


    }

}
