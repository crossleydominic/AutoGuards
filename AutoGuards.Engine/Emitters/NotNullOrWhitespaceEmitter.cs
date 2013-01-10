using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGuards.API;
using Roslyn.Compilers.CSharp;

namespace AutoGuards.Engine.Emitters
{
    public class NotNullOrWhitespaceEmitter : AutoGuardEmitter
    {
        public override Type EmitsFor
        {
            get { return typeof (NotNullOrWhitespaceAttribute); }
        }

        public override StatementSyntax EmitGuard(Roslyn.Compilers.CSharp.TypeSymbol parameterType, string parameterName)
        {
            return Syntax.EmptyStatement();
        }
    }
}
