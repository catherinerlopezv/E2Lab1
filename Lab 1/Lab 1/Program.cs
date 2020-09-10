using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace Lab_1
{
    class Program
    {
        public struct pelicula
        {
            public string nombre;
            public int anio;
            public string director;
            public int estrellas;
            public string genero;

            public override string ToString()
            {
                return "{" + nombre + ", " + anio + ", " + director + ", " + estrellas + ", " + genero + "}";
            }
        }

        static void Main(string[] args)
        {
           
            string k;
            object v;
            Console.WriteLine("creando el arbol");
            tree T = new tree();
            Console.WriteLine("agregando datos");
            pelicula peli;
            peli.nombre = "El señor de los anillos";
            peli.anio = 2000;
            peli.director = "no se";
            peli.estrellas = 5;
            peli.genero = "fantasia";
            T.put("z", 1); T.checkTree();
            T.put("y", 2); T.checkTree();
            T.put("x", 3); T.checkTree();
            T.put("w", 4); T.checkTree();
            T.put("v", 5); T.checkTree();
            T.put("u", 6); T.checkTree();
            T.put("t", 7); T.checkTree();
            T.put("s", 8); T.checkTree();

            T.put("r", 9); T.checkTree();
            T.put("q", 10); T.checkTree();
            T.put("p", 11); T.checkTree();

            T.put("o", 12); T.checkTree();
            T.put("n", 13); T.checkTree();
            T.put("m", 14); T.checkTree();
            T.put("l", 15); T.checkTree();
            T.put("k", 16); T.checkTree();
            T.put("j", 17); T.checkTree();
            T.put("i", 18); T.checkTree();
            T.put("h", 18); T.checkTree();
            T.put("g", 19); T.checkTree();
            T.put("f", 20); T.checkTree();
            T.put("e", peli); T.checkTree();
            T.put("d", 22); T.checkTree();
            T.put("c", 23); T.checkTree();
            T.put("b", 24); T.checkTree();
            T.put("a", 25); T.checkTree();

            T.print = true;

            T.printTree();

            Console.WriteLine("buscando valor de e ");
            Console.WriteLine(T.get("e"));
            Console.WriteLine("finaliza");
            Console.ReadLine();
        }
    }
}
