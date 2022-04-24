using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;



namespace Originbot.Base
{
    public static class DataInput
    {

        private static async Task<byte[]> GetByteFromSingleFileAsync(string filename)
        {
            var stream = File.OpenRead(filename);
            byte[] buffer = new byte[stream.Length];
            Console.WriteLine($"Reading {filename} ...");
            _ = await stream.ReadAsync(buffer);
            stream.Close();
            Console.WriteLine("done");
            return buffer;
        }

        /// <summary>
        /// 唯一被接口调用的函数
        /// </summary>
        /// <param name="parseString">预处理之前的数据</param>
        /// <returns>最终解析之后的两列数据</returns>
        private static List<List<double>> Parse(string parseString)
        {
            var ParseDic = GetStrFields(parseString);

            var col1 = new List<double>();
            var col2 = new List<double>();
            // 根据分号拆分字符串为小字符段
            var s = ParseDic.Split(';', StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in s)
            {
                var ss = item.Split(',', StringSplitOptions.RemoveEmptyEntries);
                col1.Add(Convert.ToDouble(ss[0]));
                col2.Add(Convert.ToDouble(ss[1]));
            }
            return new List<List<double>> { col1, col2 };
        }

        /// <summary>
        /// 子函数
        /// 删除注释，将"\r\n"替换成";"
        /// 将多个空格替换成","
        /// </summary>
        /// <param name="strWords"></param>
        /// <returns>裸数据</returns>
        private static string GetStrFields(string strWords)
        {
            var LastRemoveIndex = strWords.LastIndexOf('%');
            LastRemoveIndex = strWords.IndexOf("\r\n", LastRemoveIndex);
            // 去除前面的注释
            var result = strWords.Remove(0, LastRemoveIndex + 2);
            result = result.Replace("\r\n", ";");
            return new Regex("[\\s]+").Replace(result, ",");
        }

        /// <summary>
        /// 获取文件的内容
        /// </summary>
        /// <returns></returns>
        public static async Task<List<List<double>>> GetSingleFileContentAsync(string dataFilePath)
        {
            var buf = await GetByteFromSingleFileAsync(dataFilePath);
            return Parse(Encoding.UTF8.GetString(buf));
        }
    }
}
