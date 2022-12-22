using System;
using Queries;
using Interface;
using Services;
using Moq;

namespace TestA.Tests
{
	public class QueryTest
	{
        [Fact]
        public void TestGetRequestId()
        {
            var service = new RequestIdService();
            var targetMock = new Mock<IRequestIdService>();

            targetMock.Setup(o => o.GetRequestId("abc")).Returns("ABC");
            var objQuery = new Query();
            string result = objQuery.GetRequestId(targetMock.Object, "abc");
            Assert.Equal("ABC", result);
        }
    }
}

