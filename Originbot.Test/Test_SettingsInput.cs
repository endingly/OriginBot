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
            string settingsFilePath = "D:/用户/2020-2023/研究生涯/实验数据/微空心阴极放电模拟/[2022.5.3][400 0 -200][场近似][重新跑][时间加长]/数据分析/三针/不同时刻的中轴电子密度变化/settings.txt";
            //string projectPath = "";
            var result = SettingsInput.ParseSettings(settingsFilePath);
            if (result != null)
            {
                _output.WriteLine(result[0].Item2);
                _output.WriteLine(result[1].Item2);
            }
                
        }
    }
}
