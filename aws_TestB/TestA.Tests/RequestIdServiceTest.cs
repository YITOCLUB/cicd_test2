using System;
using Common;
using Interface;
using Moq;
using Services;

namespace TestA.Tests
{
	public class RequestIdServiceTest
	{
		public RequestIdServiceTest()
		{
		}
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("abc")]
        public void TestGetRequestId(string prefix)
        {
            var targetMock = new Mock<Utils>();
            Assert.NotNull(new RequestIdService().GetRequestId(prefix));
        }
    }
}

