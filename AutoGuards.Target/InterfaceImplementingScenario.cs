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

        void ExplicitlyImplmentedMethod();

        void SharedMethod([GreaterThan(50)]int i);
    }

    public interface IInterfaceB
    {
        void SharedMethod([LessThan(100)] int i);

    }

    public class ImplementingOneInterface : IInterfaceA
    {
        public void InterfaceAMethod(string str)
        {
        }

        public void ExplicitlyImplmentedMethod()
        {
        }

        public void SharedMethod(int i)
        {
        }
    }

    public class ImplemetingBothInterfaces : IInterfaceA, IInterfaceB
    {
        public void SharedMethod(int i)
        {
        }

        public void InterfaceAMethod(string str)
        {
        }

        public void ExplicitlyImplmentedMethod()
        {
        }
    }

}
