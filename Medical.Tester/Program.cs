using System;
using Medical.Domain;
using System.Reflection;
using Namotion.Reflection;
using System.Data;

namespace Medical.Tester
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Guid.Empty);



            DataSet ds = new DataSet();



            DataTable dt = new DataTable();

            dt.Columns.Add("Id", typeof(Guid));
            dt.Columns.Add("Name", typeof(string));

            DataRow dr = dt.NewRow();
            dr["Id"] = Guid.NewGuid();
            dr["Name"] = "张三";
            dt.Rows.Add(dr);

            DataRow dr1 = dt.NewRow();
            dr1["Id"] = Guid.NewGuid();
            dr1["Name"] = "李四";
            dt.Rows.Add(dr1);

            ds.Tables.Add(dt);

            foreach (DataRow item in dt.Rows)
            {
                Console.WriteLine(item["Id"]);
                Console.WriteLine(item["Name"]);
            }

            Console.ReadLine();
        }
    }
}
