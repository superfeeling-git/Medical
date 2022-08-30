using System;
using Medical.Domain;
using System.Reflection;
using Namotion.Reflection;
using System.Data;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Medical.Tester
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            Image img = b.Encode(BarcodeLib.TYPE.UPCA, "12345678901", Color.Black, Color.White, 290, 120);
            img.Save("E:\\Medical\\Medical.Tester\\abc.png", ImageFormat.Png);
        }
    }
}
