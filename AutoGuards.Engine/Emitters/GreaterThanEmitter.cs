using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGuards.API;
using Roslyn.Compilers.CSharp;

namespace AutoGuards.Engine.Emitters
{
    public class GreaterThanEmitter : AutoGuardEmitter
    {
        public override Type EmitsFor
        {
            get { return typeof (GreaterThanAttribute); }
        }

        public override StatementSyntax EmitGuard(AttributeData attribute, TypeSymbol parameterType, string parameterName)
        {
            throw new NotImplementedException();
        }
    }
}
