using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Originbot.Base
{
    /// <summary>
    /// 文件夹抽象
    /// 一般情况下，只有一层文件夹，底下都是文件
    /// </summary>
    public struct Folder
    {
        public string Name;
        public List<Tuple<string, string, string>> unit;
        public List<KeyValuePair<string,string>> Files;
        

        public Folder()
        {
            Name = string.Empty;
            unit = new List<Tuple<string, string, string>>();
            Files = new List<KeyValuePair<string, string>>();
        }
    }


    public static class PathInput
    {
        /// <summary>
        /// 从指定的路径下，拿到所有路径下的所有数据文件
        /// </summary>
        /// <param name="Path">指定的路径</param>
        /// <returns></returns>
        public static List<Folder> GetInputFilesInfo(string Path)
        {
            DirectoryInfo DirInfo = new DirectoryInfo(Path);
            
            // 拿到目标路径下的所有文件夹
            var fileDirectorys = DirInfo.GetDirectories();
            List<Folder> result = new List<Folder>();
            
            foreach (var f in fileDirectorys)
            {
                var fs = f.EnumerateFiles().Where(f => f.FullName.Contains(".txt"));
                if (!fs.Any())
                {
                    continue;
                }
                Folder d = new Folder();
                // 文件夹的名字即是 workbook 的名字
                d.Name = f.Name;
                // 将数据集依次存储
                foreach (var f2 in fs)
                {
                    if (f2.FullName.Contains("setting"))
                    {
                        d.unit = SettingsInput.ParseSettings(f2.FullName);
                        continue;
                    }
                    d.Files.Add(new KeyValuePair<string, string>(f2.Name, f2.FullName));
                }
                result.Add(d);
            }
            return result;
        }
    }
}
