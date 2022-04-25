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
            string settingsFilePath = "D:\\Users\\codelib\\OriginBot\\example_settings.txt";
            string projectPath = "";
            var result = SettingsInput.GetSettingsInfo(settingsFilePath, ref projectPath);
            if (result != null)
            {
                _output.WriteLine(result[0].Name);
                _output.WriteLine(result[1].Name);
            }
                
        }
    }
}
