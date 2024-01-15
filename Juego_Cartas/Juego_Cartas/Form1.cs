using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace Juego_Cartas
{
    public partial class Form1 : Form
    {
        List<Carta> cartas = new List<Carta>();
        List<Carta> mazo = new List<Carta>();
        List<Carta> mano = new List<Carta>();
        List<Carta> luchas = new List<Carta>();
        Jugador jugador1, jugador2;
        bool primerturno;
        bool turno = true;
        int cont = 0;
        int index_mano1;
        int index_mano2;


        public Form1()
        {
            InitializeComponent();
            lectura();
            
            primerturno = true;
            jugador1 = new Jugador("Jugador 1", 3, cartas, mazo);
            jugador2 = new Jugador("Jugador 2", 3, cartas, mazo);
            SeleccionarCartaJugador1.Enabled = false;
            SeleccionarCartaJugador2.Enabled = false;
            EnfrentarCartas.Enabled = false;
            label3.Text = jugador1.vida.ToString();
            label4.Text = jugador2.vida.ToString();
            if (jugador1.vida <= 0)
            {
                
            }
            else if (jugador2.vida <= 0)
            {
                this.Close();
            }
        }

        private void lectura()
        {
            StreamReader reader = new StreamReader("cartas.txt");
            while (!reader.EndOfStream)
            {
                string salida = reader.ReadLine();
                string[] linea = salida.Split(',');
                Carta carta = new Carta(linea[0], linea[3], Int32.Parse(linea[1]), Int32.Parse(linea[2]));
                cartas.Add(carta);

            }
            reader.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Genera Mazo Jugador 1
            jugador1.GenerarMazo();
            MazoJugador1.Items.AddRange(jugador1.mazo.Select(a => $"{a.nombre}, Ataque: {a.ataque}, Defensa: {a.defensa}, Elemento: {a.elemento}").ToArray());
            GenerarMazo_Jugador2.Enabled = false;
            GenerarMano_Jugador2.Enabled=false;
            
        }


        private void button2_Click(object sender, EventArgs e)
        {
            //Genera Mano Jugador 1
            jugador1.GenerarMano();
            ManoJugador1.Items.AddRange(jugador1.mano.Select(a => $"{a.nombre}, Ataque: {a.ataque}, Defensa: {a.defensa}, Elemento: {a.elemento}").ToArray());
            GenerarMazo_Jugador2.Enabled = true;
            GenerarMano_Jugador2.Enabled = true;

        }

        private void button3_Click(object sender, EventArgs e)
        {   
            //Genera Mano Jugador 2
            mano.Clear();
            jugador2.GenerarMano();
            ManoJugador2.Items.AddRange(jugador2.mano.Select(b => $"{b.nombre}, Ataque: {b.ataque}, Defensa: {b.defensa}, Elemento: {b.elemento}").ToArray());


        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Genera Mazo Jugador 2 
            mazo.Clear();
            jugador2.GenerarMazo();
            MazoJugador2.Items.AddRange(jugador2.mazo.Select(d => $"{d.nombre}, Ataque: {d.ataque}, Defensa: {d.defensa}, Elemento: {d.elemento}").ToArray());


        }

        private void llenar_listBox()
        {
            
            //Borramos ListBoxes
            ManoJugador1.Items.Clear();
            ManoJugador2.Items.Clear();
            //Actualizamos las listbox
            ManoJugador1.Items.AddRange(jugador1.mano.Select(a => $"{a.nombre}, Ataque: {a.ataque}, Defensa: {a.defensa}, Elemento: {a.elemento}").ToArray());
            ManoJugador2.Items.AddRange(jugador2.mano.Select(b => $"{b.nombre}, Ataque: {b.ataque}, Defensa: {b.defensa}, Elemento: {b.elemento}").ToArray());
            ManoJugador1.Enabled = true;
            ManoJugador2.Enabled = true;
            label3.Text = jugador1.vida.ToString();
            label4.Text = jugador2.vida.ToString();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
            primerturno = new Random().Next(2) == 0;
            if (primerturno)
            {
                MessageBox.Show("Comienza el turno de Jugador 1 \r\nElija una carta para atacar");
                MazoJugador2.Enabled = false;
                ManoJugador2.Enabled = false;
                MazoJugador1.Enabled = false;
                SeleccionarCartaJugador1.Enabled = true;
                
            }
            else
            {
                MessageBox.Show("Comienza el turno de Jugador 2 \r\nElija una carta para atacar");
                MazoJugador1.Enabled = false;
                ManoJugador1.Enabled = false;
                MazoJugador2.Enabled = false;
                SeleccionarCartaJugador2.Enabled = true;
                
            }
            button1.Enabled = false;
        }
        

        private void button3_Click_1(object sender, EventArgs e)
        {
            string carta_seleccionada = ManoJugador1.SelectedItem.ToString();
            index_mano1 = ManoJugador1.SelectedIndex;
            string[] linea = carta_seleccionada.Split(',', ':'); 
            string nombre = linea[0];
            int ataque = Int32.Parse(linea[2]);
            int defensa = Int32.Parse(linea[4]);
            string elemento = linea[6];
            MessageBox.Show(" Ataque: "+ ataque + " Defensa: "+ defensa + " Elemento: "+elemento);
            Carta car = new Carta(nombre, elemento, ataque, defensa);
            luchas.Add(car);
            label5.Text = car.nombre;
            label6.Text = car.elemento;
            label7.Text = car.ataque.ToString();
            label8.Text = car.defensa.ToString();
           
            cont++;
            if (cont >= 2)
            {
                cont = 0;
                turno = false;
                SeleccionarCartaJugador1.Enabled = false;
                SeleccionarCartaJugador2.Enabled = false;
                EnfrentarCartas.Enabled = true;
            }
            if (turno)
            {
                MessageBox.Show("Turno del jugador 2 \r\nElija una carta para defender");
                MazoJugador1.Enabled = false;
                ManoJugador1.Enabled = false;
                ManoJugador2.Enabled = true;
                SeleccionarCartaJugador2.Enabled = true;
                SeleccionarCartaJugador1.Enabled = false;
            }

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string carta = ManoJugador2.SelectedItem.ToString();
            string[] linea = carta.Split(',', ':');
            string nombre = linea[0];
            int ataque = Int32.Parse(linea[2]);
            int defensa = Int32.Parse(linea[4]);
            string elemento = linea[6];
            index_mano2 = ManoJugador2.SelectedIndex;
            MessageBox.Show(" Ataque: " + ataque + " Defensa: " + defensa + " Elemento: " + elemento);
            Carta car = new Carta(nombre,elemento,ataque,defensa);
            luchas.Add(car);
            label9.Text = car.nombre;
            label10.Text = car.elemento;
            label11.Text = car.ataque.ToString();
            label12.Text = car.defensa.ToString();

            cont++;
            if (cont >= 2)
            {
                cont = 0;
                turno = false;
                SeleccionarCartaJugador1.Enabled = false;
                SeleccionarCartaJugador2.Enabled = false;
                EnfrentarCartas.Enabled = true;
            }
            if (turno)
            {

                MessageBox.Show("Turno del jugador 1 \r\nElija una carta para defender");
                MazoJugador2.Enabled = false;
                ManoJugador2.Enabled = false;
                ManoJugador1.Enabled = true;
                SeleccionarCartaJugador1.Enabled = true;
            }

        }
        


        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Genera los Mazos y Manos de los jugadores para comenzar!");

        }

        private void EnfrentarCartas_Click(object sender, EventArgs e)
        {
            luchar(luchas, primerturno);
            label5.Text = "";
            label6.Text = "";
            label7.Text = "";
            label8.Text = "";
            label9.Text = "";
            label10.Text = "";
            label11.Text = "";
            label12.Text = "";

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void luchar(List<Carta> cartas, bool primerTurno)
        {
            
            if (primerTurno)
            {
                string elemento1 = cartas[0].elemento.Trim(); // Eliminamos espacios adicionales
                string elemento2 = cartas[1].elemento.Trim();

                if ((elemento1 == "Hielo" && elemento2 == "Fuego") ||
                    (elemento1 == "Fuego" && elemento2 == "Tierra") ||
                    (elemento1 == "Tierra" && elemento2 == "Electricidad") ||
                    (elemento1 == "Electricidad" && elemento2 == "Hielo"))
                {
                    // Si el elemento gana, el ataque se multiplica * 2
                    cartas[0].ataque *= 2;

                    if (cartas[0].ataque >= cartas[1].defensa)
                    {
                        jugador2.vida--;

                        if (jugador2.vida <= 0)
                        {
                            MessageBox.Show("Fin del Juego \r\nGanador: " + jugador1.Nombre);
                            Application.Exit();
                        }
                        else
                        {
                            MessageBox.Show(jugador1.Nombre + " Es el Ganador \r\n" + "Vidas Restantes " + jugador2.Nombre + " : " + jugador2.vida);
                        }
                    }
                    else
                    {
                        jugador1.vida--;

                        if (jugador1.vida <= 0)
                        {
                            MessageBox.Show("Fin del Juego \r\nGanador: " + jugador2.Nombre);
                            Application.Exit();
                        }
                        else
                        {
                            MessageBox.Show(jugador2.Nombre + " Es el Ganador \r\n" + "Vidas Restantes  " + jugador1.Nombre + " : " + jugador1.vida);
                        }
                    }

                    MessageBox.Show("Turno de Jugador 1 \r\nElija una carta para atacar");
                    ManoJugador2.Enabled = false;
                    SeleccionarCartaJugador1.Enabled = true;
                }
                else
                {
                    if (cartas[0].ataque >= cartas[1].defensa)
                    {
                        jugador2.vida--;

                        if (jugador2.vida <= 0)
                        {
                            MessageBox.Show("Fin del Juego \r\nGanador: " + jugador1.Nombre);
                            Application.Exit();
                        }
                        else
                        {
                            MessageBox.Show(jugador1.Nombre + " Es el Ganador \r\n" + "Vidas Restantes " + jugador2.Nombre + " : " + jugador2.vida);
                        }
                    }
                    else
                    {
                            jugador1.vida--;
                            if (jugador1.vida <= 0)
                            {
                                MessageBox.Show("Fin del Juego \r\nGanador: " + jugador2.Nombre);
                                Application.Exit();
                            }
                            else
                            {
                                MessageBox.Show(jugador2.Nombre + " Es el Ganador \r\n" + "Vidas Restantes  " + jugador1.Nombre + " : " + jugador1.vida);
                            }

                    }
                }
                MessageBox.Show("Turno de Jugador 1 \r\nElija una carta para atacar");
                ManoJugador2.Enabled = false;
                SeleccionarCartaJugador1.Enabled = true;
            }
            else
            {
                string elemento1 = cartas[1].elemento.Trim(); // Eliminamos espacios adicionales
                string elemento2 = cartas[0].elemento.Trim();

                if ((elemento1 == "Hielo" && elemento2 == "Fuego") ||
                    (elemento1 == "Fuego" && elemento2 == "Tierra") ||
                    (elemento1 == "Tierra" && elemento2 == "Electricidad") ||
                    (elemento1 == "Electricidad" && elemento2 == "Hielo"))
                {
                    // Si el elemento gana, el ataque se multiplica * 2
                    cartas[1].ataque *= 2;

                    if (cartas[1].ataque >= cartas[0].defensa)
                    {
                        jugador1.vida--;

                        if (jugador1.vida <= 0)
                        {
                            MessageBox.Show("Fin del Juego \r\nGanador: " + jugador2.Nombre);
                            Application.Exit();
                        }
                        else
                        {
                            MessageBox.Show(jugador2.Nombre + " Es el Ganador \r\n" + "Vidas Restantes " + jugador1.Nombre + " : " + jugador1.vida);
                        }
                    }
                    else
                    {
                        jugador2.vida--;

                        if (jugador2.vida <= 0)
                        {
                            MessageBox.Show("Fin del Juego \r\nGanador: " + jugador1.Nombre);
                            Application.Exit();
                        }
                        else
                        {
                            MessageBox.Show(jugador1.Nombre + " Es el Ganador \r\n" + "Vidas Restantes  " + jugador2.Nombre + " : " + jugador2.vida);
                        }
                    }

                    MessageBox.Show("Turno de Jugador 2 \r\nElija una carta para atacar");
                    ManoJugador1.Enabled = false;
                    SeleccionarCartaJugador2.Enabled = true;
                }
                else
                {
                    if (cartas[1].ataque >= cartas[0].defensa)
                    {
                        jugador1.vida--;

                        if (jugador1.vida <= 0)
                        {
                            MessageBox.Show("Fin del Juego \r\nGanador: " + jugador2.Nombre);
                            Application.Exit();
                        }
                        else
                        {
                            MessageBox.Show(jugador2.Nombre + " Es el Ganador \r\n" + "Vidas Restantes " + jugador1.Nombre + " : " + jugador1.vida);
                        }
                    }
                    else
                    {
                        jugador2.vida--;

                        if (jugador2.vida <= 0)
                        {
                            MessageBox.Show("Fin del Juego \r\nGanador: " + jugador1.Nombre);
                            Application.Exit();
                        }
                        else
                        {
                            MessageBox.Show(jugador1.Nombre + " Es el Ganador \r\n" + "Vidas Restantes  " + jugador2.Nombre + " : " + jugador2.vida);
                        }
                    }
                    MessageBox.Show("Turno de Jugador 1 \r\nElija una carta para atacar");
                    ManoJugador2.Enabled = false;
                    SeleccionarCartaJugador1.Enabled = true;
                }
                

            }

            turno = true;
            Random random = new Random();
            //Eliminar la carta seleccionada de la Mano

            jugador1.mano.RemoveAt(index_mano1);
            jugador2.mano.RemoveAt(index_mano2);
            jugador1.mazo.RemoveAt(index_mano1);
            jugador2.mazo.RemoveAt(index_mano2);
            cartas.Clear();

            int index_mazo1 = jugador1.mazo.Count();
            int index_mazo2 = jugador2.mazo.Count();


            //Agregamos carta del mazo a la Mano del Jugador
            jugador1.mano.Add(jugador1.mazo[random.Next(0, index_mazo1)]);
            jugador2.mano.Add(jugador2.mazo[random.Next(0, index_mazo2)]);

            llenar_listBox();

            index_mazo1 = 0;
            index_mazo2 = 0;
            index_mano2 = 0;
            index_mano1 = 0;
        }
       


    }
}

