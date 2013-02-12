using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGuards.Engine.Expressions
{
    public static class Fake
    {
        public static string String { get { return Any<string>(); } }
        public static bool Bool { get { return Any<bool>(); } }

        public static int Int { get { return Any<int>(); } }
        public static long Long { get { return Any<long>(); } }
        public static short Short { get { return Any<short>(); } }

        public static float Float { get { return Any<float>(); } }
        public static double Double { get { return Any<double>(); } }

        public static decimal Decimal { get { return Any<decimal>(); } }

        public static object Object { get { return Any<object>(); } }

        public static T Any<T>()
        {
            return default(T);
        }
    }
}
