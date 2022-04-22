using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace originlab_command
{
    public static class Parser
    {
        /// <summary>
        /// 将容器中所有的 byte[] 变换为字符串
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        private static Dictionary<string, string>? ByteArrayToString(Dictionary<string, byte[]?> dic)
        {
            var result = new Dictionary<string, string>();
            foreach (var kvp in dic)
            {
                if(kvp.Value==null)
                    continue;
                result[kvp.Key]= System.Text.Encoding.Default.GetString(kvp.Value);
            }
            return result;
        }

        public static Dictionary<string, List<List<double>>>? Parse(Dictionary<string,byte[]?> dic)
        {
            if (dic == null)
                return null;
            var result = new Dictionary<string, List<List<double>>>();

            var ParseDic = ByteArrayToString(dic);

            ParseDic = Pretreat(ParseDic);

            foreach (var kvp in ParseDic)
            {
                var col1 = new List<double>();
                var col2 = new List<double>();
                // 根据分号拆分字符串为小字符段
                var s = kvp.Value.Split(';', StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in s)
                {
                    var ss = item.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    col1.Add(Convert.ToDouble(ss[0]));
                    col2.Add(Convert.ToDouble(ss[1]));
                }
                result[kvp.Key] = new List<List<double>> { col1, col2 };
            }
            return result;
        }




        /// <summary>
        /// 子函数
        /// 删除注释，将"\r\n"替换成";"
        /// 将多个空格替换成","
        /// </summary>
        /// <param name="strWords"></param>
        /// <returns></returns>
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
        /// 将所有容器内需要解析的字符串先进行预处理，
        /// 删除注释，将"\r\n"替换成";"
        /// 将多个空格替换成","
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        private static Dictionary<string, string> Pretreat(Dictionary<string, string> dic)
        {
            var result = new Dictionary<string, string>();
            foreach (var item in dic)
            {
                result[item.Key]= GetStrFields(item.Value);
            }
            return result;
        }
    }
}
