using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGuards.API
{
    public class GreaterThanAttribute : AutoGuardAttribute
    {
        private object _value;

        public GreaterThanAttribute(object value)
        {
            _value = value;
        }
    }
}
