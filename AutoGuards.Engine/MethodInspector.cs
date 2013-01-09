﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGuards.API;
using AutoGuards.Engine.Emitters;
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

        public List<GuardedParameter> Inspect(CommonCompilation compilation, SemanticModel semanticModel, MethodDeclarationSyntax method)
        {
            List<GuardedParameter> resolvedParameters = new List<GuardedParameter>();

            var methodSymbol = semanticModel.GetDeclaredSymbol(method);

            //TODO: Need to do things in here like resolve base class/overriden/interface implemented methods

            foreach (var parameter in methodSymbol.Parameters)
            {
                GuardedParameter guardedParameter = null;

                foreach (var attribute in parameter.GetAttributes())
                {
                    //TODO: Think of a nicer way to ensure the type is declared in the correct assembly
                    if (string.Equals(attribute.AttributeClass.ContainingAssembly.Name, typeof (AutoGuardAttribute).Assembly.GetName().Name))
                    {
                        if (guardedParameter == null)
                        {
                            guardedParameter = new GuardedParameter();
                            guardedParameter.ParameterName = parameter.Name;
                            guardedParameter.ParameterType = parameter.Type;

                            resolvedParameters.Add(guardedParameter);
                        }

                        guardedParameter.Emitters.Add(_emitterResolver.Resolve(attribute.AttributeClass.Name));
                    }
                } 
            }

            return resolvedParameters;
        }
    }

    public class GuardedParameter
    {
        public GuardedParameter()
        {
            Emitters = new List<AutoGuardEmitter>();
        }

        public string ParameterName { get; set; }
        public TypeSymbol ParameterType { get; set; }
        public List<AutoGuardEmitter> Emitters { get; set; }
    }
}
