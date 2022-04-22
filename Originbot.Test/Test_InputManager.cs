using Xunit;
using originlab_command;
using Xunit.Abstractions;

namespace originlab_test
{
    public class Test_InputManager
    {
        private readonly ITestOutputHelper _output;

        public Test_InputManager(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Test_GetFileContentAsync()
        {
            var filePath = "D:\\Users\\codelib\\originlab\\originlab_test\\bin\\Debug\\net6.0\\µç×ÓÃÜ¶È_Æ«Öá4.txt";
            DataInput inputManager = new DataInput(filePath);
            var flag = inputManager.GetFileContentAsync();
            _output.WriteLine(flag.Result.ToString());
            _output.WriteLine(inputManager.Content[filePath].Length.ToString());
        }
    }
}