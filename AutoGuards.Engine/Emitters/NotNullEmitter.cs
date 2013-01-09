using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGuards.API;
using Roslyn.Compilers.CSharp;

namespace AutoGuards.Engine.Emitters
{
    public class NotNullEmitter : AutoGuardEmitter
    {
        public override Type EmitsFor
        {
            get { return typeof (NotNullAttribute); }
        }

        public override Roslyn.Compilers.CSharp.StatementSyntax EmitGuard(TypeSymbol parameterType, string parameterName)
        {
            throw new NotImplementedException();
        }
    }
}
