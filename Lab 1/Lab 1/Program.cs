using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace Lab_1
{
    class Program
    {
       
        static void Main(string[] args)
        {
           
            string k;
            object v;
            Console.WriteLine("creando el arbol");
            tree T = new tree();
            Console.WriteLine("agregando dato a 1");
            T.put("a", 1);
            Console.WriteLine("revvisando el arbol");
            T.checkTree();
            T.put("c", 3); T.checkTree();
            T.put("m", 6); T.checkTree();
            T.put("l", 6); T.checkTree();
            T.put("d", 4); T.checkTree();
            T.put("e", 5); T.checkTree();
            T.put("j", 6); T.checkTree();
            T.put("f", 6); T.checkTree();

            T.put("i", 9); T.checkTree();
            T.put("b", 2); T.checkTree();
            T.put("g", 7); T.checkTree();

            T.put("h", 8); T.checkTree();
            T.put("k", 6); T.checkTree();
            T.put("ka", 6); T.checkTree();
            T.put("kb", 6); T.checkTree();
            T.put("ca", 6); T.checkTree();
            T.put("ba", 6); T.checkTree();
            T.put("bb", 6); T.checkTree();
            T.put("aa", 6); T.checkTree();
            T.put("ax", 6); T.checkTree();
            T.put("ae", 6); T.checkTree();
            T.put("ad", 6); T.checkTree();
            T.put("ac", 6); T.checkTree();
            T.put("ab", 6); T.checkTree();
            T.put("af", 6); T.checkTree();

            T.print = true;

            T.printTree();

            Console.WriteLine("buscando valor de e ");
            Console.WriteLine(T.get("e"));
            Console.WriteLine("finaliza");
            Console.ReadLine();
        }
    }
}
