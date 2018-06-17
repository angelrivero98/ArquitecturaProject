using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arquitectura
{
    class Pelota
    {
        public enum Estado
        {
            EN_MOVIMIENTO,
            DETENIDA
        };
        public int x, y, radio;
        public Estado estado { get; set; }
        public Cuadrado objetivo;
        public int potencia;

        public Pelota(int x, int y, int radio)
        {
            this.x = x;
            this.y = y;
            this.radio = radio;
            this.estado = Estado.DETENIDA;
        }

        public Cuadrado getPosition()
        {
            return new Cuadrado(x, y, radio);
        }

        public bool Contiene(int x, int y)
        {
            return x <= this.x + this.radio && y < this.y + this.radio && x >= this.x && y >= this.y;          
        }

        public void mover()
        {
            if (objetivo == null)
                return;

            var tx = objetivo.x;
            var ty = objetivo.y;

            if (this.x < tx)
                x = x + potencia;
            else if (this.x > tx)
                x = x - potencia;
            if (this.y < ty)
                y = y + potencia;
            else if (this.y > ty)
                y = y - potencia;

        }

        public void Control()
        {
            switch (estado)
            {
                case Estado.DETENIDA:
                    break;

                case Estado.EN_MOVIMIENTO:
                    if (objetivo.Contiene(this.x, this.y))
                    {
                        Console.WriteLine("Detenida");
                        this.estado = Estado.DETENIDA;
                        this.objetivo = null;
                        this.potencia = 0;
                    }
                    mover();
                    break;
            }
        }

        public void setObjetivo(Cuadrado c, int potencia)
        {
            this.objetivo = c;
            this.potencia = potencia;
        }


    }
}
