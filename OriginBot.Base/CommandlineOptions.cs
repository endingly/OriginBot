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
        [Option('d', "directory", Required = true, HelpText = "Path of input files.")]
        public string? Filespath { get; set; }
    }
}
