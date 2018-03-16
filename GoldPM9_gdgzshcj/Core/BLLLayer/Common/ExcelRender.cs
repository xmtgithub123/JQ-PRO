using System.Data;
using System.IO;
using System.Text;
using System.Web;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.Collections;
using System;
using System.Collections.Generic;
namespace Common
{
    /// <summary>
    /// 使用NPOI操作Excel，无需Office COM组件
    /// NPOI官方网站http://npoi.codeplex.com/
    /// </summary>
    public class ExcelRender
    {

        public static FileStream GetFileStream(string fileFullPath)
        {
            return new FileStream(fileFullPath, FileMode.Open, FileAccess.Read);
        }

        /// <summary>
        /// 根据Excel列类型获取列的值
        /// </summary>
        /// <param name="cell">Excel列</param>
        /// <returns></returns>
        private static string GetCellValue(ICell cell)
        {
            if (cell == null)
                return string.Empty;
            switch (cell.CellType)
            {
                case CellType.BLANK:
                    return string.Empty;
                case CellType.BOOLEAN:
                    return cell.BooleanCellValue.ToString();
                case CellType.ERROR:
                    return cell.ErrorCellValue.ToString();
                //case CellType.NUMERIC:
                //    return cell.DateCellValue.ToString();
                case CellType.Unknown:
                default:
                    return cell.ToString();//This is a trick to get the correct value of the cell. NumericCellValue will return a numeric value no matter the cell value is a date or a number
                case CellType.STRING:
                    return cell.StringCellValue;
                case CellType.FORMULA:
                    try
                    {
                        HSSFFormulaEvaluator e = new HSSFFormulaEvaluator(cell.Sheet.Workbook);
                        e.EvaluateInCell(cell);
                        return cell.ToString();
                    }
                    catch
                    {
                        return cell.NumericCellValue.ToString();
                    }
            }
        }

        /// <summary>
        /// 保存Excel文档流到文件
        /// </summary>
        /// <param name="ms">Excel文档流</param>
        /// <param name="fileName">文件名</param>
        private static void SaveToFile(MemoryStream ms, string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                byte[] data = ms.ToArray();

                fs.Write(data, 0, data.Length);
                fs.Flush();

                data = null;
            }
        }


        /// <summary>
        /// DataReader转换成Excel文档流
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static MemoryStream RenderToExcel(IDataReader reader)
        {
            MemoryStream ms = new MemoryStream();

            using (reader)
            {
                IWorkbook workbook = GetIWorkbook(null, ".xls");
                ISheet sheet = workbook.CreateSheet();
                IRow headerRow = sheet.CreateRow(0);
                int cellCount = reader.FieldCount;

                // handling header.
                for (int i = 0; i < cellCount; i++)
                {
                    headerRow.CreateCell(i).SetCellValue(reader.GetName(i));
                }

                // handling value.
                int rowIndex = 1;
                while (reader.Read())
                {
                    IRow dataRow = sheet.CreateRow(rowIndex);

                    for (int i = 0; i < cellCount; i++)
                    {
                        dataRow.CreateCell(i).SetCellValue(reader[i].ToString());
                    }

                    rowIndex++;
                }

                AutoSizeColumns(sheet);

                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;

                ms.Close();
                workbook = null;
                sheet = null;
            }
            return ms;
        }

        /// <summary>
        /// DataReader转换成Excel文档流，并保存到文件
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="fileName">保存的路径</param>
        public static void RenderToExcel(IDataReader reader, string fileName)
        {
            using (MemoryStream ms = RenderToExcel(reader))
            {
                SaveToFile(ms, fileName);
            }
        }

        /// <summary>
        /// DataReader转换成Excel文档流，并输出到客户端
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="context">HTTP上下文</param>
        /// <param name="fileName">输出的文件名</param>
        public static void RenderToExcel(IDataReader reader, HttpContext context, string fileName)
        {
            using (MemoryStream ms = RenderToExcel(reader))
            {
                RenderToBrowser(ms, context, fileName);
            }
        }
        /// <summary>
        /// DataTable转换成Excel文档流，并保存到文件
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fileName">保存的路径</param>
        public static void RenderToExcel(DataTable table, string fileName)
        {
            using (MemoryStream ms = RenderToExcel(table))
            {
                SaveToFile(ms, fileName);
            }
        }

