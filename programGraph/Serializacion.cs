using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace programGraph
{
    internal class Serializacion
    {
        public static void Save(string name, Objeto objeto, string path = @"C:\Users\gbg\Desktop\University and College\Programacion Grafica SEM 1-2025\Files") 
        {
            
            try
            {
                string data = JsonSerializer.Serialize(objeto);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    Console.WriteLine($"Carpeta creada en: {path}");
                }

                string rutaCompleta = Path.Combine(path, name + ".json");

                File.WriteAllText(rutaCompleta, data);
                //Console.WriteLine(data);
                //Console.WriteLine(path);
                Console.WriteLine("Guardado con exito");    
            }
            catch (Exception ex) 
            {
                Console.WriteLine("-------------------------------ERROR");
                Console.WriteLine(ex.ToString());   
            }
        }

        public static Objeto Load(string nameObject, string path= @"C:\Users\gbg\Desktop\University and College\Programacion Grafica SEM 1-2025\Files\") 
        {

            /*try
            {
                
                if (!Directory.Exists(path))
                {
                    Console.WriteLine("El directorio no existe");
                    return null;
                }

                string rutaDirectorio = Path.Combine(path, nameObject + ".json");

                if (!File.Exists(rutaDirectorio))
                {
                    Console.WriteLine("El archivo no existe: " + rutaDirectorio);
                    return null;
                }

                string data = File.ReadAllText(rutaDirectorio);
                Objeto? objeto = JsonSerializer.Deserialize<Objeto>(data);
                Console.WriteLine("Objeto obtenido con exito");
                return objeto;
            }
            catch (Exception ex) 
            {
                Console.WriteLine("-----------------Error");
                Console.WriteLine(ex.ToString());
               return null;
            }*/

            Objeto objeto = new Objeto();
            path += nameObject + ".json";
            try
            {
                string serie = File.ReadAllText(path);
                objeto = JsonSerializer.Deserialize<Objeto>(serie);
                Console.WriteLine("Objeto obtenido con exito");
            }
            catch (Exception e)
            {
                Console.WriteLine("-----------------Error");
                Console.WriteLine(e.Message);
            }
            return objeto;
        }
    }
}
