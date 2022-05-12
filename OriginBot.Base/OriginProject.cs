using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Origin;

namespace Originbot.Base
{
    public class OriginProject
    {
        Origin.Application _org;
        WorksheetPage? _orgWorkBook;     // 当前工作簿句柄
        string _projectPath;

        /// <summary>
        /// 默认构造函数，新建一个 origin 工程，并由此类维护
        /// </summary>
        public OriginProject()
        {
            _org = new Origin.Application();
            if (_org == null)
            {
                Console.Error.WriteLine("Origin could not be started. Check that your installation and project references are correct.");
            }
            _projectPath = "";
            // 新建工程
            _org.NewProject();
            _orgWorkBook = null;
        }

        public void Exit()
        {
            _org.Exit();
        }

        public string ProjectPath
        {
            get
            {
                return _projectPath;
            }
            set
            {
                _projectPath = value;   
            }
        }

        private void Save()
        {
            // Save:
            if (_org.Save(_projectPath) == false)
            {
                Console.Error.WriteLine("Failed to save the project into " + _projectPath);
            }
            else
            {
                Console.WriteLine("Saved into " + _projectPath);
            }
        }

        private WorksheetPage AddWorkBook()
        {
            return _org.WorksheetPages.Add(System.Type.Missing, System.Type.Missing);
        }

        private void CreateWorkBook(Folder settingsInfo)
        {
            // 新增一个工作簿
            _orgWorkBook = AddWorkBook();

            // 设置工作簿名称
            _orgWorkBook.LongName = settingsInfo.Name;
            // 添加数据
            for (int i = 0; i < settingsInfo.Files.Count; i++)
            {
                CreateWorkSheet(settingsInfo.Files[i].Value, settingsInfo.unit);
            }
            Save();
        }

        private void CreateWorkSheet(string filepath, List<Tuple<string, string, string>> unit)
        {
            try
            {
                var orgWks = _orgWorkBook.Layers.Add() as Origin.Worksheet;

                if (orgWks == null)
                {
                    Console.Error.WriteLine("Faild to add a work sheet!");
                    return;
                }
                // 将文件名设置为工作表名称
                orgWks.Name = filepath.Substring(filepath.LastIndexOf("\\") + 1, filepath.LastIndexOf(".") - filepath.LastIndexOf("\\") - 1);

                // 为工作表添加两列
                //var col1 = orgWks.Columns.Add(System.Type.Missing);
                //var col2 = orgWks.Columns.Add(System.Type.Missing);
                var col1 = orgWks.Columns[0];
                var col2 = orgWks.Columns[1];

                // 设置长名称，单位以及备注
                /*
                orgWks.Columns[0].LongName = "Temperature";
                orgWks.Columns[0].Units = @"(\+(o)C)";
                orgWks.Columns[1].LongName = "Presure";
                orgWks.Columns[1].Units = @"(lb/in\+(2))";*/
                col1.LongName = unit[0].Item2;
                col1.Units = unit[0].Item3;

                // 直接在这里填入列名称了，工具项目要什么飞机大炮
                col2.LongName = orgWks.Name;
                col2.Units = unit[1].Item3;

                // 设置列类型
                col1.Type = Origin.COLTYPES.COLTYPE_X;
                col2.Type = Origin.COLTYPES.COLTYPE_Y;

                // 设置长名称以及单位可见
                orgWks.LabelVisible[LABELTYPEVALS.LT_LONG_NAME] = true;
                orgWks.LabelVisible[LABELTYPEVALS.LT_UNIT] = true;

                // 提取数据
                var data = DataInput.GetSingleFileContent(filepath);

                // 注入工作表
                col1.SetData(data[0].ToArray(), System.Type.Missing); //col (1)
                col2.SetData(data[1].ToArray(), System.Type.Missing); //col (2)
            }
            catch (Exception)
            {
                Console.WriteLine("ERROR");
            }
        }

