using System;
namespace Project
{


    class Program
    { 
        static void Main(string[] args)
        {
            int rows = 35; // Número de filas (debe ser impar)
            int cols = 35; // Número de columnas (debe ser impar)
            MazeGenerator mazeGenerator = new MazeGenerator(rows, cols);
            mazeGenerator.PrintMaze();
            
            mazeGenerator.JugarPorTurno();
        }
    }
}