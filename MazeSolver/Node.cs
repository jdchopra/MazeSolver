namespace MazeSolver
{
    class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public double F { get; set; }
        public int G { get; set; }
        public double H { get; set; }
        public bool Traversable { get; set; }
        public Node Parent { get; set; }
    }
}