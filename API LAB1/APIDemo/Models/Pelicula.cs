using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDemo
{
    public class Pelicula
    {
      
        public string nombre { get => nombre; set => nombre = value; }
        public int anio { get => anio; set => anio = value; }
        public string director { get => director; set => director = value; }
        public int estrellas { get => estrellas; set => estrellas = value; }
        public string genero { get => genero; set => genero = value; }

        public override string ToString()
        {
            return "{" + nombre + ", " + anio + ", " + director + ", " + estrellas + ", " + genero + "}";
        }
    }
}
