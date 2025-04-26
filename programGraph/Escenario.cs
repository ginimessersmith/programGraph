using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace programGraph
{
    public class Escenario
    {
        public Dictionary<String, Objeto> listaDeObjetos { get; set; } = new Dictionary<string, Objeto>();
        public Punto centro { get; set; } = new Punto();
        [JsonConstructor]
        public Escenario()
        {
            listaDeObjetos = new Dictionary<string, Objeto>();
        }

        public void AddObjeto(String name, Objeto objeto)
        {
            listaDeObjetos.Add(name, objeto);
            this.centro = calcularCentroMasa();
            return;
        }

        public void DibujarEscenario()
        {
            if (listaDeObjetos == null)
            {
                Console.WriteLine("Error: datos nulos en Escenario.Dibujar()");
                return;
            }

            foreach (var objeto in listaDeObjetos.Values)
            {
                objeto.Dibujar();   
            }
        }

        public Dictionary<String, Objeto> GetObjetos()
        {
            return listaDeObjetos;
        }

        public Punto calcularCentroMasa() 
        {
            if (listaDeObjetos.Count==0) 
            {
                return new Punto(0,0,0);
            }
            else 
            {
                float sumaX = 0;
                float sumaY = 0;
                float sumaZ = 0;

                foreach (var item in listaDeObjetos) {
                    Punto centro = item.Value.calcularCentroMasa();
                    sumaX += centro.X;
                    sumaY += centro.Y;
                    sumaZ += centro.Z;
                }

                int numObj = listaDeObjetos.Count;
                float promedioX = sumaX/numObj;
                float promedioY = sumaY/numObj;
                float promedioZ = sumaZ/numObj;
                return new Punto(promedioX, promedioY, promedioZ);
            }
        }
    }
}
