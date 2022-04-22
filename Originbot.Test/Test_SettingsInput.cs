using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit;
using Originbot.Base;

namespace Originbot.Test
{
    using OriginWorkBook = Newtonsoft.Json.Linq.JObject;

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
            string settingsFilePath = "D:\\Users\\codelib\\OriginBot\\path.json";
            var result = SettingsInput.GetSettingsInfo(settingsFilePath).Result;
            _output.WriteLine(result.First.First.First.ToString());

        }
    }
}
