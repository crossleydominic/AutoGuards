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
            Emitters = new List<EmitterAttributePair>();
        }

        public string ParameterName { get; set; }
        public TypeSymbol ParameterType { get; set; }
        public List<EmitterAttributePair> Emitters { get; set; }
    }

    public class EmitterAttributePair
    {
        private AutoGuardEmitter _emitter;
        private AttributeData _attribute;

        public EmitterAttributePair(AutoGuardEmitter emitter, AttributeData attribute)
        {
            _emitter = emitter;
            _attribute = attribute;
        }

        public AutoGuardEmitter Emitter { get { return _emitter; } }
        public AttributeData Attribute { get { return _attribute; } }
    }
}
