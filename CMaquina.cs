using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arquitectura
{
    public struct S_objeto
    {
        public bool activo;
        public int x, y;
    }
    class CMaquina
    {
        public enum estados
        {
            BUSQUEDA,
            NBUSQUEDA,
            IRBATERIA,
            RECARGAR,
            MUERTO,
            ALEATORIO,
        };

        private int Estado;
        private int x, y;
        private S_objeto[] objetos = new S_objeto[10];
        private S_objeto bateria;

        private int indice;

        private int energia;

        public CMaquina()
        {
            Estado = (int)estados.NBUSQUEDA;
            x = 320;
            y = 240;
            indice = -1;
            energia = 800;

        }

        public int CoordX
        {
            get { return x; }
        }
        public int CoordY
        {
            get { return y; }
        }
        public int EstadoM
        {
            get { return Estado; }
        }

        public void Inicializa(ref S_objeto [] Pobjetos, S_objeto Pbateria)
        {
            objetos = Pobjetos;
            bateria = Pbateria;

        }

        public void Control()
        {
            switch (Estado)
            {
                case (int)estados.BUSQUEDA:
                    Busqueda();

                    if (x == objetos[indice].x && y == objetos[indice].y)
                    {
                        objetos[indice].activo = false;
                        Estado = (int)estados.NBUSQUEDA;

                    }
                    else if (energia < 400)
                        Estado = (int)estados.IRBATERIA;
                    break;

                case (int)estados.NBUSQUEDA:
                    NuevaBusqueda();
                    if (indice == -1)
                        Estado = (int)estados.ALEATORIO;
                    else
                        Estado = (int)estados.BUSQUEDA;

                    break;

                case (int)estados.IRBATERIA:
                    IrBateria();
                    if (x == bateria.x && y == bateria.y)
                        Estado = (int)estados.RECARGAR;
                    if (energia == 0)
                        Estado = (int)estados.MUERTO; 

                    break;
                case (int)estados.RECARGAR:
                    Recargar();
                    Estado = (int)estados.BUSQUEDA;
                    break;

                case (int)estados.MUERTO:
                    Muerto();
                    break;

                case (int)estados.ALEATORIO:
                    Aleatorio();
                    if (energia == 0)
                        Estado = (int)estados.MUERTO;
                    break;
            }
        }

        public void Busqueda()
        {
            if (x < objetos[indice].x)
                x++;
            else if (x > objetos[indice].x)
                x--;
            if (y < objetos[indice].y)
                y++;
            else if (y > objetos[indice].y)
                y--;

            energia--;
        }

        public void NuevaBusqueda()
        {
            indice = -1;
            for(int n = 0; n < 10; n++)
            {
                if (objetos[n].activo == true)
                    indice = n;

            }
        }

        public void Aleatorio()
        {
            Random random = new Random();

            int nx = random.Next(0, 3);
            int ny = random.Next(0, 3);

            x += nx - 1;
            y += ny - 1;
            energia--;

        }
        public void IrBateria()
        {
            if (x < bateria.x)
                x++;
            else if (x > bateria.x)
                x--;
            if (y < bateria.y)
                y++;
            else if (y > bateria.y)
                y--;

            energia--;
        }
        public void Recargar()
        {
            energia = 1000;
        }
        public void Muerto()
        {

        }

    }
}
