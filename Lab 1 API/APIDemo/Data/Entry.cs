using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDemo
{
    public class Entry
    {
        public string key;
        public object value;

        public Entry(string k, object v)
        {
            key = k;
            value = v;
        }

        public string toString()
        {
            return "(" + key + "," + value + ")";
        }
    }
}
