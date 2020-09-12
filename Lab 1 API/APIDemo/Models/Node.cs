using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDemo.Models
{
    public class Node
    {
        public Entry[] e;
        public Node[] child;
        public Node parent;

        /* =====================================================
           Node(): a 2-4 node contains 4 subtress (and 3 keys)
           ===================================================== */
        public Node()
        {
            int i;

            e = new Entry[3];
            child = new Node[4];

            for (i = 0; i < 3; i++)
            {
                e[i] = null;
            }

            for (i = 0; i < 4; i++)
                child[i] = null;

            parent = null;
        }

        public string toString()
        {
            string textoNodo = "[";
            if (e[0] == null)
            {

                textoNodo += "(-,-),";
            }
            else
            {
                textoNodo += e[0].toString() + ",";
            }
            if (e[1] == null)
            {
                textoNodo += "(-,-),";
            }
            else
            {
                textoNodo += e[1].toString() + ",";
            }
            if (e[2] == null)
            {
                textoNodo += "(-,-),";
            }
            else
            {
                textoNodo += e[2].toString();
            }
            textoNodo += "]";
            return textoNodo;
        }
    }
}
