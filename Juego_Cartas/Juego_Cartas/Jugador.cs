using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Juego_Cartas
{
    internal class Jugador
    {
        public string Nombre;
        public int vida;
        public List<Carta> cartas = new List<Carta>();
        public List<Carta> mazo = new List<Carta>();
        public List<Carta> mano = new List<Carta>();

        public Jugador(string nombre, int vida, List<Carta> cartas, List<Carta> mazo)
        {
            Nombre = nombre;
            this.vida = vida;
            this.cartas = cartas;
            this.mazo = mazo;
        }

        public string GenerarMazo()
        {
            string texto = "";
            Random random = new Random();
            int contador = 0;
            for (int i = 0; i <= 19; i++)
            {
                contador++;
                int index = random.Next(cartas.Count);
                string nombre_mazo_rndm = cartas[index].nombre.ToString();
                string ataque_mazo_rndm = cartas[index].ataque.ToString();
                string defensa_mazo_rndm = cartas[index].defensa.ToString();
                string elemento_mazo_rndm = cartas[index].elemento.ToString();
                mazo.Add(cartas[index]);
                texto += contador + "Nombre: " + nombre_mazo_rndm + " Ataque: " + ataque_mazo_rndm + " Defensa: " + defensa_mazo_rndm + " Elemento: " + elemento_mazo_rndm + "\r\n";
                cartas.Remove(cartas[index]);

            }

            return texto;
            
        }

        public string GenerarMano()
        {
            string texto = "";
            Random random = new Random();
            int contador_2 = 0;
            for (int i2 = 0; i2 <= 4; i2++)
                {

                 contador_2++;
                 int index2 = random.Next(mazo.Count);

                 string nombre_mano_rndm = mazo[index2].nombre.ToString();
                 string ataque_mano_rndm = mazo[index2].ataque.ToString();
                 string defensa_mano_rndm = mazo[index2].defensa.ToString();
                 string elemento_mano_rndm = mazo[index2].elemento.ToString();
                 mano.Add(mazo[index2]);
                 texto += contador_2 + "Nombre: " + nombre_mano_rndm + " Ataque: " + ataque_mano_rndm + " Defensa: " + defensa_mano_rndm + " Elemento: " + elemento_mano_rndm + "\r\n";
                 mazo.Remove(mazo[index2]);
             }

            
            return texto;
        }
    }
}
