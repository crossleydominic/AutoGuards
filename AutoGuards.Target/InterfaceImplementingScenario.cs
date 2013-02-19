using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGuards.API;

namespace AutoGuards.Target
{
    public interface IInterfaceA
    {
        void InterfaceAMethod([NotNull] string str);

        void SharedMethod([GreaterThan(50)]int i);

        void ExplicitlyImplmentedMethod([NotNullOrWhitespace] string str);
    }

    public interface IInterfaceB
    {
        void SharedMethod([LessThan(100)] int i);
    }

    public class ImplementingOneInterface : IInterfaceA
    {
        public void InterfaceAMethod([Matches("[0-9]")] string str)
        {
            Console.WriteLine("ImplementingOneInterface.InterfaceAMethod invoked successfully");
        }

        public void SharedMethod([LessThan(100)] int i)
        {
            Console.WriteLine("ImplementingOneInterface.SharedMethod invoked successfully");
        }

        void IInterfaceA.ExplicitlyImplmentedMethod([Matches("[a-z]")] string str)
        {
            Console.WriteLine("ImplementingOneInterface.ExplicitlyImplmentedMethod invoked successfully");
        }
    }

    public class ImplemetingBothInterfaces : IInterfaceA, IInterfaceB
    {
        public void SharedMethod(int i)
        {
            Console.WriteLine("ImplemetingBothInterfaces.SharedMethod invoked successfully");
        }

        public void InterfaceAMethod(string str){ /* Do nothing */ }

        void IInterfaceA.ExplicitlyImplmentedMethod(string str) { /* Do nothing */ }
    }
}
