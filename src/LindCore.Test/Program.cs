using LindCore.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LindCore.Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LoggerFactory.Logger_Debug("test...");
            Console.WriteLine("Lind for .NetCore Platform!");
            Console.ReadKey();
        }
    }
}
