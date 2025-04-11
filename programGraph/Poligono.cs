using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace programGraph
{
    public class Poligono : IGraphics
    {
        public List<Punto> puntos { get; set; } = new List<Punto>();
        private float[] color;
        public Punto centro { get; set; } = new Punto();
        public PrimitiveType primitiveType { get; set; } = new PrimitiveType();

        public Poligono(List<Punto> listaPuntos, float r, float g, float b)
        {
            this.puntos = new List<Punto>(listaPuntos);
            this.centro = new Punto();
            this.color = new float[] { r, g, b };
        }

        public Poligono(Punto centro)
        {
            this.centro = centro;
            this.puntos = new List<Punto>();
            this.primitiveType = PrimitiveType.LineLoop;
        }

        public Poligono()
        {
            this.centro = new Punto();
            this.puntos = new List<Punto>();
            this.primitiveType = PrimitiveType.LineLoop;
            this.color = new float[] { 0.1f, 0.1f, 0.1f };
        }

        public List<Punto> GetPuntos()
        {
            return puntos;
        }

        public float[] GetColor()
        {
            return color;
        }

        public Punto calcularCentroMasa()
        {
            if (puntos.Count == 0)
            {
                return new Punto(0, 0, 0);
            }
            else
            {
                float minX = puntos.Min(p => p.X);
                float maxX = puntos.Max(p => p.X);

                float minY = puntos.Min(p => p.Y);
                float maxY = puntos.Max(p => p.Y);

                float minZ = puntos.Min(p => p.Z);
                float maxZ = puntos.Max(p => p.Z);

                float nuevoCentroX = (minX + maxX) / 2;
                float nuevoCentroY = (minY + maxY) / 2;
                float nuevoCentroZ = (minZ + maxZ) / 2;

                return new Punto(nuevoCentroX, nuevoCentroY, nuevoCentroZ);
            }
        }

        public void Dibujar()
        {
            GL.Color4(this.color);
            GL.Begin(PrimitiveType.LineLoop);
            foreach (var punto in puntos)
            {
                GL.Vertex3(punto.X, punto.Y, punto.Z);
            }

            GL.End();
            GL.Flush();
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
            throw new NotImplementedException();
        }

        public void trasladar(Punto valorTralado)
        {
            throw new NotImplementedException();
        }

        public Punto getCentro()
        {
            return this.calcularCentroMasa();
        }

    }
}
