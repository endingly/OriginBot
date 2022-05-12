using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Originbot.Base
{
    [Obsolete]
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
        static public List<Tuple<string, string, string>> ParseSettings(string path)
        {
            var stream = File.OpenRead(path);
            byte[] buffer = new byte[stream.Length];
            Console.WriteLine($"Reading {path} ...");
            _ = stream.Read(buffer);
            stream.Close();
            Console.WriteLine("done");
            var str = Encoding.UTF8.GetString(buffer);

            var Result = new List<Tuple<string, string, string>>();
            var re = str.Split("\r\n");
            foreach (var item in re)
            {
                var r = item.Split("-");
                Result.Add(new Tuple<string, string, string>(r[0], r[1], r[2]));
            }
            return Result;
        } 
        #region 弃用
        /// <summary>
        /// 读取设置文件
        /// </summary>
        /// <param name="settingsFilePath"></param>
        /// <returns></returns>
        [Obsolete]
        public static List<OriginWorkBook>? GetSettingsInfo(string settingsFilePath, ref string ProjectSavePath)
        {
            // 读取设置文件
            var filestream = File.OpenRead(settingsFilePath);
            byte[] infos = new byte[filestream.Length];
            _ = filestream.Read(infos);
            // 将字节流读取为字符串
            var parseString = Encoding.UTF8.GetString(infos);
            if (parseString == null)
                return null;
            var result = ParseStringToOriginWorkBookList(parseString, ref ProjectSavePath);
            return result;
        }

        [Obsolete]
        private static OriginWorkBook ParseStringToOriginWorkBook(string str)
        {
            // 初始化结构
            var result = new OriginWorkBook();
            //var ParsePtr = 0;  // 解析指针
            
            // 提取工作簿名字
            result.Name = str.Substring(str.IndexOf('{') + 1,
                                        str.IndexOf('}') - str.IndexOf('{') - 1);
            // 提取单位以及单位描述
            var unitD = str.Substring(str.IndexOf('(') + 1, 
                                      str.IndexOf(')') - str.IndexOf('(') - 1);
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

        [Obsolete]
        /// <summary>
        /// 将多个工作簿解析
        /// </summary>
        /// <param name="str">设置文件中的内容</param>
        /// <param name="ProjectSavePath">将放入工程的存储路径</param>
        /// <returns></returns>
        private static List<OriginWorkBook> ParseStringToOriginWorkBookList(string str, ref string ProjectSavePath)
        {
            // 初始化结构
            var result = new List<OriginWorkBook>();
            //var ParsePtr = 0;  // 解析指针
            // 提取存储路径
            ProjectSavePath = str.Substring(str.IndexOf('[') + 1,
                                            str.IndexOf(']') - str.IndexOf('[') - 1);
            // 根据 txt 文件的特征，将文件内容拆分成结构相同的几段分别解析
            var parStrings = str.Split('{').ToList();
            // 删除存储路径字符
            parStrings.Remove(parStrings[0]);

            foreach (var par in parStrings)
            {
                result.Add(ParseStringToOriginWorkBook(par.Insert(0, "{")));
            }
            return result;
        }
        #endregion
    }
}
