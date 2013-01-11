using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGuards.Target
{
    public static class RecoverableScope
    {
        public static void Execute(Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("Invocation of action failed with exception '{0}'", e.Message));
            }
        }
    }
}
