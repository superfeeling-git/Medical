using System;
using Medical.Domain;
using System.Reflection;
using Namotion.Reflection;
using System.Data;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Newtonsoft.Json;

namespace Medical.Tester
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            Console.WriteLine(program.Test());
            Console.ReadLine();
        }

        public string Test()
        {
            var str = string.Empty;
            try
            {
                str = Guid.NewGuid().ToString();
                throw new Exception();
            }
            catch (Exception)
            {
                str = "error";
            }
            return str;
        }
    }
}
