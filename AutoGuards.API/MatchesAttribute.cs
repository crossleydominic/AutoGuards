using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGuards.API
{
    public class MatchesAttribute : AutoGuardAttribute
    {
        private string _regex;

        public MatchesAttribute(string regex)
        {
            _regex = regex;
        }
    }
}
