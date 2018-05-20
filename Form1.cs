using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arquitectura
{
    public partial class Form1 : Form
    {
        private CMaquina maquina = new CMaquina();

        public S_objeto[] ListaObjetos = new S_objeto[10];
        public S_objeto MiBateria;

        public Form1()
        {
           
            InitializeComponent();
            
            Random random = new Random();

            for (int n = 0; n < 10; n++)
            {
                ListaObjetos[n].x = random.Next(0, 639);
                ListaObjetos[n].y = random.Next(0, 479);

                ListaObjetos[n].activo = true;
            }

            MiBateria.x = random.Next(0, 639);
            MiBateria.y = random.Next(0, 479);
            MiBateria.activo = true;
            maquina.Inicializa(ref ListaObjetos, MiBateria);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Font fuente = new Font("Arial", 16);
            SolidBrush brocha = new SolidBrush(Color.Black);
            if (maquina.EstadoM == (int)CMaquina.estados.MUERTO)
                e.Graphics.DrawRectangle(Pens.Black, maquina.CoordX - 4, maquina.CoordY - 4, 8, 8);
            else
                e.Graphics.DrawRectangle(Pens.Green, maquina.CoordX - 4, maquina.CoordY - 4, 8, 8);
            for (int n=0;n<10;n++)
                if(ListaObjetos[n].activo==true)
                    e.Graphics.DrawRectangle(Pens.Indigo, ListaObjetos[n].x - 4, ListaObjetos[n].y - 4, 8, 8);

            e.Graphics.DrawRectangle(Pens.IndianRed, MiBateria.x - 4, MiBateria.y - 4, 8, 8);

            e.Graphics.DrawString("Estado -> "+maquina.EstadoM.ToString() , fuente, brocha, 10, 10);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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

        }
    }
}
