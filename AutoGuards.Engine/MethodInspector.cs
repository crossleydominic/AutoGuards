using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGuards.API;
using AutoGuards.Engine.Emitters;
using Roslyn.Compilers;
using Roslyn.Compilers.CSharp;
using Roslyn.Compilers.Common;

namespace AutoGuards.Engine
{
    public class MethodInspector
    {
        private EmitterResolver _emitterResolver;

        public MethodInspector()
        {
            _emitterResolver = new EmitterResolver();
        }

        public void InspectHierarchy(MethodSymbol methodSymbol, List<GuardedParameter> guardedParameters)
        {
            MethodSymbol overriddenMethod = methodSymbol.OverriddenMethod;

            if (overriddenMethod != null)
            {
                InspectHierarchy(overriddenMethod, guardedParameters);
            }

            foreach (var parameter in methodSymbol.Parameters)
            {
                GuardedParameter guardedParameter = null;

                foreach (var attribute in parameter.GetAttributes())
                {
                    //TODO: Think of a nicer way to ensure the type is declared in the correct assembly and derives from the correct type
                    Type baseAutoGuardType = typeof(AutoGuardAttribute);
                    if (string.Equals(attribute.AttributeClass.BaseType.ContainingAssembly.Name, baseAutoGuardType.Assembly.GetName().Name, StringComparison.Ordinal) &&
                        string.Equals(attribute.AttributeClass.BaseType.Name, baseAutoGuardType.Name, StringComparison.Ordinal))
                    {
                        if (guardedParameter == null)
                        {
                            guardedParameter = new GuardedParameter();
                            guardedParameter.ParameterName = parameter.Name;
                            guardedParameter.ParameterType = parameter.Type;

                            guardedParameters.Add(guardedParameter);
                        }

                        guardedParameter.Emitters.Add(new EmitterAttributePair(_emitterResolver.Resolve(attribute.AttributeClass.Name), attribute));
                    }
                }
            }
        }

        public List<GuardedParameter> Inspect(MethodDeclarationSyntax method, MethodSymbol methodSymbol)
        {
            List<GuardedParameter> guardedParameters = new List<GuardedParameter>();

            InspectHierarchy(methodSymbol, guardedParameters);

            return guardedParameters;
        }
    }
}
