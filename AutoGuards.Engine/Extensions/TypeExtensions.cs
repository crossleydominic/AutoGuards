using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGuards.Engine.Extensions
{
    public static class TypeExtensions
    {
        public static string GetGlobalName(this Type type)
        {
            return "global::" + type.FullName;
        }
    }
}
