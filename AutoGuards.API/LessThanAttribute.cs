using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGuards.API
{
    public class LessThanAttribute : AutoGuardAttribute
    {
        private object _value;

        public LessThanAttribute(object value)
        {
            _value = value;
        }
    }
}
