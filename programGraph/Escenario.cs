using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace programGraph
{
    public class Escenario
    {
        public Dictionary<String, Objeto> listaDeObjetos { get; set; } = new Dictionary<string, Objeto>();

        public Escenario()
        {
            listaDeObjetos = new Dictionary<string, Objeto>();
        }

        public void AddObjeto(String name, Objeto objeto)
        {
            listaDeObjetos.Add(name, objeto);
        }

        public void DibujarEscenario()
        {
            foreach (var objeto in listaDeObjetos.Values)
            {
                objeto.Dibujar();   
            }
        }

        public Dictionary<String, Objeto> GetObjetos()
        {
            return listaDeObjetos;
        }
    }
}
