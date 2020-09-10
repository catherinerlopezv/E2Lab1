using System;
using System.Collections.Generic;
using System.Text;

namespace Lab_1
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
