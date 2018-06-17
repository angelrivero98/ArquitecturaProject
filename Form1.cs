using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arquitectura
{
    public partial class Form1 : Form
    {
        public Image corredor = Image.FromFile(@"Images\corredor.png");
        public Image bateria = Image.FromFile(@"Images\pila.png");
        public Image dot = Image.FromFile(@"Images\dot.png");
        public Image dead = Image.FromFile(@"Images\x.png");
        public Image campoBg = Image.FromFile(@"Images\campo1.png");
        public Image ballImg = Image.FromFile(@"Images\ball.png");

        private PictureBox campo = new PictureBox();

        public int CANTIDAD_DE_MAQUINAS = 2;
        private List<CMaquina> maquinas = new List<CMaquina>();

        public S_objeto[] ListaObjetos = new S_objeto[10];
        public S_objeto MiBateria;

        Pelota pelota;
        Jugador jHome, jAway;

        public Form1()
        { 
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            Console.WriteLine(this.Width);
            Random random = new Random();

            this.pelota = new Pelota(this.Width / 2, this.Height / 2 , 40);
            this.jHome = new Jugador(this.Width / 2 - 300, this.Height / 2, pelota, Jugador.Estado.SIN_BALON, random, this.Width, this.Height, Jugador.Direccion.DERECHA);
            this.jAway = new Jugador(this.Width / 2 + 230, this.Height / 2, pelota, Jugador.Estado.SIN_BALON, random, this.Width, this.Height, Jugador.Direccion.IZQUIERDA);

            timer1.Interval = 15;
            for (int n = 0; n < 10; n++)
            {
                //639
                //479
                ListaObjetos[n].x = random.Next(30, 400);
                ListaObjetos[n].y = random.Next(30, 400);

                ListaObjetos[n].activo = true;
            }

            MiBateria.x = random.Next(0, 639);
            MiBateria.y = random.Next(0, 479);
            MiBateria.activo = true;

            for (int i = 0; i < CANTIDAD_DE_MAQUINAS; i++)
            {
                var m = new CMaquina(random.Next(150, 300), random.Next(100, 300), ListaObjetos, MiBateria);
                maquinas.Add(m);
                stateList.Items.Add(String.Format("Estado{0} -> {1}", i, m.EstadoM));
            }
                            
        }

        private void PaintMaquina(CMaquina maquina, PaintEventArgs e)
        {              
            if (maquina.EstadoM == (int)CMaquina.estados.MUERTO)
                e.Graphics.DrawImage(dead,maquina.CoordX-4,maquina.CoordY-4,32,32);
            else
              e.Graphics.DrawImage(corredor,maquina.CoordX-4,maquina.CoordY-4,32,32);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(ballImg, pelota.x, pelota.y);
            e.Graphics.DrawImage(corredor, jHome.x, jHome.y, 32, 32);
            e.Graphics.DrawImage(corredor, jAway.x, jAway.y, 32, 32);

        }

        private void mnuSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuInicio_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }
        private void mnuParo_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pelota.Control();
            jHome.Control();
            jAway.Control();
            this.Invalidate();
        }

    }
}
