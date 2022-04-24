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
            string file = "D:\\�û�\\2020-2023\\�о�����\\ʵ������\\΢���������ŵ�ģ��\\[2022.4.1] �綴��Ŀ���� [9.5mm] [�п��]\\���ݷ���\\ƽ��\\�����ܶ�\\�����ܶ�_ƫ��1.txt";
            var result = await DataInput.GetSingleFileContentAsync(file);
            foreach (var item in result[0])
            {
                _output.WriteLine(System.Convert.ToString(item));
            }
        }
    }
}