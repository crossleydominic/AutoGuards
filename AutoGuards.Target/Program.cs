using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

            ScenarioExecutionScope.Execute(() => { impl.UsesNotNull(null); }, true);
            ScenarioExecutionScope.Execute(() => { impl.UsesNotNull(new object()); }, false);

            ScenarioExecutionScope.Execute(() => { impl.UsesNotNullOrWhitespace(null); }, true);
            ScenarioExecutionScope.Execute(() => { impl.UsesNotNullOrWhitespace(""); }, true);
            ScenarioExecutionScope.Execute(() => { impl.UsesNotNullOrWhitespace(" "); }, true);
            ScenarioExecutionScope.Execute(() => { impl.UsesNotNullOrWhitespace("abc"); }, false);

            ScenarioExecutionScope.Execute(() => { impl.UsesNotEmpty(new List<object>()); }, true);
            ScenarioExecutionScope.Execute(() => { impl.UsesNotEmpty(new List<object>() { new object() }); }, false);

            ScenarioExecutionScope.Execute(() => { impl.NotNullAndNotEmpty(null); }, true);
            ScenarioExecutionScope.Execute(() => { impl.NotNullAndNotEmpty(new List<object>()); }, true);
            ScenarioExecutionScope.Execute(() => { impl.NotNullAndNotEmpty(new List<object>() { new object() }); }, false);

            ScenarioExecutionScope.Execute(() => { impl.IsDefined((FileMode)999); }, true);
            ScenarioExecutionScope.Execute(() => { impl.IsDefined(FileMode.Append); }, false);

            ScenarioExecutionScope.Execute(() => { impl.Matches("def"); }, true);
            ScenarioExecutionScope.Execute(() => { impl.Matches("abc"); }, false);

            Console.WriteLine("Finished... ");
            Console.ReadLine();
        }
    }
}