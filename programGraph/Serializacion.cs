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
    public static class Serializacion
    {
        private static JsonSerializerOptions _options = new JsonSerializerOptions
        {
            WriteIndented = true,
            IncludeFields = true,
        };
        public static void Save<T>(string name,  T objeto, string path = @"C:\Users\gbg\Desktop\University and College\Programacion Grafica SEM 1-2025\Files") 
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

        public static T Load<T>(string nameObject, string path= @"C:\Users\gbg\Desktop\University and College\Programacion Grafica SEM 1-2025\Files\") 
        {
     
            try
            {
                
                if (!Directory.Exists(path))
                {
                    Console.WriteLine("El directorio no existe");
                    return default;
                }

                string rutaDirectorio = Path.Combine(path, nameObject + ".json");

                if (!File.Exists(rutaDirectorio))
                {
                    Console.WriteLine("El archivo no existe: " + rutaDirectorio);
                    return default;
                }

                string data = File.ReadAllText(rutaDirectorio);
                return JsonSerializer.Deserialize<T>(data);
            }
            catch (Exception ex) 
            {
                Console.WriteLine("-----------------Error");
                Console.WriteLine(ex.ToString());
                return default;
            }

        }
    }
}
