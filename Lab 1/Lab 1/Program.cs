using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace Lab_1
{
    class Program
    {
        public struct pelicula
        {
         
            public string director;
            public string imdbRating;
            public string genero;
            public string anio;
            public string rottenTomatoesRating;
            public string nombre;


            public override string ToString()
            {
                return "{" + director + ", " + imdbRating + ", " + genero + ", " + anio + ", " + rottenTomatoesRating+ ", " + nombre+ "}";
            }
        }

        static void Main(string[] args)
        {
           
            Console.WriteLine("creando el arbol");
            tree T = new tree();
            Console.WriteLine("agregando datos");
            pelicula peli;

            Random rnd = new Random();

            String ubicacion = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\..\..\..\test1.json";
            string[] lineas = File.ReadAllLines(ubicacion);
            var tokens =new Regex(":( \"(.*?)\"| ([\\w]+|[\\d]|\\.)+)");
            var tokens2 = new Regex("(\")([\\w]|[\\d])");

            //Console.WriteLine(match2.Groups[1]);

            for (int i = 0; i < lineas.Length; i++)
            {
                Match matchPelicula = tokens.Match(lineas[i]);
                // Console.WriteLine(lineas[i]);
                if (lineas[i].Contains("director"))
                {
                    peli.director = matchPelicula.Groups[1].Value;
                    matchPelicula = tokens.Match(lineas[i+1]);
                    peli.imdbRating =   matchPelicula.Groups[1].Value;
                    matchPelicula = tokens.Match(lineas[i+2]);
                    peli.genero = matchPelicula.Groups[1].Value;
                    matchPelicula = tokens.Match(lineas[i+3]);
                    peli.anio =   matchPelicula.Groups[1].Value;
                    matchPelicula = tokens.Match(lineas[i+4]);
                    peli.rottenTomatoesRating = matchPelicula.Groups[1].Value;
                    matchPelicula = tokens.Match(lineas[i+5]);
                    peli.nombre = matchPelicula.Groups[1].Value;

                    Match matchTitulo = tokens2.Match(peli.nombre);
                 
                    T.put(matchTitulo.Groups[2].Value.ToLower(), peli);
              
                    i = i + 5;
                }
            }
            T.print = true;

             T.printTree();

            string opcion="";
                     while(opcion!="NO")
            {
            A:
                Console.Write("Buscar categoria, ingrese una letra o ingrese NO para salir: ");
                try
                {
                    opcion = Console.ReadLine();
                    Console.WriteLine(T.get(opcion));

                    goto A;
                }
                catch

                {
                   
                    goto A;
                }




            }


          
            
            Console.ReadLine();
        }
    }
}