        public void CreatDataGroupFromDirectory(string FilesPath)
        {
            
            var parsResult = PathInput.GetInputFilesInfo(FilesPath);
            
            string ProjectSavePath = FilesPath + "\\数据分析.opju";
            if (parsResult == null)
            {
                Console.WriteLine("Path is empty!");
                return;
            }
            //var _orgWorkBook=_org.WorksheetPages.Add(System.Type.Missing, System.Type.Missing);

            // 设置工程的保存路径
            ProjectPath = ProjectSavePath;
            if (FilesPath == ".")
            {
                ProjectPath = System.Environment.CurrentDirectory+"\\数据分析.opj";
            }
            foreach (var item in parsResult)
            {
                CreateWorkBook(item);
            }
        }

        #region 弃用代码
        [Obsolete]
        private void CreateWorkBook(OriginWorkBook settingsInfo)
        {
            // 新增一个工作簿
            _orgWorkBook = AddWorkBook();

            // 设置工作簿名称
            _orgWorkBook.LongName = settingsInfo.Name;
            // 添加数据
            for (int i = 0; i < settingsInfo.DataFilePath.Count; i++)
            {
                CreateWorkSheet(settingsInfo.DataFilePath[i],
                                settingsInfo.ColUnit);
            }
            Save();
        }

        /// <summary>
        /// 为工作簿添加一张工作表
        /// </summary>
        /// <param name="filepath">工作表的数据来源</param>
        /// <param name="unit">单位描述</param>
        /// <param name="index">工作表在工作簿中的位置</param>
        /// <returns></returns>
        [Obsolete]
        private void CreateWorkSheet(string filepath, List<KeyValuePair<string, string>> unit)
        {
            try
            {
                var orgWks = _orgWorkBook.Layers.Add() as Origin.Worksheet;

                if (orgWks == null)
                {
                    Console.Error.WriteLine("Faild to add a work sheet!");
                    return;
                }
                // 将文件名设置为工作表名称
                orgWks.Name = filepath.Substring(filepath.LastIndexOf("\\") + 1, filepath.LastIndexOf(".") - filepath.LastIndexOf("\\") - 1);

                // 为工作表添加两列
                //var col1 = orgWks.Columns.Add(System.Type.Missing);
                //var col2 = orgWks.Columns.Add(System.Type.Missing);
                var col1 = orgWks.Columns[0];
                var col2 = orgWks.Columns[1];

                // 设置长名称，单位以及备注
                /*
                orgWks.Columns[0].LongName = "Temperature";
                orgWks.Columns[0].Units = @"(\+(o)C)";
                orgWks.Columns[1].LongName = "Presure";
                orgWks.Columns[1].Units = @"(lb/in\+(2))";*/
                col1.LongName = unit[0].Key;
                col1.Units = unit[0].Value;
                col2.LongName = unit[1].Key;
                col2.Units = unit[1].Value;

                // 设置列类型
                col1.Type = Origin.COLTYPES.COLTYPE_X;
                col2.Type = Origin.COLTYPES.COLTYPE_Y;

                // 设置长名称以及单位可见
                orgWks.LabelVisible[LABELTYPEVALS.LT_LONG_NAME] = true;
                orgWks.LabelVisible[LABELTYPEVALS.LT_UNIT] = true;

                // 提取数据
                var data = DataInput.GetSingleFileContent(filepath);

                // 注入工作表
                col1.SetData(data[0].ToArray(), System.Type.Missing); //col (1)
                col2.SetData(data[1].ToArray(), System.Type.Missing); //col (2)
            }
            catch (Exception)
            {
                Console.WriteLine("ERROR");
            }
        }
        [Obsolete]
        public void CreatDataGroupFromSettingsFile(string settingFilePath)
        {
            string ProjectSavePath = "";
            var settingsInfo = SettingsInput.GetSettingsInfo(settingFilePath, ref ProjectSavePath);
            if (settingsInfo == null)
            {
                Console.WriteLine("Settings Error!");
                return;
            }
            //var _orgWorkBook=_org.WorksheetPages.Add(System.Type.Missing, System.Type.Missing);

            // 设置工程的保存路径
            ProjectPath = ProjectSavePath;
            foreach (var item in settingsInfo)
            {
                CreateWorkBook(item);
            }
        }
        #endregion
    }
}
