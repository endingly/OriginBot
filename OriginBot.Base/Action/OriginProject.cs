using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Origin;

namespace Originbot.Base
{
    /// 别名
    using OriginWorkBook = System.Collections.Generic.Dictionary<string, List<KeyValuePair<string, string>>>;    

    public class OriginProject
    {
        Origin.Application _org;
        string _projectPath;

        public OriginProject()
        {
            _org = new Origin.Application();
            _projectPath = "";
            // 新建工程
            _org.NewProject();
        }

        public OriginProject(string path)
        {
            _org = new Origin.Application();
            _projectPath = path;
            // 新建工程
            _org.NewProject();
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
                Console.WriteLine("Failed to save the project into " + _projectPath);
            }
            else
            {
                Console.WriteLine("Saved into " + _projectPath);
            }
        }

        /// <summary>
        /// 首先需要明确，一次实验会有多组实验结果
        /// 所以，我们应当把每组实验单独列为一个工作簿
        /// 然后工作簿内，对各实验条件的区分再单独列表
        /// 导入数据的文本文件名一般应当以下面的格式来命名
        /// [导出的物理变量数据]_[区分特征1]_[区分特征2].txt
        /// </summary>
        /// <param name="filenames"></param>
        /// <returns></returns>
        private bool CreateWorkSheet(OriginWorkBook filenames)
        {
            try
            {
                // 创建应用程序句柄
                //Origin.Application org = new Origin.Application();
                if (_org == null)
                {
                    Console.WriteLine("Origin could not be started. Check that your installation and project references are correct.");
                    return false;
                }

                // 新建工程
                _org.NewProject();

                // 添加工作簿
                Origin.WorksheetPage orgWkBk = _org.WorksheetPages.Add(System.Type.Missing, System.Type.Missing);



            }
            catch (Exception)
            {

                throw;
            }
            return true;
        }


        public void CreateOPJ()
        {
            int NUMPTS = 100;
            Origin.Worksheet orgWks;
            Double[] s1 = new double[NUMPTS];
            Double[] s2 = new double[NUMPTS];

            //------------------------------------------------------------
            try
            {
                // 创建应用程序句柄
                Origin.Application org = new Origin.Application();
                
                if (org == null)
                {
                    Console.WriteLine("Origin could not be started. Check that your installation and project references are correct.");
                    return;
                }

                // 新建工程
                org.NewProject();

                // 添加工作簿
                Origin.WorksheetPage orgWkBk = org.WorksheetPages.Add(System.Type.Missing, System.Type.Missing);

                // 工作表
                orgWks = (Origin.Worksheet)orgWkBk.Layers[0];
                

                // 工作表名称
                orgWks.Name = "RawData";

                // 为工作表添加两列
                orgWks.Columns.Add(System.Type.Missing);
                orgWks.Columns.Add(System.Type.Missing);

                // 设置长名称，单位以及备注
                orgWks.Columns[0].LongName = "Temperature";
                orgWks.Columns[0].Units = @"(\+(o)C)";

                orgWks.Columns[1].LongName = "Presure";
                orgWks.Columns[1].Units = @"(lb/in\+(2))";

                // 设置列类型
                orgWks.Columns[0].Type = Origin.COLTYPES.COLTYPE_X;
                orgWks.Columns[1].Type = Origin.COLTYPES.COLTYPE_Y;

                // 设置长名称以及单位可见
                orgWks.LabelVisible[LABELTYPEVALS.LT_LONG_NAME] = true;
                orgWks.LabelVisible[LABELTYPEVALS.LT_UNIT] = true;

                // 初始化数据
                for (int ii = 0; ii <= NUMPTS - 1; ii++)
                {
                    s1[ii] = ii * 0.1;
                    s2[ii] = ii / 13.4;
                }

                // 注入工作表
                orgWks.Columns[0].SetData(s1, System.Type.Missing); //col (1)
                orgWks.Columns[1].SetData(s2, System.Type.Missing); //col (2)


                //------------------------------------------------------------

                String PathName = "D:\\Users\\codelib\\originlab\\originlab_command\\bin\\Debug\\net6.0\\test.opju";

                
            }

            catch
            {
                Console.WriteLine("ERROR");
            }

        }

    }
}
