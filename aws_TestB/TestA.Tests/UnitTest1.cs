using Common;
#if CMT
//1205
rm -f -r ./utestresults ./coveragereport
dotnet test ./TestA.Tests/TestA.Tests.csproj --logger trx --results-directory ./utestresults  --collect:"XPlat Code Coverage"
reportgenerator -reports:"./utestresults/*/coverage.cobertura.xml" -targetdir:"./coveragereport" -reporttypes:Html

#endif
namespace sample161.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            string result = Utils.GetRequestId("");
            Assert.True(!string.IsNullOrEmpty(result), "NG");
        }
        [Fact]
        public void Test2()
        {
            string result = Utils.GetRequestId("abc");
            Assert.NotNull(result);
        }
        [Fact]
        public void Test3()
        {
            string result = Utils.GetRequestId("012");
            Assert.IsType<string>(result);
        }
        [Theory]
        [InlineData("aaa_", "aaa_")]
        [InlineData("0123_", "0123_")]
        [InlineData("__", "__")]
        [InlineData("666", "666")]
        public void Test4(string sAns,string sP1)
        {
            bool result = Utils.GetRequestId(sP1).StartsWith(sAns);
            Assert.True(result);
        }
    }
}