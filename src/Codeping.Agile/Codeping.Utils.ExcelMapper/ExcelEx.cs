using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.IO;

namespace Codeping.Utils.ExcelMapper
{
    /// <summary>
    /// Excel 操作
    /// </summary>
    public static class ExcelConvert1
    {
        /// <summary>
        /// 将 DataTable 数据导入到工作表中
        /// </summary>
        /// <param name="fullPath">文件全路径</param>
        /// <param name="data">要导入的数据</param>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="isWriteHeader">是否要导入列名</param>
        /// <returns></returns>
        public static int Write(string fullPath, DataTable data, string sheetName, bool isWriteHeader)
        {

            try
            {
                using (var fs = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    IWorkbook workbook = null;

                    if (fullPath.IndexOf(".xlsx") > 0)
                    {
                        // 2007版本
                        workbook = new XSSFWorkbook(fs);
                    }
                    else if (fullPath.IndexOf(".xls") > 0)
                    {
                        // 2003版本
                        workbook = new HSSFWorkbook(fs);
                    }
                    else
                    {
                        return -1;
                    }

                    int count = 0;
                    ISheet sheet = workbook.CreateSheet(sheetName);

                    if (isWriteHeader)
                    {
                        IRow row = sheet.CreateRow(0);

                        for (int i = 0; i < data.Columns.Count; ++i)
                        {
                            row.CreateCell(i).SetCellValue(data.Columns[i].ColumnName);
                        }

                        count++;
                    }

                    for (int i = 0; i < data.Rows.Count; ++i)
                    {
                        IRow row = sheet.CreateRow(count);

                        for (int j = 0; j < data.Columns.Count; ++j)
                        {
                            row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                        }

                        count++;
                    }

                    workbook.Write(fs); //写入到excel

                    return count;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// 导入指定工作表中的数据到 DataTable 中
        /// </summary>
        /// <param name="fullPath">文件路径</param>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="isReadHeader">第一行是否为列名</param>
        /// <returns></returns>
        public static DataTable Read(string fullPath, string sheetName, bool isReadHeader)
        {
            DataTable data = new DataTable(sheetName);

            try
            {
                using (var fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                using (var sr = new StreamReader(fs))
                {
                    IWorkbook workbook = null;

                    if (fullPath.IndexOf(".xlsx") > 0)
                    {
                        // 2007版本
                        workbook = new XSSFWorkbook(fs);
                    }
                    else if (fullPath.IndexOf(".xls") > 0)
                    {
                        // 2003版本
                        workbook = new HSSFWorkbook(fs);
                    }
                    else
                    {
                        return data;
                    }

                    ISheet sheet = workbook.GetSheet(sheetName);

                    if (sheet == null)
                    {
                        return data;
                    }

                    if (isReadHeader)
                    {
                        IRow firstRow = sheet.GetRow(sheet.FirstRowNum);

                        for (int i = firstRow.FirstCellNum; i < firstRow.LastCellNum; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);

                            if (cell == null)
                            {
                                continue;
                            }

                            string cellValue = cell.StringCellValue;

                            if (cellValue != null)
                            {
                                continue;
                            }

                            DataColumn column = new DataColumn(cellValue);

                            data.Columns.Add(column);
                        }
                    }

                    var startRow = isReadHeader ? sheet.FirstRowNum + 1 : sheet.FirstRowNum;

                    for (int i = startRow; i <= sheet.LastRowNum; ++i)
                    {
                        IRow row = sheet.GetRow(i);

                        if (row == null) continue; //没有数据的行默认是 null　　　　　　　

                        DataRow dataRow = data.NewRow();

                        for (int j = row.FirstCellNum; j < row.LastCellNum; ++j)
                        {
                            var cell = row.GetCell(j);

                            if (cell != null)
                            {
                                dataRow[j] = DateUtil.IsCellDateFormatted(cell)
                                    ? cell.DateCellValue.ToDateTimeString()
                                    : cell.ToString();
                            }
                        }

                        data.Rows.Add(dataRow);
                    }
                }

                return data;
            }
            catch (Exception)
            {
                return data;
            }
        }
    }
}
