using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MazeSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\Jon\Desktop\Junifer - maze_for_candidates 2017\large_input.txt";
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
