using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;

namespace Originbot.Base
{
    using OriginWorkBook = Newtonsoft.Json.Linq.JObject;    // 别名

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
        public static async Task<OriginWorkBook?> GetSettingsInfo(string settingsFilePath)
        {
            var filestream = File.OpenRead(settingsFilePath);
            byte[] infos = new byte[filestream.Length];
            _ = await filestream.ReadAsync(infos);

            var parseString = Encoding.UTF8.GetString(infos);

            if (parseString == null)
                return null;
            OriginWorkBook result = JsonConvert.DeserializeObject<OriginWorkBook>(parseString);

            if (result != null)
                return result;
            return null;
        }
    }
}
