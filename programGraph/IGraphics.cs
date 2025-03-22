using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace programGraph
{
    internal interface IGraphics
    {
        public Punto centro {get;set;}

        public void Dibujar();
        public void setCentro(Punto centro);

        public void rotar(Punto angulo);
        public void escalar(float factor);

        public void trasladar(Punto valorTralado);

        public Punto calcularCentroMasa();

    }
}
