using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arquitectura
{
    class Cuadrado
    {
        public int x, y, alto;

        public Cuadrado(int x, int y, int h)
        {
            this.x = x;
            this.y = y;
            this.alto = h;
        }

        public bool Contiene(int tx, int ty)
        {
            return tx <= this.x + this.alto && ty < this.y + this.alto && tx >= this.x && ty >= this.y;
        }

        public static Boolean areTheSame(Cuadrado c1, Cuadrado c2)
        {
            return c1.x == c2.x && c1.y == c2.y;
        }
    }
}
