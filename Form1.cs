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
        public Image dead = Image.FromFile(@"Images\x.png");
        public Image campoBg = Image.FromFile(@"Images\campo1White.png");
        public Image ballImg = Image.FromFile(@"Images\ball.png");
        public Image bodyPlayer1 = Image.FromFile(@"Images\body1.png");
        public Image leglPlayer1 = Image.FromFile(@"Images\rbody1.png");
        public Image legrPlayer1 = Image.FromFile(@"Images\lbody1.png");

        public Image bodyPlayer2 = Image.FromFile(@"Images\body2.png");
        public Image leglPlayer2 = Image.FromFile(@"Images\lbody2.png");
        public Image legrPlayer2 = Image.FromFile(@"Images\rbody2.png");

        private PictureBox campo = new PictureBox();

        public int CANTIDAD_DE_MAQUINAS = 2;
        private List<CMaquina> maquinas = new List<CMaquina>();

        public S_objeto[] ListaObjetos = new S_objeto[10];
        public S_objeto MiBateria;

        Pelota pelota;
        Jugador jHome, jAway;
        Random random;

        bool legDraw = false;

        public Form1()
        { 
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            Console.WriteLine(this.Width);
            random = new Random();

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

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(ballImg, pelota.x, pelota.y);
            //e.Graphics.DrawImage(corredor, jHome.x, jHome.y, 32, 32);
            if (legDraw)
            {
                e.Graphics.DrawImage(leglPlayer1, jHome.x + 13, jHome.y + 5);
                e.Graphics.DrawImage(legrPlayer2, jAway.x + 13, jAway.y + 5);
            }
            else
            {
                e.Graphics.DrawImage(legrPlayer1, jHome.x - 13, jHome.y + 15);
                e.Graphics.DrawImage(leglPlayer2, jAway.x - 13, jAway.y + 15);
            }

            e.Graphics.DrawImage(bodyPlayer1, jHome.x, jHome.y);
            //e.Graphics.DrawImage(c2, maquina.CoordX - 4, maquina.CoordY - 4, 32, 32);
            e.Graphics.DrawImage(bodyPlayer2, jAway.x, jAway.y);

        }

        private void mnuSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuInicio_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            legTimer.Enabled = true;
        }
        private void mnuParo_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            legTimer.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pelota.Control();
            jHome.Control();
            jAway.Control();
            this.Invalidate();
        }

        private void legTimer_Tick_1(object sender, EventArgs e)
        {
            if (legDraw)
            {
                legDraw = false;
            }
            else
            {
                legDraw = true;
            }
        }

    }
}
