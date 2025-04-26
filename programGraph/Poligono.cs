using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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
        public Punto CentroDependiente { get; set; }

        public PrimitiveType primitiveType { get; set; } = new PrimitiveType();
        [JsonConstructor]
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
            this.CentroDependiente = new Punto();
        }

        public Poligono()
        {
            this.centro = new Punto();
            this.puntos = new List<Punto>();
            this.primitiveType = PrimitiveType.LineLoop;
            this.color = new float[] { 0.1f, 0.1f, 0.1f };
            this.CentroDependiente = new Punto();
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
            GL.Begin(PrimitiveType.Polygon);
            foreach (var punto in puntos)
            {
                GL.Vertex3(punto.X, punto.Y, punto.Z);
            }

            GL.End();
            GL.Flush();
        }

        public void escalar(float factor)
        {
            //Matrix4 load = Matrix4.CreateScale((float)factor);
            //Vector4 result;
            //foreach (var item in puntos)
            //{
            //Vector4 vector = new Vector4((float)item.X - (float)centro.X, (float)item.Y - (float)centro.Y, (float)item.Z - (float)centro.Z, 1);
            //result = vector * load;

            //                item.X = result.X + centro.X;
            //              item.Y = result.Y + centro.Y;
            //            item.Z = result.Z + centro.Z;
            //      }

            for (int i = 0; i < puntos.Count; i++)
            {
                var p = puntos[i];
                puntos[i] = new Punto(
                    centro.X + (p.X - centro.X) * factor,
                    centro.Y + (p.Y - centro.Y) * factor,
                    centro.Z + (p.Z - centro.Z) * factor
                );
            }
        }

        public void rotar(Punto angulo)
        {
            var rx = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(angulo.X));
            var ry = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(angulo.Y));
            var rz = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(angulo.Z));
            var transform = rx * ry * rz;

            // Aplicamos la rotación a cada punto
            for (int i = 0; i < puntos.Count; i++)
            {
                var p = puntos[i];
                // Convertimos a vector relativo al centro
                var v = new Vector4(p.X - centro.X, p.Y - centro.Y, p.Z - centro.Z, 1f);
                var r = Vector4.Transform(v, transform);
                // Asignamos el punto rotado de vuelta, re-centrando
                puntos[i] = new Punto(
                    centro.X + r.X,
                    centro.Y + r.Y,
                    centro.Z + r.Z
                );
            }
            // Recalcula el centro si es necesario
            centro = calcularCentroMasa();
        }

        public void setCentro(Punto centro)
        {
            centro = centro;
        }

        public void trasladar(Punto valorTralado)
        {
            //Matrix4 load = Matrix4.CreateTranslation((float)valorTralado.X,(float)valorTralado.Y,(float)valorTralado.Z);
            //Vector4 result;
            //foreach (var item in puntos) {
                //Vector4 vector = new Vector4((float)item.X, (float)item.Y, (float)item.Z,1);
               // result = vector * load;

               // item.X = result.X;
                //item.Y = result.Y;
               // item.Z = result.Z;
            //}
           // this.centro = this.calcularCentroMasa();

            for (int i = 0; i < puntos.Count; i++)
            {
                puntos[i] = new Punto(
                    puntos[i].X + valorTralado.X,
                    puntos[i].Y + valorTralado.Y,
                    puntos[i].Z + valorTralado.Z
                );
            }


        }

        public Punto getCentro()
        {
            return this.calcularCentroMasa();
        }

        public Poligono Clone()
        {
            // Duplica la lista de puntos
            var puntosCopy = this.puntos
                .Select(pt => new Punto(pt.X, pt.Y, pt.Z))
                .ToList();
            // Crea un nuevo Poligono con el mismo color
            return new Poligono(puntosCopy, color[0], color[1], color[2]);
        }

    }
}
