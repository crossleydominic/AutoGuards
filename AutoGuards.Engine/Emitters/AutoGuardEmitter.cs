using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roslyn.Compilers.CSharp;

namespace AutoGuards.Engine.Emitters
{
    public abstract class AutoGuardEmitter
    {
        public abstract Type EmitsFor { get; }

        public abstract StatementSyntax EmitGuard(TypeSymbol parameterType, string parameterName);
    }
}
