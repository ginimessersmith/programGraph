using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace programGraph
{
    internal class Escenario
    {
        private Dictionary<String, Objeto> listaDeObjetos;

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
