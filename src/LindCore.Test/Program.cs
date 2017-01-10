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
            LoggerFactory.Instance.Logger_Debug("test...");
            Console.ReadKey();
        }
    }
}
