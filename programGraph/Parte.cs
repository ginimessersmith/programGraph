using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace programGraph
{
    public class Parte : IGraphics
    {
        public List<Poligono> listaPoligonos { get; set; } = new List<Poligono>();
        private Punto centroMasa;
        public Punto centro { get; set; }

       public Parte()
        {
            listaPoligonos = new List<Poligono>(); 
            centroMasa = new Punto(0.0f, 0.0f, 0.0f);
        }
        public Punto calcularCentroMasa()
        {
            if (listaPoligonos.Count == 0)
            {
                return new Punto(0.0f, 0.0f, 0.0f);
            }
            else
            {

                float ejeX = 0.0f;
                float ejeY = 0.0f;
                float ejeZ = 0.0f;

                foreach (var valor in listaPoligonos)
                {
                    
                    Poligono poligono = valor;

                    Punto centroPoligono = poligono.calcularCentroMasa();

                    ejeX += centroPoligono.X;
                    ejeY += centroPoligono.Y;
                    ejeZ += centroPoligono.Z;
                }

                int numeroPoligonos = listaPoligonos.Count;
                float promedioEjeX = ejeX / numeroPoligonos;
                float promedioEjeY = ejeY / numeroPoligonos;
                float promedioEjeZ = ejeZ / numeroPoligonos;

                return new Punto(promedioEjeX, promedioEjeY, promedioEjeZ);
            }
        }

        public void Dibujar()
        {
            GL.PushMatrix();
            GL.Translate(centroMasa.X, centroMasa.Y, centroMasa.Z);
            foreach (var poligono in listaPoligonos)
            {
                poligono.Dibujar();
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
            centroMasa = centro;

        }

        public void trasladar(Punto valorTralado)
        {
            throw new NotImplementedException();
        }

        public List<Poligono> GetPoligonos()
        {
            return listaPoligonos;
        }

        public void AddPoligono(Poligono poligono)
        {
            listaPoligonos.Add(poligono);
            this.centro = this.calcularCentroMasa();
        }
    }
}
