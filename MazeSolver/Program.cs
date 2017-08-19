using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MazeSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = string.Empty;
            if (args.Length == 0)
            {
                Console.WriteLine("Please enter the path of the maze file to solve.");
                path = Console.ReadLine();
            }
            else
            {
                path = args[0];
            }
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Maze maze = MazeReader.MazeFromFile(path);
            List<Point> solutionPath = MazeSolver.SolveMaze(maze);
            if (solutionPath != null)
                Output.Print(maze.Description, solutionPath);
            else
                Console.WriteLine("No solution was found.");
            stopWatch.Stop();
            Console.WriteLine("Runtime was: " + stopWatch.ElapsedMilliseconds + "ms");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }    
    }
}
