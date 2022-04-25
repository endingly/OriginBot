using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Originbot.Base
{
    public struct OriginWorkBook
    {
        public string Name;
        public List<KeyValuePair<string, string>> ColUnit;
        public List<string> DataFilePath;

        public OriginWorkBook()
        {
            Name = "";
            ColUnit = new List<KeyValuePair<string, string>>();
            DataFilePath = new List<string>();
        }
    }

    /// <summary>
    /// 解析用户指定的配置文件
    /// </summary>
    public static class SettingsInput
    {
        /// <summary>
        /// 异步读取设置文件
        /// </summary>
        /// <param name="settingsFilePath"></param>
        /// <returns></returns>
        public static OriginWorkBook? GetSettingsInfo(string settingsFilePath)
        {
            // 读取设置文件
            var filestream = File.OpenRead(settingsFilePath);
            byte[] infos = new byte[filestream.Length];
            _ = filestream.Read(infos);
            // 将字节流读取为字符串
            var parseString = Encoding.UTF8.GetString(infos);
            if (parseString == null)
                return null;
            var result = ParseStringToOriginWorkBook(parseString);
            return result;
        }

        private static OriginWorkBook ParseStringToOriginWorkBook(string str)
        {
            // 初始化结构
            var result = new OriginWorkBook();

            // 提取名字
            result.Name = str.Substring(str.IndexOf('[') + 1, str.IndexOf(']') - 1);
            // 提取单位以及单位描述
            var unitD = str.Substring(str.IndexOf('(') + 1, str.IndexOf(')') - str.IndexOf('(') - 1);
            foreach (var pair in unitD.Split(';'))
            {
                var x = pair.Split(',');
                result.ColUnit.Add(new KeyValuePair<string, string>(x[0], x[1]));
            }
            // 提取文件路径描述
            // 找到最后一个 )
            int index = str.IndexOf(')') + 3;
            var paths = str.Substring(index).Split("\r\n");
            result.DataFilePath = paths.ToList();
            return result;
        }
    }
}
