using Namotion.Reflection;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Medical.Utility
{
    public static class ExcelHelper
    {
        #region 导出Excel

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="FilePath"></param>
        /// <param name="list"></param>
        /// <param name="columns"></param>
        /// <param name="SheetName"></param>
        /// <returns></returns>
        public static string ExportData<T>(string FilePath, List<T> list, Dictionary<string, string> columns, string SheetName = "sheet1")
        {
            //建工作簿
            HSSFWorkbook book = new HSSFWorkbook();
            //创建工作表
            var sheet1 = book.CreateSheet(SheetName);

            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI Team";

            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "NPOI SDK Example";

            book.DocumentSummaryInformation = dsi;
            book.SummaryInformation = si;

            var props = typeof(T).GetProperties();

            //构造标题行
            IRow rowHead = sheet1.CreateRow(0);
            for (int p = 0; p < props.Length; p++)
            {
                ICell cell = rowHead.CreateCell(p);
                if (columns.ContainsKey(props[p].Name))
                    cell.SetCellValue(columns[props[p].Name]);
            }

            int i = 1;
            foreach (var item in list)
            {
                IRow row = sheet1.CreateRow(i);

                int j = 0;
                //内层循环
                foreach (var prop in props)
                {
                    ICell cell = row.CreateCell(j);
                    //动态获取属性的值
                    var obj = prop.GetValue(item);
                    //赋值
                    cell.SetCellValue(obj.ToString());
                    j++;
                }

                i++;
            }

            var fileName = $"{DateTime.Now.ToString("yyyyMMddHHmmss")}.xls";
            using (FileStream file = new FileStream($"{FilePath}/{fileName}", FileMode.OpenOrCreate))
            {
                book.Write(file);
            }
            return fileName;
        }
        #endregion

        #region 导出Excel

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="FilePath"></param>
        /// <param name="list"></param>
        /// <param name="columns"></param>
        /// <param name="SheetName"></param>
        /// <returns></returns>
        public static string ExportData<T>(string FilePath, List<T> list, string SheetName = "sheet1")
        {
            //建工作簿
            HSSFWorkbook book = new HSSFWorkbook();
            //创建工作表
            var sheet1 = book.CreateSheet(SheetName);

            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI Team";

            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "NPOI SDK Example";

            book.DocumentSummaryInformation = dsi;
            book.SummaryInformation = si;

            var props = typeof(T).GetProperties();

            //构造标题行
            IRow rowHead = sheet1.CreateRow(0);
            for (int p = 0; p < props.Length; p++)
            {
                ICell cell = rowHead.CreateCell(p);
                cell.SetCellValue(props[p].GetXmlDocsSummary());
            }

            int i = 1;
            foreach (var item in list)
            {
                IRow row = sheet1.CreateRow(i);

                int j = 0;
                //内层循环
                foreach (var prop in props)
                {
                    ICell cell = row.CreateCell(j);
                    //动态获取属性的值
                    var obj = prop.GetValue(item);
                    //赋值
                    cell.SetCellValue(obj.ToString());
                    j++;
                }

                i++;
            }

            var fileName = $"{DateTime.Now.ToString("yyyyMMddHHmmss")}.xls";
            using (FileStream file = new FileStream($"{FilePath}/{fileName}", FileMode.OpenOrCreate))
            {
                book.Write(file);
            }
            return fileName;
        }
        #endregion        

        #region 导出Excel

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="FilePath"></param>
        /// <param name="list"></param>
        /// <param name="columns"></param>
        /// <param name="SheetName"></param>
        /// <returns></returns>
        public static string ExportDataExtr<T>(this List<T> list, string FilePath, string TableName = "", string Author = "", string Common = "注释", string SheetName = "sheet1")
        {
            //建工作簿
            HSSFWorkbook book = new HSSFWorkbook();
            //创建工作表
            var sheet1 = book.CreateSheet(SheetName);

            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI Team";

            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "NPOI SDK Example";

            book.DocumentSummaryInformation = dsi;
            book.SummaryInformation = si;


            var props = typeof(T).GetProperties();

            //设置样式
            var HeadStyle = book.CreateCellStyle();
            //水平居中
            HeadStyle.Alignment = HorizontalAlignment.Center;
            //创建字体
            var HeadFont = book.CreateFont();
            //设置字体字号
            HeadFont.FontHeight = 20 * 20;
            //设置字体
            HeadFont.FontName = "微软雅黑";
            //设置颜色
            HeadFont.Color = HSSFColor.Red.Index;
            //双线
            HeadFont.Underline = FontUnderlineType.Double;

            HeadStyle.SetFont(HeadFont);


            //合并单元格
            if (string.IsNullOrEmpty(TableName))
                TableName = typeof(T).GetXmlDocsSummary();

            var HeadCell = sheet1.CreateRow(0).CreateCell(0);

            HeadCell.SetCellValue(TableName);

            //设置单元格样式
            HeadCell.CellStyle = HeadStyle;

            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, props.Length - 1));

            //构造标题行
            IRow rowHead = sheet1.CreateRow(1);
            for (int p = 0; p < props.Length; p++)
            {
                ICell cell = rowHead.CreateCell(p);
                if (props[p].GetXmlDocsSummary() == "密码")
                {
                    if (!string.IsNullOrEmpty(Author) && !string.IsNullOrEmpty(Common))
                    {
                        //批注
                        var patr = sheet1.CreateDrawingPatriarch();
                        //var comment1 = patr.CreateCellComment(patr.CreateAnchor(0, 0, 0, 0, 2, 3, 4, 4));
                        var comment1 = patr.CreateCellComment(patr.CreateAnchor(col1: 3, row1: 4, col2: 8, row2: 8, dx1: 0, dy1: 0, dx2: 0, dy2: 0)); ;
                        comment1.Author = Author;
                        comment1.String = new HSSFRichTextString(Common);
                        cell.CellComment = comment1;
                    }
                }
                cell.SetCellValue(props[p].GetXmlDocsSummary());
            }

            //单元格样式
            var CellStyle = book.CreateCellStyle();

            var DataFormat = book.CreateDataFormat();

            CellStyle.DataFormat = DataFormat.GetFormat("yyyy年m月d日");


            int i = 2;
            foreach (var item in list)
            {
                IRow row = sheet1.CreateRow(i);

                int j = 0;
                //内层循环
                foreach (var prop in props)
                {
                    ICell cell = row.CreateCell(j);
                    //动态获取属性的值
                    var obj = prop.GetValue(item);

                    if (DateTime.TryParse(obj.ToString(), out DateTime date))
                    {
                        cell.SetCellValue(date);
                        cell.CellStyle = CellStyle;
                    }
                    else
                    {
                        //赋值
                        cell.SetCellValue(obj.ToString());
                    }

                    j++;
                }

                i++;
            }

            var fileName = $"{DateTime.Now.ToString("yyyyMMddHHmmss")}.xls";
            using (FileStream file = new FileStream($"{FilePath}/{fileName}", FileMode.OpenOrCreate))
            {
                book.Write(file);
            }
            return fileName;
        }
        #endregion

        #region MD5加密方法
        /// <summary>
        /// MD5加密方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string MD5Hash(this string input)
        {
            using (var md5 = MD5.Create())
            {
                var value = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                var result = BitConverter.ToString(value);
                return result.Replace("-", "");
            }
        }
        #endregion

    }
}
