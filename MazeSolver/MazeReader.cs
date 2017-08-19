using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MazeSolver
{
    class MazeReader
    {
        public static Maze MazeFromFile(string path)
        {
            // Load the text file into memory
            string[] fileLines = File.ReadAllLines(path);

            // Seperate the parameter data from the maze data
            string[] parameterLines = fileLines
                .Take(3)
                .ToArray();

            char[][] mazeDescription = fileLines
                .Skip(3)
                .Select(line => Regex.Replace(line, @"\s", "").ToCharArray()) // Strip whitespace from each maze row and seperate into chars
                .ToArray();

            // Extract values from parameter lines
            int width = Int32.Parse(parameterLines[0].Split(' ')[0]);
            int height = Int32.Parse(parameterLines[0].Split(' ')[1]);
            int startX = Int32.Parse(parameterLines[1].Split(' ')[0]);
            int startY = Int32.Parse(parameterLines[1].Split(' ')[1]);
            int endX = Int32.Parse(parameterLines[2].Split(' ')[0]);
            int endY = Int32.Parse(parameterLines[2].Split(' ')[1]);

            return new Maze(width, height, startX, startY, endX, endY, mazeDescription);
        }
    }
}
