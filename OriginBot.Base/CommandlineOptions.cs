using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

namespace Originbot.Base
{
    public class CommandlineOptions
    {
        [Option('f', "file", Required = false, HelpText = "Input settings file path.")]
        public string? Filepath { get; set; }
    }
}
