using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGuards.Engine.Expressions
{
    public static class It
    {
        public static T Is<T>()
        {
            return default(T);
        }
    }
}
