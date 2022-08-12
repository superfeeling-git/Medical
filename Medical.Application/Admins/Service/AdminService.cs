using Medical.Application.Admins.Dto;
using Medical.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;

using System.IO;
using NPOI.SS.UserModel;
using NPOI.SS.Formula.Functions;

namespace Medical.Application.Admins.Service
{
    [Authorize]
    public class AdminService : ApplicationService, IAdminService
    {
        private readonly IRepository<Admin> rep;

        public AdminService(IRepository<Admin> rep)
        {
            this.rep = rep;
        }

        [HttpPost("/Admin/Add")]
        public async Task<AdminDto> Create(AdminDto adminDto)
        {
            var entity = ObjectMapper.Map<AdminDto, Admin>(adminDto);
            var admin = await rep.InsertAsync(entity);
            return ObjectMapper.Map<Admin, AdminDto>(admin);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<string> ExportExcel()
        {
            Dictionary<string, string> columns = new Dictionary<string, string>();
            columns.Add("UserName", "用户名");
            columns.Add("Password", "密码");
        }
    }

    public class ExcelHelper
    {
        /// <summary>
        /// 1、string FilePath
        /// 2、string SheetName
        /// 3、string Company Subject
        /// 4、List List
        /// 5、列名 字典项
        /// 6、
        /// </summary>
        /// <returns></returns>
        public static string ExportData<T>(string FilePath, List<T> list, Dictionary<string,string> columns, string SheetName = "sheet1")
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
            using (FileStream file = new FileStream($"{Directory.GetCurrentDirectory()}/{fileName}", FileMode.OpenOrCreate))
            {
                book.Write(file);
            }
            return fileName;
        }
    }
}
