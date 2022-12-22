
using System.Collections;
using System.Linq.Expressions;

namespace Dto
{
    public class ResponseDtoBase
    {

        [GraphQLName("is_error")]
        [GraphQLDescription("�G���[�L��")]
        public bool IsError { get; set; } = false;

        [GraphQLName("error_code")]
        [GraphQLDescription("�G���[�R�[�h")]
        public string? ErrorCode { get; set; } = string.Empty;

        [GraphQLName("error_message")]
        [GraphQLDescription("�G���[���b�Z�[�W")]
        public string? ErrorMessage { get; set; } = string.Empty;


    }

}
