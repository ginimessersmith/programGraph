using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace programGraph
{
    public class Objeto : IGraphics
    {
        public List<Parte> listaPartes { get; set; } = new List<Parte>();
        private Punto centroDeMasa;
        [JsonConstructor]

        public Objeto()
        {
            listaPartes = new List<Parte>();
            centroDeMasa = new Punto(0.0f, 0.0f, 0.0f); 
        }

        public Punto centro { get ; set; }

        public Punto calcularCentroMasa()
        {
            if (listaPartes.Count == 0)
                return new Punto(0, 0, 0);

            float sumX = 0, sumY = 0, sumZ = 0;
            foreach (var parte in listaPartes)
            {
                var c = parte.calcularCentroMasa();
                sumX += c.X;
                sumY += c.Y;
                sumZ += c.Z;
            }
            float n = listaPartes.Count;
            return new Punto(sumX / n, sumY / n, sumZ / n);
        }

        public void Dibujar()
        {
            // Asegúrate de estar en el modelo de vista
            GL.MatrixMode(MatrixMode.Modelview);

            // Guarda la matriz actual
            GL.PushMatrix();

            // Traslada TODO el objeto a su centro lógico
            GL.Translate(centro.X, centro.Y, centro.Z);

            // Dibuja sus partes en coordenadas *locales*
            foreach (var parte in listaPartes)
            {
                parte.Dibujar();
            }

            // Restaura la matriz previa
            GL.PopMatrix();
        }

        public void escalar(float factor)
        {
            foreach (var item in listaPartes) 
            {
                item.setCentro(this.centro);
                item.escalar(factor);
            }

            this.centro = calcularCentroMasa();
        }

        public void rotar(Punto angulo)
        {
            foreach (var item in listaPartes) 
            {
                item.setCentro(this.centro);
                item.rotar(angulo);
            }
        }

        public void setCentro(Punto centro)
        {
            foreach (var item in listaPartes) { 
                item.setCentro(centro); 
            }   
        }

        public void trasladar(Punto valorTralado)
        {
            
            foreach (var item in listaPartes) {
                item.setCentro(this.centro);
                item.trasladar(valorTralado);
            }
            this.centro = calcularCentroMasa();
        }

        public Parte GetParte(int idx) 
        {
            Console.WriteLine(idx);
            Console.WriteLine(listaPartes.Count);
            return listaPartes[idx];    
        }

        public void Addparte(Parte parte)
        {
            listaPartes.Add(parte);
            this.centro = calcularCentroMasa();
            return;
        }

        public List<Parte> Getpartes()
        {
            return listaPartes;
        }

    }
}
