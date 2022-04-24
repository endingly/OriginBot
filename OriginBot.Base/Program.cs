// See https://aka.ms/new-console-template for more information
//Console.WriteLine("hello world");
string projectSavePath = "D:\\Users\\codelib\\OriginBot\\test.opju";
string settingsFilePath = "D:\\Users\\codelib\\OriginBot\\data.txt";
Originbot.Base.OriginProject origin =new Originbot.Base.OriginProject(projectSavePath);
origin.CreatWorkBookFromSettingsFile(settingsFilePath);