        /// <summary>
        /// DataTable转换成Excel文档流，并输出到客户端
        /// </summary>
        /// <param name="table"></param>
        /// <param name="response"></param>
        /// <param name="fileName">输出的文件名</param>
        public static void RenderToExcel(DataTable table, HttpContext context, string fileName)
        {
            using (MemoryStream ms = RenderToExcel(table))
            {
                RenderToBrowser(ms, context, fileName);
            }
        }
        /// <summary>
        /// DataTable转换成Excel文档流
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static MemoryStream RenderToExcel(DataTable table)
        {
            MemoryStream ms = new MemoryStream();
            using (table)
            {
                IWorkbook workbook = GetIWorkbook(null, ".xls");
                ISheet sheet = workbook.CreateSheet();

                IRow headerRow = sheet.CreateRow(0);

                // handling header.
                foreach (DataColumn column in table.Columns)
                    headerRow.CreateCell(column.Ordinal).SetCellValue(column.Caption);//If Caption not set, returns the ColumnName value

                // handling value.
                int rowIndex = 1;

                foreach (DataRow row in table.Rows)
                {
                    IRow dataRow = sheet.CreateRow(rowIndex);

                    foreach (DataColumn column in table.Columns)
                    {
                        dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                    }

                    rowIndex++;
                }
                AutoSizeColumns(sheet);

                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;

                // ms.Close();
                workbook = null;
                sheet = null;
            }
            return ms;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="excelFileStream">>Excel文档流</param>
        /// <param name="ext">扩展名</param>
        /// <returns></returns>
        private static IWorkbook GetIWorkbook(Stream excelFileStream, string ext)
        {
            try
            {
                return new XSSFWorkbook(excelFileStream);
            }
            catch
            {
                return new HSSFWorkbook(excelFileStream);
            }
            //if (ext == ".xlsx") return excelFileStream == null ? new XSSFWorkbook() : new XSSFWorkbook(excelFileStream);

            //return excelFileStream == null ? new HSSFWorkbook() : new HSSFWorkbook(excelFileStream);
        }
        /// <summary>
        /// 自动设置Excel列宽
        /// </summary>
        /// <param name="sheet">Excel表</param>
        private static void AutoSizeColumns(ISheet sheet)
        {
            if (sheet.PhysicalNumberOfRows > 0)
            {
                IRow headerRow = sheet.GetRow(0);

                for (int i = 0, l = headerRow.LastCellNum; i < l; i++)
                {
                    sheet.AutoSizeColumn(i);
                }
            }
        }
        /// <summary>
        /// 输出文件到浏览器
        /// </summary>
        /// <param name="ms">Excel文档流</param>
        /// <param name="context">HTTP上下文</param>
        /// <param name="fileName">文件名</param>
        private static void RenderToBrowser(MemoryStream ms, HttpContext context, string fileName)
        {
            if (context.Request.Browser.Browser == "IE")
                fileName = HttpUtility.UrlEncode(fileName);
            context.Response.AddHeader("Content-Disposition", "attachment;fileName=" + fileName);
            context.Response.BinaryWrite(ms.ToArray());
        }
        /// <summary>
        /// Excel文档流是否有数据
        /// </summary>
        /// <param name="excelFileStream">Excel文档流</param>
        /// <param name="ext">扩展名</param>
        /// <returns></returns>
        public static bool HasData(Stream excelFileStream, string ext)
        {
            return HasData(excelFileStream, 0, ext);
        }

        /// <summary>
        /// Excel文档流是否有数据
        /// </summary>
        /// <param name="excelFileStream">Excel文档流</param>
        /// <param name="sheetIndex">表索引号，如第一个表为0</param>
        /// <param name="ext">扩展名</param>
        /// <returns></returns>
        public static bool HasData(Stream excelFileStream, int sheetIndex, string ext)
        {
            bool result = false;
            IWorkbook workbook = GetIWorkbook(excelFileStream, ext);
            if (workbook.NumberOfSheets > 0)
            {
                if (sheetIndex < workbook.NumberOfSheets)
                {
                    ISheet sheet = workbook.GetSheetAt(sheetIndex);
                    result = sheet.PhysicalNumberOfRows > 0;
                    sheet = null;
                }
            }

            excelFileStream.Close();
            workbook = null;
            return result;
        }

        /// <summary>
        /// Excel文档流转换成DataTable
        /// 第一行必须为标题行
        /// </summary>
        /// <param name="excelFileStream">Excel文档流</param>
        /// <param name="sheetName">表名称</param>
        /// <param name="ext">扩展名</param>
        /// <returns></returns>
        public static DataTable RenderFromExcel(Stream excelFileStream, string sheetName, string ext)
        {
            return RenderFromExcel(excelFileStream, sheetName, 0, ext);
        }

        /// <summary>
        /// Excel文档流转换成DataTable
        /// </summary>
        /// <param name="excelFileStream">Excel文档流</param>
        /// <param name="sheetName">表名称</param>
        /// <param name="headerRowIndex">标题行索引号，如第一行为0</param>
        /// <param name="ext">扩展名</param>
        /// <returns></returns>
        public static DataTable RenderFromExcel(Stream excelFileStream, string sheetName, int headerRowIndex, string ext)
        {
            DataTable table = null;

            IWorkbook workbook = GetIWorkbook(excelFileStream, ext);
            ISheet sheet = workbook.GetSheet(sheetName);
            table = RenderFromExcel(sheet, headerRowIndex);

            excelFileStream.Close();
            workbook = null;
            sheet = null;

            return table;
        }

        /// <summary>
        /// Excel文档流转换成DataTable
        /// 默认转换Excel的第一个表
        /// 第一行必须为标题行
        /// </summary>
        /// <param name="excelFileStream">Excel文档流</param>
        /// <param name="ext">扩展名</param>
        /// <returns></returns>
        public static DataTable RenderFromExcel(Stream excelFileStream, string ext)
        {
            return RenderFromExcel(excelFileStream, 0, 0, ext);
        }

        /// <summary>
        /// Excel文档流转换成DataTable
        /// 第一行必须为标题行
        /// </summary>
        /// <param name="excelFileStream">Excel文档流</param>
        /// <param name="sheetIndex">表索引号，如第一个表为0</param>
        /// <param name="ext">扩展名</param>
        /// <returns></returns>
        public static DataTable RenderFromExcel(Stream excelFileStream, int sheetIndex, string ext)
        {
            return RenderFromExcel(excelFileStream, sheetIndex, 0, ext);
        }

        public static DataTable RenderFromExcel(string fileFullName)
        {
            FileStream fs = GetFileStream(fileFullName);
            return RenderFromExcel(fs, Path.GetExtension(fileFullName));
        }

        /// <summary>
        /// Excel文档流转换成DataTable
        /// </summary>
        /// <param name="excelFileStream">Excel文档流</param>
        /// <param name="sheetIndex">表索引号，如第一个表为0</param>
        /// <param name="headerRowIndex">标题行索引号，如第一行为0</param>
        /// <param name="ext">扩展名</param>
        /// <returns></returns>
        public static DataTable RenderFromExcel(Stream excelFileStream, int sheetIndex, int headerRowIndex, string ext)
        {
            DataTable table = null;

            IWorkbook workbook = GetIWorkbook(excelFileStream, ext);
            ISheet sheet = workbook.GetSheetAt(sheetIndex);

            table = RenderFromExcel(sheet, headerRowIndex);

            excelFileStream.Close();
            workbook = null;
            sheet = null;

            return table;
        }

        /// <summary>
        /// Excel文档流转换成DataTable
        /// </summary>
        /// <param name="excelFileStream">Excel文档流</param>
        /// <param name="headerRowIndex">标题行索引号，如第一行为0</param>
        /// <param name="ext">扩展名</param>
        /// <returns></returns>
        public static DataSet RenderFromExcelByDataSet(Stream excelFileStream, int headerRowIndex, string ext)
        {
            DataSet ds = new DataSet();
            IWorkbook workbook = GetIWorkbook(excelFileStream, ext);
            int count = workbook.NumberOfSheets;
            for (int i = 0; i < count; i++)
            {
                ISheet sheet = workbook.GetSheetAt(i);
                DataTable table = RenderFromExcel(sheet, headerRowIndex);
                ds.Tables.Add(table);
                sheet = null;
            }
            excelFileStream.Close();
            workbook = null;
            return ds;
        }

        /// <summary>
        /// Excel表格转换成DataTable
        /// </summary>
        /// <param name="sheet">表格</param>
        /// <param name="headerRowIndex">标题行索引号，如第一行为0</param>
        /// <returns></returns>
        private static DataTable RenderFromExcel(ISheet sheet, int headerRowIndex)
        {
            DataTable table = new DataTable();

            IRow headerRow = sheet.GetRow(headerRowIndex);

            if (headerRow == null) return table;

            int cellCount = headerRow.LastCellNum;//LastCellNum = PhysicalNumberOfCells
            int rowCount = sheet.LastRowNum;//LastRowNum = PhysicalNumberOfRows - 1

            //handling header.
            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                string headColumnName = GetCellValue(headerRow.GetCell(i));

                while (table.Columns.Contains(headColumnName)) headColumnName = headColumnName + "+";

                DataColumn column = new DataColumn(headColumnName);
                table.Columns.Add(column);

            }

            for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
            {
                IRow row = sheet.GetRow(i);
                DataRow dataRow = table.NewRow();

                if (row != null)
                {
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                        {
                            if (dataRow.ItemArray.Length > j)
                            {
                                dataRow[j] = GetCellValue(row.GetCell(j));
                            }
                        }
                    }
                }

                table.Rows.Add(dataRow);
            }

