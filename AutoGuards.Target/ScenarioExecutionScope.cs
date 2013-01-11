using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGuards.Target
{
    public static class ScenarioExecutionScope
    {
        public static void Execute(Action action, bool exceptionExpected)
        {
            Console.WriteLine();
            Console.WriteLine("Executing scenario...");

            bool exceptionThrown;
            try
            {
                action();
                exceptionThrown = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("Invocation of action failed with exception '{0}'", e.Message));
                exceptionThrown = true;
            }

            bool suceeded = (exceptionExpected ? exceptionThrown == true : exceptionThrown == false);

            ConsoleColor originalColor = Console.ForegroundColor;
            if (suceeded)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Success");
                Console.ForegroundColor = originalColor;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Failure");
                Console.ForegroundColor = originalColor;
            }
        }
    }
}
