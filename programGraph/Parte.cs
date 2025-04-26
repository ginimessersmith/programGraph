using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using System.Text.Json.Serialization;

namespace programGraph
{
    public class Parte : IGraphics
    {
        public List<Poligono> listaPoligonos { get; set; } = new List<Poligono>();
        public Punto centro { get; set; }
        [JsonConstructor]
        public Parte()
        {
            listaPoligonos = new List<Poligono>();
            centro = new Punto(0.0f, 0.0f, 0.0f);
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
            GL.MatrixMode(MatrixMode.Modelview);

            // Guardamos la matriz actual
            GL.PushMatrix();

            // Movemos toda la parte a su posición lógica
            GL.Translate(centro.X, centro.Y, centro.Z);

            foreach (var poligono in listaPoligonos)
            {
                poligono.Dibujar();
            }

            GL.PopMatrix();
        }

        public void escalar(float factor)
        {
            foreach (var item in listaPoligonos) 
            {
                item.setCentro(this.centro);
                item.escalar(factor);
            }
            this.centro = calcularCentroMasa();
        }

        public void rotar(Punto angulo)
        {
            foreach (var item in listaPoligonos) 
            {
                item.setCentro(this.centro);
                item.rotar(angulo);
            }
        }

        public void setCentro(Punto centro)
        {
            foreach (var item in listaPoligonos) { 
                item.setCentro(centro);
            }
            this.centro = centro;
        }

        public void trasladar(Punto valorTralado)
        {
            
            foreach (var item in listaPoligonos) { 
                item.setCentro(this.centro);
                item.trasladar(valorTralado);
                
            }
            this.centro = calcularCentroMasa();

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