            return table;
        }

        /// <summary>
        /// DataSet转换成Excel文档流，并输出到客户端
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="response"></param>
        /// <param name="fileName">输出的文件名</param>
        public static void RenderToExcel(DataSet ds, HttpContext context, string fileName)
        {
            using (MemoryStream ms = RenderToExcel(ds))
            {
                RenderToBrowser(ms, context, fileName);
            }
        }

        /// <summary>
        /// DataSet转换成Excel文档流
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static MemoryStream RenderToExcel(DataSet ds)
        {
            MemoryStream ms = new MemoryStream();
            IWorkbook workbook = GetIWorkbook(null, ".xls");

            for (int i = 0; i < ds.Tables.Count; i++)
            {
                DataTable table = ds.Tables[i];
                using (table)
                {
                    ISheet sheet = workbook.CreateSheet(table.TableName);
                    IRow headerRow = sheet.CreateRow(0);
                    // handling header.
                    foreach (DataColumn column in table.Columns)
                        headerRow.CreateCell(column.Ordinal).SetCellValue(column.Caption);//If Caption not set, returns the ColumnName value

                    // handling value.
                    int rowIndex = 1;

                    foreach (DataRow row in table.Rows)
                    {
                        IRow dataRow = sheet.CreateRow(rowIndex);

                        foreach (DataColumn column in table.Columns)
                        {
                            dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                        }

                        rowIndex++;
                    }
                    AutoSizeColumns(sheet);
                    sheet = null;
                }

            }
            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;
            workbook = null;
            ms.Close();

            return ms;
        }
        #region 自定义重载方法
        /// <summary>Hashtable List转换成Excel文档流，并输出到客户端
        /// 
        /// </summary>
        /// <param name="ht">List</param>
        /// <param name="context"></param>
        /// <param name="fileName">文件名称</param>
        public static void RenderToExcel(Dictionary<string, object> dic, HttpContext context, string fileName)
        {

            using (MemoryStream ms = RenderToExcel(dic))
            {
                // SaveToFile(ms, "c:\\1.xls");
                RenderToBrowser(ms, context, fileName);
            }
        }
        /// <summary>List转换成Excel文档流
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static MemoryStream RenderToExcel(Dictionary<string, object> dic)
        {
            MemoryStream ms = new MemoryStream();
            IWorkbook workbook = GetIWorkbook(null, ".xls");
            foreach (KeyValuePair<string, object> item in dic)
            {
                ISheet sheet;
                sheet = workbook.CreateSheet(item.Key.ToString());
                IRow headerRow = sheet.CreateRow(0);
                var list = item.Value as IList;
                if (list.Count > 0)
                {
                    // var list = item.Value as IList;
                    var p = list[0].GetType().GetProperties();
                    for (int i = 0; i < p.Length; i++)
                    {
                        var property = p[i];
                        headerRow.CreateCell(i).SetCellValue(property.Name);
                    }
                    int rowIndex = 1;
                    foreach (object item1 in list)
                    {
                        IRow dataRow = sheet.CreateRow(rowIndex);
                        var p1 = item1.GetType().GetProperties();
                        for (int i = 0; i < p1.Length; i++)
                        {
                            var property = p1[i];
                            dataRow.CreateCell(i).SetCellValue(property.GetValue(item1, null).ToString());
                        }
                        rowIndex++;
                    }
                }
                AutoSizeColumns(sheet);
                sheet = null;
            }
            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;
            workbook = null;
            ms.Close();
            return ms;
        }

        /// <summary>list转换成Excel文档流，并输出到客户端
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="response"></param>
        /// <param name="fileName">输出的文件名</param>
        public static void RenderToExcel(IList list, HttpContext context, string fileName)
        {

            using (MemoryStream ms = RenderToExcel(list))
            {
                RenderToBrowser(ms, context, fileName);
            }
        }

        /// <summary>List转换成Excel文档流
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static MemoryStream RenderToExcel(IList list)
        {
            MemoryStream ms = new MemoryStream();
            IWorkbook workbook = GetIWorkbook(null, ".xls");
            ISheet sheet;
            sheet = workbook.CreateSheet("Sheet1");
            IRow headerRow = sheet.CreateRow(0);

            var p = list[0].GetType().GetProperties();
            for (int i = 0; i < p.Length; i++)
            {
                var property = p[i];
                headerRow.CreateCell(i).SetCellValue(property.Name);
            }
            int rowIndex = 1;
            foreach (object item in list)
            {
                IRow dataRow = sheet.CreateRow(rowIndex);
                var p1 = item.GetType().GetProperties();
                for (int i = 0; i < p1.Length; i++)
                {
                    var property = p1[i];
                    dataRow.CreateCell(i).SetCellValue(property.GetValue(item, null).ToString());
                }
                rowIndex++;
            }
            AutoSizeColumns(sheet);
            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;
            workbook = null;
            sheet = null;
            return ms;
        }
        #endregion
    }
}
