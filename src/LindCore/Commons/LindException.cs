using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LindCore.Commons
{
    public class LindException : Exception
    {
        public LindException(string msg) : base(msg)
        {

        }
    }
}
