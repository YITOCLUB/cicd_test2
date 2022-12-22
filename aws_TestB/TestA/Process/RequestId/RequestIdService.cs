using Common;
using Interface;

namespace Services
{
	public class RequestIdService: IRequestIdService
    {
		public RequestIdService()
		{

        }
        public string GetRequestId(string? prefix)
        {
            Utils.DebOut($"--RequestIdService:GetRequestId--{prefix}");
            return Utils.GetRequestId($"##1201001##---{prefix}");
        }
	}
}

