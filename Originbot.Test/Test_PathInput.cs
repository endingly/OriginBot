using Xunit.Abstractions;
using Xunit;
using Originbot.Base;

namespace Originbot.Test
{
    public class Test_PathInputInput
    {
        private readonly ITestOutputHelper _output;

        public Test_PathInputInput(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Test_GetDirectoryInfo()
        {
            string Path = "D:\\用户\\2020-2023\\研究生涯\\实验数据\\微空心阴极放电模拟\\[2022.5.3][400 0 -200][场近似][重新跑][时间加长]\\数据分析\\三针";
            var result = PathInput.GetInputFilesInfo(Path);
            foreach (var file in result)
            {
                foreach (var item in file.Files)
                {
                    _output.WriteLine(item.Value);
                }
            }
        }
    }
}