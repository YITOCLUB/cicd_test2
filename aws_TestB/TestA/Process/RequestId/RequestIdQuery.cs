using Common;
using Interface;
using Services;


namespace Queries;

public partial class Query
{
    [GraphQLName("request_id")]
    [GraphQLDescription("uuid取得調査用 C#のGUIDを使用")]
    public string GetRequestId(
        [Service] IRequestIdService service,
        string? prefix
        )
    {
        //##Utils.DebOut($"--■GetRequestId--{prefix}");
        //##return Utils.GetRequestId($"##1201001##---{prefix}");
        return service.GetRequestId(prefix);
    }
}
