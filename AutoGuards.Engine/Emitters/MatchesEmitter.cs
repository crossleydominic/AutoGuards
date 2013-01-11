using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roslyn.Compilers.CSharp;

namespace AutoGuards.Engine.Emitters
{
    public class MatchesEmitter : AutoGuardEmitter
    {
        public override Type EmitsFor
        {
            get { return typeof (MatchesEmitter); }
        }

        public override StatementSyntax EmitGuard(TypeSymbol parameterType, string parameterName)
        {
            //Emit Regex.Matches(string value);
            return null;
        }
    }
}
