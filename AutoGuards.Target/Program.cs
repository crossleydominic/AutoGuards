using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGuards.API;

namespace AutoGuards.Target
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try{ DoSomething(null, null, null); }catch (Exception e){ Console.WriteLine(e.Message); }

            try{ DoSomething(new StringBuilder(""), string.Empty, null); }catch (Exception e){ Console.WriteLine(e.Message); }

            try { DoSomething(new StringBuilder(""), "someString", new List<string>()); } catch (Exception e) { Console.WriteLine(e.Message); }

            Console.WriteLine("Program ran");
            Console.ReadLine();
        }

        private static void DoSomething(
            [NotNull] StringBuilder obj, 
            [NotNullOrWhitespace]string str,
            [NotEmpty] List<string> list)
        {
            var blah = ((IList)list).Count;
            Console.WriteLine("DoSomething invoked");
        }
    }
}