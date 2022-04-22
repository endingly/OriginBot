using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using originlab_command;
using Xunit.Abstractions;
using Xunit;
using System.Text.RegularExpressions;

namespace originlab_test
{
    public class Test_Parser
    {
        private readonly ITestOutputHelper _output;

        public Test_Parser(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Test_Parse()
        {
            var filePath = "D:\\Users\\codelib\\originlab\\originlab_test\\bin\\Debug\\net6.0\\电子密度_偏轴4.txt";
            DataInput inputManager = new DataInput(filePath);
            var flag = inputManager.GetFileContentAsync();
            var result = Parser.Parse(inputManager.Content);
            if (result == null)
                Assert.True(false);
            else
            {
                _output.WriteLine(flag.Result.ToString());
                _output.WriteLine(result.Count.ToString());
                Assert.True(true);
            }
        }

    }
}
