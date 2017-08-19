namespace MazeSolver
{
    class Maze
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int EndX { get; set; }
        public int EndY { get; set; }
        public char[][] Description { get; set; }

        public Maze(int width, int height, int startX, int startY, int endX, int endY, char[][] description)
        {
            Width = width;
            Height = height;
            StartX = startX;
            StartY = startY;
            EndX = endX;
            EndY = endY;
            Description = description;
        }
    }
}