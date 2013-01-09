using System;
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
            DoSomething(null);

            Console.WriteLine("Program ran");
            Console.ReadLine();
        }

        private static void DoSomething([NotNull] object obj)
        {
            
        }
    }
}