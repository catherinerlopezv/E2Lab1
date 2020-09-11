using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIDemo.Models;

namespace APIDemo.Data
{
    public sealed class Singleton
    {
        private readonly static Singleton _instance = new Singleton();
        public tree Peliculas { get; set; }
        public int LastId = 5;
        private Singleton()
        {
            

           // tree Peliculas = new tree();
                Pelicula Peli = new Pelicula();
                
                Peli.nombre = "El señor de los anillos";
                Peli.anio = 2000;
                Peli.director = "no se";
                Peli.estrellas = 5;
                Peli.genero = "fantasia";

                Peliculas.put("a",Peli);
        }

        public static Singleton Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
