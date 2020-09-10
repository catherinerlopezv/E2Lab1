using System;
using System.Collections.Generic;
using System.Text;

namespace Lab_1
{
    public class Entry
    {
       
            public string key;
            public int value;

            public Entry(string k, int v)
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
