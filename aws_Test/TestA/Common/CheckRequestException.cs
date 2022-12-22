namespace Common;

public class CheckRequestException : Exception
{
    public CheckRequestException()
    {
    }

    public CheckRequestException(string message) : base(message)
    {
    }

    public CheckRequestException(string message, Exception inner) : base(message, inner)
    {
    }

}
