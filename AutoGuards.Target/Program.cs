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
            SimpleImplementation impl = new SimpleImplementation();

            RecoverableScope.Execute(() => { impl.UsesNotNull(null); });
            RecoverableScope.Execute(() => { impl.UsesNotNull(new object()); });

            RecoverableScope.Execute(() => { impl.UsesNotNullOrWhitespace(null); });
            RecoverableScope.Execute(() => { impl.UsesNotNullOrWhitespace(""); });
            RecoverableScope.Execute(() => { impl.UsesNotNullOrWhitespace(" "); });
            RecoverableScope.Execute(() => { impl.UsesNotNullOrWhitespace("abc"); });

            RecoverableScope.Execute(() => { impl.UsesNotEmpty(new List<object>());});
            RecoverableScope.Execute(() => { impl.UsesNotEmpty(new List<object>() { new object() }); });

            RecoverableScope.Execute(() => { impl.NotNullAndNotEmpty(null); });
            RecoverableScope.Execute(() => { impl.NotNullAndNotEmpty(new List<object>()); });
            RecoverableScope.Execute(() => { impl.NotNullAndNotEmpty(new List<object>() { new object() }); });

            Console.WriteLine("Finished... ");
            Console.ReadLine();
        }
    }
}