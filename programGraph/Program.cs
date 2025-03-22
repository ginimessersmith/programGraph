
using System;
using OpenTK;
using programGraph;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("hola mundo");

            using (Game game = new(800, 600))
            {
                game.Run();
            }
        }
    }
}

