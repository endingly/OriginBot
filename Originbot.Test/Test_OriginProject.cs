using Xunit;
using Originbot.Base;
using Xunit.Abstractions;

namespace Originbot.Test
{
    public class Test_OriginProject
    {
        private readonly ITestOutputHelper _output;

        public Test_OriginProject(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Test_CreatDataGroup()
        {
            //string projectSavePath = "D:\\Users\\codelib\\OriginBot\\test.opj";
            string settingsFilePath = "D:\\Users\\codelib\\OriginBot\\example_settings.txt";
            OriginProject project = new OriginProject();
            
            project.CreatDataGroupFromSettingsFile(settingsFilePath);
            project.Exit();
            Assert.True(true);
        }

        [Fact]
        public void Test_CreatDataGroupV2()
        {
            //string projectSavePath = "D:\\\\Users\\\\codelib\\\\OriginBot\\\\test.opj";
            string FilePath = "D:\\用户\\2020-2023\\研究生涯\\实验数据\\微空心阴极放电模拟\\[2022.5.3][400 0 -200][场近似][重新跑][时间加长]\\数据分析\\三针";
            OriginProject project = new OriginProject();

            project.CreatDataGroupFromDirectory(FilePath);
            project.Exit();
            Assert.True(true);
        }
    }
}
