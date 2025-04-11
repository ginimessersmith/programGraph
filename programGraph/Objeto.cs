using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace programGraph
{
    public class Objeto : IGraphics
    {
        public List<Parte> listaPartes { get; set; } = new List<Parte>();
        private Punto centroDeMasa;

        public Objeto()
        {
            listaPartes = new List<Parte>();
            centroDeMasa = new Punto(0.0f, 0.0f, 0.0f); // Inicialmente en el origen
        }

        public Punto centro { get ; set; }

        public Punto calcularCentroMasa()
        {
            if (listaPartes.Count == 0)
            {
                return new Punto(0.0f, 0.0f, 0.0f);
            }
            else
            {
                float ejeX = 0;
                float ejeY = 0;
                float ejeZ = 0;

                foreach (var valor in listaPartes)
                {
                    Parte parte = valor;
                    ejeX += parte.calcularCentroMasa().Y;
                    ejeY += parte.calcularCentroMasa().Y;
                    ejeZ += parte.calcularCentroMasa().Z;
                }

                int numPartes = listaPartes.Count;
                float promedioEjeX = ejeX / numPartes;
                float promedioEjeY = ejeY / numPartes;
                float promedioEjeZ = ejeZ / numPartes;

                return new Punto(promedioEjeX, promedioEjeY, promedioEjeZ);
            }
        }

        public void Dibujar()
        {
            GL.PushMatrix();
            GL.Translate(centroDeMasa.X, centroDeMasa.Y, centroDeMasa.Z);

            foreach (var parte in listaPartes)
            {
                parte.Dibujar();
            }

            GL.PopMatrix();
        }

        public void escalar(float factor)
        {
            throw new NotImplementedException();
        }

        public void rotar(Punto angulo)
        {
            throw new NotImplementedException();
        }

        public void setCentro(Punto centro)
        {
            centroDeMasa = centro;
        }

        public void trasladar(Punto valorTralado)
        {
            throw new NotImplementedException();
        }

        public void Addparte(Parte parte)
        {
            listaPartes.Add(parte);
            this.centro = calcularCentroMasa();
        }

        public List<Parte> Getpartes()
        {
            return listaPartes;
        }

    }
}
