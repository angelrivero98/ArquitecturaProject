﻿using System;
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
        public Image corredor = Image.FromFile("C:/Users/Angel/Documents/ProyectoArquitectura-competencia/Properties/corredor.png");
        public Image bateria = Image.FromFile("C:/Users/Angel/Documents/ProyectoArquitectura-competencia/Properties/pila.png");
        public Image dot = Image.FromFile("C:/Users/Angel/Documents/ProyectoArquitectura-competencia/Properties/dot.png");
        public Image dead = Image.FromFile("C:/Users/Angel/Documents/ProyectoArquitectura-competencia/Properties/X.png");
           
        public int CANTIDAD_DE_MAQUINAS = 2;
        private List<CMaquina> maquinas = new List<CMaquina>();

        public S_objeto[] ListaObjetos = new S_objeto[10];
        public S_objeto MiBateria;

        public Form1()
        { 
            InitializeComponent();

            Random random = new Random();
            
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
                e.Graphics.DrawImage(dead,maquina.CoordX-4,maquina.CoordY-4,16,16);
            else
              e.Graphics.DrawImage(corredor,maquina.CoordX-4,maquina.CoordY-4,32,32);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {   
            
            foreach (var maquina in maquinas)
            {
                PaintMaquina(maquina, e);
            }

            for (int n=0;n<10;n++)
                if(ListaObjetos[n].activo==true)
                    e.Graphics.DrawImage(dot, ListaObjetos[n].x - 4, ListaObjetos[n].y - 4, 32, 32);

            e.Graphics.DrawImage(bateria, MiBateria.x - 4, MiBateria.y - 4, 32, 32);       
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
            int i = 0;
            foreach (var maquina in maquinas)
            {
                maquina.Control();
                stateList.Items[i] = String.Format("Estado{0} -> {1}", i, maquina.EstadoM);
                i++;
            }

            this.Invalidate();

            
        }

    }
}
