using Xunit;
using Originbot.Base;
using Xunit.Abstractions;

namespace Originbot.Test
{
    public class Test_DataInput
    {
        private readonly ITestOutputHelper _output;

        public Test_DataInput(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async void Test_GetSingleFileContentAsync()
        {
            string file = "D:\\用户\\2020-2023\\研究生涯\\实验数据\\微空心阴极放电模拟\\[2022.4.1] 风洞项目经验 [9.5mm] [托宽后方]\\数据分析\\平板\\电子密度\\电子密度_偏轴1.txt";
            var result = await DataInput.GetSingleFileContentAsync(file);
            foreach (var item in result[0])
            {
                _output.WriteLine(System.Convert.ToString(item));
            }
        }
    }
}