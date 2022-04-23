using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OriginWorkBook = Newtonsoft.Json.Linq.JObject;    // 别名

namespace Originbot.Base
{
    public class DataInput
    {
        List<string>? _files;
        private Dictionary<string, byte[]?> _content;
        public Dictionary<string, byte[]?> Content { get { return _content; } }

        public DataInput()
        {
            // 默认文件
            _files = new List<string>();
            _content = new Dictionary<string, byte[]?>();
        }

        public DataInput(string file)
        {
            _files = new List<string>();
            _files.Add(file);
            _content = new Dictionary<string, byte[]?>();
        }

        public DataInput(List<string> files)
        {
            _files = files;
            _content = new Dictionary<string, byte[]?>();
        }

        private async Task<byte[]?> GetFileContentAsync(string filename)
        {
            if (_files == null) return null;
            if (!_files.Contains(filename))
            {
                return null;
            }
            var stream = File.OpenRead(filename);
            byte[] buffer = new byte[stream.Length];
            Console.WriteLine($"Reading {filename} ...");
            var flag = await stream.ReadAsync(buffer);
            stream.Close();
            Console.WriteLine("done");
            return buffer;
        }

        /// <summary>
        /// 获取所有文件的内容
        /// </summary>
        /// <returns>是否成功</returns>
        /// <exception cref="ArgumentNullException">参数错误</exception>
        public async ValueTask<bool> GetFileContentAsync()
        {
            if (_files==null)
            {
                Console.WriteLine("no file to read");
            }
            else
            {
                foreach (var filename in _files)
                {
                    _content[filename] = await GetFileContentAsync(filename);
                } 
            }
            return true;
        }
    }
}
