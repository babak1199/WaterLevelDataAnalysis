using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.ComponentModel;
using Excel = Microsoft.Office.Interop.Excel;


namespace ExportToExcel
{
    public class ExcelFormat
    {

        private void ExportToExcel(string[] data)
        {
            Excel.Application excel = null;
            Excel.Workbooks workbooks = null;
            Excel.Workbook workbook = null;
            Excel.Sheets sheets = null;

            #region create excel object
            excel = new Excel.Application();
            excel.ScreenUpdating = false;
            excel.Visible = false;

            workbooks = excel.Workbooks;
            workbook = workbooks.Add();

            sheets = workbook.Worksheets;
            #endregion

            DataTable dt = ConvertToDataTable(data);

            string name = "";
            Excel.Worksheet worksheet = CreateWorksheet(sheets, name);

            // put data table values in excel sheet
            Excel.Range range = worksheet.Range["A1", "B" +
                                            (dt.Rows.Count).ToString()];
            range.Value = ConvertDataTableTo2DObjectArray(dt);

            // create chart
            name = "";
            CreateChart(worksheet, name, dt.Rows.Count);

            string file = "name" + ".xlsx";
            workbook.SaveAs(file, Excel.XlFileFormat.xlWorkbookDefault);
            workbook.Close(true);
            excel.Quit();

            TryKillProcessByMainWindowHwnd(excel.Hwnd);
        }

        private Excel.Worksheet CreateWorksheet(Excel.Sheets sheets, string name)
        {
            Excel.Worksheet worksheet = null;
            worksheet = sheets.Add();
            worksheet.Name = name;

            return worksheet;
        }

        private object[,] ConvertDataTableTo2DObjectArray(DataTable dt)
        {
            object[,] arr = new object[dt.Rows.Count, dt.Columns.Count];
            for (int r = 0; r < dt.Rows.Count; r++)
            {
                DataRow dr = dt.Rows[r];
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    arr[r, c] = dr[c];
                }
            }

            return arr;
        }

        private void CreateChart(Excel.Worksheet worksheet,
                                                    string name, int rows, bool bCreate = true)
        {
            Excel.Chart chart = null;
            int sheetIndex = 1;
            if (bCreate)
                chart = worksheet.Shapes.AddChart(Excel.XlChartType.xlXYScatter).Chart;
            else
            {
                Excel.ChartObject chartObject = worksheet.ChartObjects(1);
                chart = chartObject.Chart;
            }
            Excel.Range chrtYSeries =
                                worksheet.get_Range("B1:B" + rows.ToString());

            Excel.SeriesCollection allSeries = chart.SeriesCollection() as Excel.SeriesCollection;
            Excel.Series oSeries = null;
            if (bCreate)
            {
                allSeries.NewSeries();
                oSeries = (Excel.Series)chart.SeriesCollection(sheetIndex);
                oSeries.Values = chrtYSeries;
            }
            else
            {
                chart.ChartWizard(chrtYSeries);
                oSeries = (Excel.Series)chart.SeriesCollection(1);
                Excel.ChartArea area = chart.ChartArea;
                area.Height = 355;
                area.Width = 780;
            }
            oSeries.Name = name;
            oSeries.XValues = worksheet.get_Range("A1", "A" + rows.ToString());
            chart.Refresh();
        }

        private DataTable ConvertToDataTable(string[] data, string splitter = ",")
        {
            DataTable dt = new DataTable();

            int colNo = data[0].Split(splitter.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Length;
            for (int i = 0; i < colNo; i += 2)
                dt.Columns.Add(i.ToString(), typeof(string));

            for (int r = 0; r < data.Length; r++)
            {
                string[] cols = data[r].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                List<object> obj = new List<object>();
                for (int i = 0; i < colNo; i++)
                    obj.Add(cols[i]);
                dt.LoadDataRow(obj.ToArray(), true);
            }

            return dt;
        }

        #region access and kill the processes
        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        /// <summary> Tries to find and kill process by hWnd to the main window of the process.</summary>
        /// <param name="hWnd">Handle to the main window of the process.</param>
        /// <returns>True if process was found and killed. False if process was not found by hWnd or if it could not be killed.</returns>
        public static bool TryKillProcessByMainWindowHwnd(int hWnd)
        {
            uint processID;
            GetWindowThreadProcessId((IntPtr)hWnd, out processID);
            if (processID == 0) return false;
            try
            {
                Process.GetProcessById((int)processID).Kill();
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (Win32Exception)
            {
                return false;
            }
            catch (NotSupportedException)
            {
                return false;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
            return true;
        }

        /// <summary> Finds and kills process by hWnd to the main window of the process.</summary>
        /// <param name="hWnd">Handle to the main window of the process.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when process is not found by the hWnd parameter (the process is not running). 
        /// The identifier of the process might be expired.
        /// </exception>
        /// <exception cref="Win32Exception">See Process.Kill() exceptions documentation.</exception>
        /// <exception cref="NotSupportedException">See Process.Kill() exceptions documentation.</exception>
        /// <exception cref="InvalidOperationException">See Process.Kill() exceptions documentation.</exception>
        public static void KillProcessByMainWindowHwnd(int hWnd)
        {
            uint processID;
            GetWindowThreadProcessId((IntPtr)hWnd, out processID);
            if (processID == 0)
                throw new ArgumentException("Process has not been found by the given main window handle.", "hWnd");
            Process.GetProcessById((int)processID).Kill();
        }
        #endregion
    }
}

