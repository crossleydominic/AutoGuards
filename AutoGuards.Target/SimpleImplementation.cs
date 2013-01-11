using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGuards.API;

namespace AutoGuards.Target
{
    public class SimpleImplementation
    {
        public void UsesNotNull([NotNull] object obj)
        {
            Console.WriteLine("UsesNotNull invoked successfully");
        }

        public void UsesNotNullOrWhitespace([NotNullOrWhitespace] string str)
        {
            Console.WriteLine("UsesNotNull invoked successfully");
        }

        public void UsesNotEmpty([NotEmpty] List<object> objs)
        {
            Console.WriteLine("UsesNotEmpty invoked successfully");
        }

        public void NotNullAndNotEmpty([NotNull] [NotEmpty] List<object> objs)
        {
            Console.WriteLine("SingleParamMultipleAttributes invoked successfully");
        }

        public void IsDefined([IsDefined] FileMode filemode)
        {
            Console.WriteLine("IsDefined invoked successfully");
        }

        public void Matches([Matches("abc")] string str)
        {
            Console.WriteLine("Matches invoked successfully");
        }
    }
}
