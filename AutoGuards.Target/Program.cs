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
            //Simple scenario
            Console.WriteLine("=== Start of Simple Scenarios ===");
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

            ScenarioExecutionScope.Execute(() => { impl.LessThan(15); }, true);
            ScenarioExecutionScope.Execute(() => { impl.LessThan(6); }, false);

            ScenarioExecutionScope.Execute(() => { impl.GreaterThan(6); }, true);
            ScenarioExecutionScope.Execute(() => { impl.GreaterThan(15); }, false);

            //Overriding scenario
            Console.WriteLine("=== Start of Overriding Scenarios ===");
            OverridingImplementationDerived implDerived = new OverridingImplementationDerived();
            ScenarioExecutionScope.Execute(() => { implDerived.AbstractMethod(null); }, true);
            ScenarioExecutionScope.Execute(() => { implDerived.AbstractMethod(string.Empty); }, true);
            ScenarioExecutionScope.Execute(() => { implDerived.AbstractMethod(" "); }, true);
            ScenarioExecutionScope.Execute(() => { implDerived.AbstractMethod("someVal"); }, false);

            ScenarioExecutionScope.Execute(() => { implDerived.VirtualMethod(21);}, true);
            ScenarioExecutionScope.Execute(() => { implDerived.VirtualMethod(4); }, true);
            ScenarioExecutionScope.Execute(() => { implDerived.VirtualMethod(10); }, false);

            ScenarioExecutionScope.Execute(() => { implDerived.MethodToBeHidden("A"); }, true);
            ScenarioExecutionScope.Execute(() => { implDerived.MethodToBeHidden("5"); }, false);

            //Interface implementation scenario
            Console.WriteLine("=== Start of Interface Implementing Scenarios ===");
            ImplementingOneInterface implOne = new ImplementingOneInterface();
            ScenarioExecutionScope.Execute(() => { implOne.InterfaceAMethod(null); }, true);
            ScenarioExecutionScope.Execute(() => { implOne.InterfaceAMethod("5"); }, false);

            ScenarioExecutionScope.Execute(() => { implOne.SharedMethod(20); }, true);
            ScenarioExecutionScope.Execute(() => { implOne.SharedMethod(120); }, true);
            ScenarioExecutionScope.Execute(() => { implOne.SharedMethod(80); }, false);

            ScenarioExecutionScope.Execute(() => { ((IInterfaceA)implOne).ExplicitlyImplmentedMethod(null); }, true);
            ScenarioExecutionScope.Execute(() => { ((IInterfaceA)implOne).ExplicitlyImplmentedMethod(""); }, true);
            ScenarioExecutionScope.Execute(() => { ((IInterfaceA)implOne).ExplicitlyImplmentedMethod("d"); }, false);
            ScenarioExecutionScope.Execute(() => { ((IInterfaceA)implOne).ExplicitlyImplmentedMethod("a"); }, false);


            Console.WriteLine("Finished... ");
            Console.ReadLine();
        }
    }
}