using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGuards.Engine.Emitters;
using Roslyn.Compilers.CSharp;

namespace AutoGuards.Engine
{
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
