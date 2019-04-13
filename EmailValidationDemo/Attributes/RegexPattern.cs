using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAutorizationDemo.Application
{
    [AttributeUsage(System.AttributeTargets.Field | System.AttributeTargets.Parameter | System.AttributeTargets.Property, AllowMultiple = false)]
    public class RegexPatternAttribute : Attribute
    {
        public string Pattern { get; set; }
        private string _error;

        public RegexPatternAttribute(string pattern)
        {
            Pattern = pattern;
        }

        public string Error
        {
            get { return _error; }
            set { _error = value;  }
        }

    }
}
