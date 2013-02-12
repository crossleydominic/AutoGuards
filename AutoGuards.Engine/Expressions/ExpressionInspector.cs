using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AutoGuards.Engine.Expressions
{
    public static class ExpressionInspector
    {
        public static Invocation GetInvocation(Expression exp)
        {
            Type owningType = null;
            string methodName = null;

            if (exp is MethodCallExpression)
            {
                MethodCallExpression methodExp = (MethodCallExpression)exp;
                return new Invocation(methodExp.Method.DeclaringType, methodExp.Method.Name);
            }
            else if (exp is MemberExpression)
            {
                MemberExpression memberExp = (MemberExpression) exp;
                return new Invocation(memberExp.Member.DeclaringType, memberExp.Member.Name);
            }

            return null;
        }
    }
}
