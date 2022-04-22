using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace originlab_command
{



    public class SettingsInput
    {

        public async void GetSettingsInfo(string settingsFilePath)
        {
            var filestream = File.OpenRead(settingsFilePath);
            byte[] infos = new byte[filestream.Length];
            _ = await filestream.ReadAsync(infos);

            var parseString = Encoding.UTF8.GetString(infos);

            var result = JsonConvert.DeserializeObject<Dictionary<string, List<KeyValuePair<string, string>>>>(parseString);

            

        }
    }
}
