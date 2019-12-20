using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

using MarketWatchdogLambda;

namespace MarketWatchdogLambda.Tests
{
    public class FunctionTest
    {
        [Fact]
        public async Task TestToUpperFunction()
        {

            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();
            await function.FunctionHandler("Si", context);

            //Assert.Equal("HELLO WORLD", upperCase);
        }
    }
}
