using System;
using System.Collections.Generic;
using System.Linq;

namespace MazeSolver
{
    class Output
    {
        public static void Print(char[][] mazeDescription, List<Point> path)
        {
            char[][] outputMaze = mazeDescription;
            outputMaze = PlotPath(outputMaze, path);
            outputMaze = PlotWallsAndSpaces(outputMaze);
            PrintMaze(outputMaze);
        }

        private static char[][] PlotPath(char[][] description, List<Point> path)
        {
            foreach (Point p in path)
            {
                if (p.Equals(path.First()))
                    description[p.Y][p.X] = 'S';
                else if (p.Equals(path.Last()))
                    description[p.Y][p.X] = 'E';
                else
                    description[p.Y][p.X] = 'X';
            }
            return description;
        }

        private static char[][] PlotWallsAndSpaces(char[][] description)
        {
            for (int i = 0; i < description.Length; i++)
            {
                for (int j = 0; j < description[i].Length; j++)
                {
                    if (description[i][j] == '1')
                        description[i][j] = '#';
                    else if (description[i][j] == '0')
                        description[i][j] = ' ';
                }
            }
            return description;
        }

        private static void PrintMaze(char[][] description)
        {
            for (int i = 0; i < description.Length; i++)
            {
                Console.WriteLine(description[i]);
            }
        }
    }
}
