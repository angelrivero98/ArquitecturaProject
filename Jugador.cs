using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arquitectura
{
    class Jugador
    {
        public enum Estado
        {
            SIN_BALON,
            CON_BALON,
            SIN_ENERGIA,
        };

        public enum Direccion
        {
            DERECHA,
            IZQUIERDA
        };

        private Pelota pelota { get; set; }
        public int x, y;
        public int limitX, limitY;
        Estado estado;
        Direccion direccion;
        Random random;
        int vel = 1;
        Cuadrado objetivo;

        public Jugador(int x, int y, Pelota pelota, Estado estado, Random random, int lx, int ly, Direccion direccion)
        {
            this.x = x;
            this.y = y;
            this.pelota = pelota;
            this.estado = estado;
            this.random = random;
            this.limitX = lx;
            this.limitY = ly;
            this.direccion = direccion;
            this.objetivo = pelota.getPosition();
        }

        public void Control()
        {
            switch (estado)
            {
                case Estado.SIN_BALON:
                    vel = random.Next(1, 3);
                    
                    Sin_Balon();
                    if (objetivo.Contiene(this.x+32, this.y+32))
                    {
                        if (Cuadrado.areTheSame(objetivo, pelota.getPosition()))
                            this.estado = Estado.CON_BALON;
                        if (random.Next(0, 20) == 1)
                        {
                            // TODO: Esta posicion random deberia ser inversa, es decir, que el carajo vaya a su arqueria
                            // Tambien hay que limitar el rango, que no se vaya tan lejos de la pelota, solo una pequeña desviacion
                            Console.WriteLine("Random t");
                            this.objetivo = randomTarget();
                        }
                        else
                        {
                            Console.WriteLine("Real t");
                            this.objetivo = pelota.getPosition();
                        }
                    }
                    break;

                case Estado.CON_BALON:
                    Con_Balon();
                    break;
            }
        }

        public void Sin_Balon()
        {
            mover();
            //energia--;
        }

        public void Con_Balon()
        {
            
            pelota.setObjetivo(randomTarget(), 5);
            pelota.estado = Pelota.Estado.EN_MOVIMIENTO;
            this.estado = Estado.SIN_BALON;
        }

        public void moverHaciaBalon()
        {
            if (x < pelota.x)
                x = x + vel;
            else if (x > pelota.x)
                x = x - vel;
            if (y < pelota.y)
                y = y + vel;
            else if (y > pelota.y)
                y = y - vel;
        }

        public void mover()
        {
            if (objetivo == null)
                return;

            var tx = objetivo.x;
            var ty = objetivo.y;

            if (this.x < tx)
                x = x + vel;
            else if (this.x > tx)
                x = x - vel;
            if (this.y < ty)
                y = y + vel;
            else if (this.y > ty)
                y = y - vel;

            vel = vel - vel / 2;
        }

        public Cuadrado randomTarget()
        {
            int nx = 0;
            int ny = 0;
            if (this.direccion == Direccion.DERECHA)
            {
                nx = random.Next(this.x, limitX);
                ny = random.Next(40, limitY);
            }
            else
            {
                nx = random.Next(0, this.x);
                ny = random.Next(40, limitY);
            }  

            return new Cuadrado(nx, ny, 50);
        }
    }
}
