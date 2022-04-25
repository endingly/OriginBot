
using CommandLine;
using Originbot.Base;

namespace Originbot
{
    class Program
    {
        static void Main(string[] args)
        {

            var result = Parser.Default.ParseArguments<CommandlineOptions>(args).WithParsed(Run);
            
        }

        static void Run(CommandlineOptions options)
        {
            string projectSavePath = "D:\\Users\\codelib\\OriginBot\\test.opju";
            //string settingsFilePath = "D:\\Users\\codelib\\OriginBot\\data.txt";
            OriginProject origin = new OriginProject(projectSavePath);
            origin.CreatWorkBookFromSettingsFile(options.Filepath);
            origin.Exit();
        }
    }
}

