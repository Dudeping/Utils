using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Codeping.Utils;

namespace Utils.Tests
{
    public class ResultTest
    {
        [Fact]
        public void Result()
        {
            var result = new Result();
            Assert.False(result.Succeeded);

            result.Ok();
            Assert.True(result.Succeeded);
            Assert.Null(result.Exception);
            Assert.Null(result.ErrorMessage);

            result.Fail("error");
            Assert.False(result.Succeeded);
            Assert.Null(result.Exception);
            Assert.Equal("error", result.ErrorMessage);

            var ex = new Exception("error");
            result.Fail(ex);
            Assert.False(result.Succeeded);
            Assert.NotNull(result.Exception);
            Assert.Equal("error", result.ErrorMessage);
            Assert.Equal("error", result.Exception.Message);
        }

        [Fact]
        public void ResultGenericTest()
        {
            var result = new Result<string>();
            Assert.False(result.Succeeded);

            result.Ok();
            Assert.True(result.Succeeded);
            Assert.Null(result.Exception);
            Assert.Null(result.ErrorMessage);
            Assert.Null(result.Value);

            result.Ok("ok!");
            Assert.True(result.Succeeded);
            Assert.Null(result.Exception);
            Assert.Null(result.ErrorMessage);
            Assert.Equal("ok!", result.Value);

            result.Fail("error");
            Assert.False(result.Succeeded);
            Assert.Null(result.Value);
            Assert.Null(result.Exception);
            Assert.Equal("error", result.ErrorMessage);

            var ex = new Exception("error");
            result.Fail(ex);
            Assert.False(result.Succeeded);
            Assert.Null(result.Value);
            Assert.NotNull(result.Exception);
            Assert.Equal("error", result.ErrorMessage);
            Assert.Equal("error", result.Exception.Message);
        }

        [Fact]
        public void MargeTest()
        {
            var result = new Result();
            result.Ok();

            var merged = new Result();
            merged.Merge(result);

            Assert.True(result.Succeeded);
            Assert.True(merged.Succeeded);

            result.Fail("error");
            merged.Merge(result);

            Assert.False(result.Succeeded);
            Assert.False(merged.Succeeded);
            Assert.Equal("error", result.ErrorMessage);
            Assert.Equal(result.ErrorMessage, merged.ErrorMessage);
        }

        [Fact]
        public void MargeGenericTest()
        {
            var result = new Result<int>();
            result.Ok(1);

            var merged = new Result<int>();
            merged.Merge(result);

            Assert.True(result.Succeeded);
            Assert.True(merged.Succeeded);
            Assert.Equal(1, result.Value);
            Assert.Equal(result.Value, merged.Value);

            result.Fail("error");
            merged.Merge(result);

            Assert.False(result.Succeeded);
            Assert.False(merged.Succeeded);
            Assert.Equal("error", result.ErrorMessage);
            Assert.Equal(result.ErrorMessage, merged.ErrorMessage);
        }
    }
}
