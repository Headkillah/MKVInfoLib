using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MKV;

namespace LibTest
{
    class Program
    {
        static void Main(string[] args)
        {
            MKVFileInfo mkv = new MKVFileInfo(@"C:\Users\Benny\Desktop\mkv\test.mkv");
            Console.WriteLine(mkv.resolution);
            Console.WriteLine(mkv.duration);
            Console.ReadKey();
        }
    }
}
