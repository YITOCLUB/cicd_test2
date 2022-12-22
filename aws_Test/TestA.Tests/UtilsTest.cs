using System;
using HotChocolate;
using Microsoft.AspNetCore.Http;
using Serilog;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Text;
using Common;

namespace TestA.Tests
{
    public class UtilsTest
    {
        [Fact]
        public void Test_getDebStr()
        {
            Assert.NotNull(Utils.getDebStr(new object()));
        }

        public static bool isDebOut { get; set; } = true;

        [Fact]
        public void Test_DebOut_01()
        {
            Utils.isDebOut = !Utils.isDebOut;
            Utils.DebOut("");
            Utils.isDebOut = !Utils.isDebOut;
            Assert.True(true);

        }
        [Theory]
        //[InlineData(null)]
        [InlineData("")]
        [InlineData("abc")]
        [InlineData(123)]
        [InlineData(345,"def")]
        [InlineData("", "",789)]
        [InlineData(345, "hij",null,012)]
        public void Test_DebOut_02(params object[] args)
        {
            Utils.DebOut(args);
            Assert.True(true);
        }
        [Fact]
        public void Test_DebOut_03()
        {
            Utils.DebOut("1234");
            Assert.True(true);
        }
        [Fact]
        public void Test_DebOut_04()
        {
            Utils.DebOut("5678",null);
            Assert.True(true);
        }
        [Fact]
        public void Test_DebOut_045()
        {
            Utils.DebOut("90abc", new Exception());
            Assert.True(true);
        }

    }

}

