using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGuards.API;

namespace AutoGuards.Target
{
    public abstract class OverridingImplementationBase
    {
        public abstract void AbstractMethod([NotNull] string str);

        public virtual void VirtualMethod([LessThan(20)] int i) {}

        public virtual void MethodToBeHidden([Matches("[A-Z]")] string str) {}
    }

    public class OverridingImplementationDerived : OverridingImplementationBase
    {
        public override void AbstractMethod([NotNullOrWhitespace] string str)
        {
            Console.WriteLine("AbstractMethod invoked successfully");
        }

        public override void VirtualMethod([GreaterThan(5)]int i)
        {
            Console.WriteLine("VirtualMethod invoked successfully");
        }

        public new void MethodToBeHidden([Matches("[0-9]")] string str)
        {
            Console.WriteLine("MethodToBeHidden invoked successfully");
        }
    }
}
