using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Originbot.Base
{
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
    }
}
