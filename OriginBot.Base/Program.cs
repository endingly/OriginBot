
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
            OriginProject origin = new OriginProject();
            if (options.Filespath != null)
            {
                origin.CreatDataGroupFromDirectory(options.Filespath);
                origin.Exit();
            }
            else
            {
                System.Console.Error.WriteLine("Maybe input path is nowork!");
            }
        }
    }
}

