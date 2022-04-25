using Origin;
using System;
using Xunit;
using Xunit.Abstractions;

namespace Originbot.Test
{
    public class Try_Origin
    {
        private readonly ITestOutputHelper _output;

        public Try_Origin(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Test_tryCreatWorkSheet()
        {
            int NUMPTS = 100;
            Origin.Worksheet orgWks;
            Double[] s1 = new double[NUMPTS];
            Double[] s2 = new double[NUMPTS];

            //------------------------------------------------------------
            try
            {
                //Create the Origin COM object:
                Origin.Application org = new Origin.Application();

                if (org == null)
                {
                    Console.WriteLine("Origin could not be started. Check that your installation and project references are correct.");
                    return;
                }

                //Initialize new project:
                org.NewProject();

                //Add a workbook
                Origin.WorksheetPage orgWkBk = org.WorksheetPages.Add(System.Type.Missing, System.Type.Missing);

                //The sheet:
                orgWks = (Origin.Worksheet)orgWkBk.Layers[0];

                //Set Sheet name:
                orgWks.Name = "RawData";

                //Add two Columns
                orgWks.Columns.Add(System.Type.Missing);
                orgWks.Columns.Add(System.Type.Missing);

                //Set Long Names, Units, and Comment to the two columns:
                orgWks.Columns[0].LongName = "Temperature";
                orgWks.Columns[0].Units = @"(\+(o)C)";

                orgWks.Columns[1].LongName = "Presure";
                orgWks.Columns[1].Units = @"(lb/in\+(2))";

                //Set column types:
                orgWks.Columns[0].Type = Origin.COLTYPES.COLTYPE_X;
                orgWks.Columns[1].Type = Origin.COLTYPES.COLTYPE_Y;

                //Set LongName and Units as visible
                orgWks.set_LabelVisible(Origin.LABELTYPEVALS.LT_LONG_NAME, true);
                orgWks.set_LabelVisible(Origin.LABELTYPEVALS.LT_UNIT, true);

                //Set data array s1 and s2
                for (int ii = 0; ii <= NUMPTS - 1; ii++)
                {
                    s1[ii] = ii * 0.1;
                    s2[ii] = ii / 13.4;
                }

                //Create a single column data range in the workSheet
                orgWks.Columns[0].SetData(s1); //col (1)
                orgWks.Columns[1].SetData(s2); //col (2)

                //------------------------------------------------------------
                //The sheet:
                orgWks = (Origin.Worksheet)orgWkBk.Layers.Add() as Worksheet;

                //------------------------------------------------------------

                String PathName = "D:\\ProjectName.opj";

                // Save:
                if (org.Save(PathName) == false)
                {
                    _output.WriteLine("Failed to save the project into " + PathName);
                }
                else
                {
                    _output.WriteLine("Saved into " + PathName);
                }
                org.Exit();
            }
            catch
            {
                _output.WriteLine("ERROR");
            }
        }
    }
}
