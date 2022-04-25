using Xunit.Abstractions;
using Xunit;
using Originbot.Base;

namespace Originbot.Test
{
    public class Test_SettingsInput
    {
        private readonly ITestOutputHelper _output;

        public Test_SettingsInput(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Test_GetSettingsInfo()
        {
            string settingsFilePath = "D:\\Users\\codelib\\OriginBot\\data.txt";
            var result = SettingsInput.GetSettingsInfo(settingsFilePath);
            if (result != null)
                /*foreach (var item in result.Value.DataFilePath)
                {
                    _output.WriteLine(item);
                }*/
                _output.WriteLine(result.Value.Name);
        }
    }
}
