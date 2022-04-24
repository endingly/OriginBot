﻿using Xunit;
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
            string projectSavePath = "D:\\Users\\codelib\\OriginBot\\test.opj";
            string settingsFilePath = "D:\\Users\\codelib\\OriginBot\\data.txt";
            OriginProject project = new OriginProject(projectSavePath);
            project.CreatWorkBookFromSettingsFile(settingsFilePath);
            Assert.True(true);
        }
    }
}
