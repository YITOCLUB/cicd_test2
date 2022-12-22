using HotChocolate.Language;
using System.Reflection.Metadata;
namespace Common;


public class YayoiBigInteger : ScalarType<long, StringValueNode>
{
    public YayoiBigInteger() : base("BigInteger")
    {
    }

    public override IValueNode ParseResult(object? resultValue) => ParseValue(resultValue);

    protected override long ParseLiteral(StringValueNode valueSyntax) => long.Parse(valueSyntax.Value); 

    protected override StringValueNode ParseValue(long runtimeValue) => new StringValueNode($"{runtimeValue}");
}
