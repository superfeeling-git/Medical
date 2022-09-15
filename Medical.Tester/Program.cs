using System;
using Medical.Domain;
using System.Reflection;
using Namotion.Reflection;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MiniExcelLibs;
using System.Net.Http;
using Medical.Application.Admins.Dto;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SqlSugar;
using System.Data;
using DbType = SqlSugar.DbType;

namespace Medical.Tester
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //创建数据库对象
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "Data Source=.;Initial Catalog=HongZhui;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;",
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true
            });

            //调试SQL事件，可以删掉 (要放在执行方法之前)
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql);//输出sql,查看执行sql 性能无影响

                //5.0.8.2 获取无参数化SQL 对性能有影响，特别大的SQL参数多的，调试使用
                //UtilMethods.GetSqlString(DbType.SqlServer,sql,pars)
            };

            var dt = db.Ado.GetDataTable("select * from hz_AboutClass");

            foreach (DataColumn item in dt.Columns)
            {
                
                Console.WriteLine(item.ColumnName);
            }

            Console.ReadLine();







            /*string path = @"E:\Medical\Medical.Tester\收文笺";
            var files = Directory.GetFiles(path);
            var list = files.Where(m => !m.Contains('~')).Select(m =>
            {
                var fullfileName = Path.GetFileName(m).Trim(".doc".ToCharArray());
                var index = Regex.Match(fullfileName, @"^\d+").Value;
',                {
                    index = Convert.ToInt16(index),
                    fileName = Regex.Replace(fullfileName, @"^\d+", ""),
                };
            }).OrderBy(m => m.index);

            MiniExcel.SaveAs(@"E:\\Medical\\Medical.Tester\\收文笺\temp.xlsx", list);*/

            Console.ReadLine();
        }
    }
}
