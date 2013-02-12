using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGuards.Engine.Expressions
{
    public class Invocation
    {
        private Type _declaringType;
        private string _methodName;

        public Invocation(Type declaringType, string methodName)
        {
            _declaringType = declaringType;
            _methodName = methodName;
        }

        public Type DeclaringType { get { return _declaringType; } }
        public string MethodName { get { return _methodName; } }
    }
}
