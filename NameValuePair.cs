using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class NameValuePair
    {
        public string Name;
        public object Value;

        public NameValuePair (string n, object v)
        {
            Name = n;
            Value = v;
        }

        public override string ToString ()
        {
            return Name + " " + Value.GetType ().Name.ToString () + " " + Value.ToString ();
        }
    }
}
