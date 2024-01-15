using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego_Cartas
{
    internal class Carta
    {
        public string nombre, elemento;
        public int ataque, defensa;

        public Carta(string nombre, string elemento, int ataque, int defensa)
        {
            this.nombre = nombre;
            this.elemento = elemento;
            this.ataque = ataque;
            this.defensa = defensa;
        }
    }
}
