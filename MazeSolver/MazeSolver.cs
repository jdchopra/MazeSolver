using System;
using System.Collections.Generic;
using System.Linq;

namespace MazeSolver
{
    /* ALGORITHM
     *  Solve maze using an implementation of A*. A modification to A* that terminates
     *  when the goal is added to the open list can be used in this scenario as the
     *  movement cost of each move (N, E, S, W) is always constant.
     */
    class MazeSolver
    {
        public static List<Point> SolveMaze(Maze maze)
        {
            // Initialise values
            var openList = new List<Node>();
            var closedList = new List<Node>();
            var startNode = new Node { X = maze.StartX, Y = maze.StartY, G = 0 }; // G is always 0 at the start position
            var endNode = new Node { X = maze.EndX, Y = maze.EndY };
            bool foundSolution = false;

            // Add starting node to open list
            startNode.F = CalculateF(startNode, startNode, endNode); // startNode is passed twice as at the start it is also the current node
            openList.Add(startNode);

            while (openList.Count > 0) // If the openList reaches 0, the maze is unsolvable
            {
                // Check if openList contains end point
                if (openList.FirstOrDefault(n => n.X == maze.EndX && n.Y == maze.EndY) != null)
                {
                    foundSolution = true;
                    break;
                }
                Node currentNode = SelectNodeWithLowestF(openList);
                closedList.Add(currentNode);
                openList.Remove(currentNode);

                // Search the four adjacent squares
                List<Node> traversableAdjacentNodes = GetTraversableAdjacentNodes(currentNode, maze);
                foreach (Node node in traversableAdjacentNodes)
                {
                    if(NodeIsInList(node, closedList))
                    {
                        continue; // If this node is already closed, ignore it
                    }
                    if(!NodeIsInList(node, openList)) // If the node is not in the open list, set parent, compute scores and add it
                    {
                        Node newOpenNode = UpdateParentAndScores(node, currentNode, startNode, endNode);
                        openList.Add(newOpenNode);
                    }
                    else // If it is in the open list check if routing through the current node is faster than the previous route
                    {
                        // Select the node in the open list
                        Node openNode = openList.First(n => n.X == node.X && n.Y == node.Y);
                        if (node.Parent != null && node.Parent.G + 1 < openNode.G )
                        {
                            openNode = UpdateParentAndScores(openNode, currentNode, startNode, endNode);
                        }
                    }
                }
            }

            if (foundSolution)
            {
                List<Point> path = FindPath(openList, maze);
                return path;
            }
            else
            {
                return null;
            }
        }

        /* HEURISTIC
         *  As movement is limited to four directions, the Manhattan heuristic is
         *  appropriate here. To break ties a slight consideration of the cross product
         *  between the direct path from the start to end goal and the path between the
         *  current node and end goal is made. This results in the algorithm following a
         *  much more efficient searching path through sparse/open mazes.
         */
        private static double ManhattanWithCrossProduct(Node currentNode, Node startNode, Node endNode)
        {
            int dxCurrent = currentNode.X - endNode.X;
            int dyCurrent = currentNode.Y - endNode.Y;
            int dxInitial = startNode.X - endNode.X;
            int dyInitial = startNode.X - endNode.X;

            int h = Math.Abs(dxCurrent) + Math.Abs(dyCurrent);
            int crossProduct = Math.Abs(dxCurrent * dyInitial - dxInitial * dyCurrent);
            double hWithCrossProduct = h + crossProduct * 0.001;

            return hWithCrossProduct;
        }

        /* PATH SCORING
         *  Add the movement cost from the start position to the current position, to the heuristic
         *  score from the current position to the end goal to evaluate the chosen path
         */
        private static double CalculateF(Node currentNode, Node startNode, Node endNode)
        {
            return currentNode.G + ManhattanWithCrossProduct(currentNode, startNode, endNode);
        }

        /* NODE SELECTION
         *  Select the lowest F cost square in the list, if there are still ties despite the
         *  cross product heuristic, selecting the first node that matches the query is enough
         */
        private static Node SelectNodeWithLowestF(List<Node> list)
        {
            return list.OrderBy(n => n.F).First();
        }

        /* MOVE VALIDATION
         *  This method checks for valid moves, ensuring that the adjacent nodes are reachable
         */
        private static List<Node> GetTraversableAdjacentNodes(Node currentNode, Maze maze)
        {
            var adjacentNodes = new List<Node>()
            {
                new Node { X = currentNode.X, Y = currentNode.Y + 1 },
                new Node { X = currentNode.X, Y = currentNode.Y - 1 },
                new Node { X = currentNode.X + 1, Y = currentNode.Y },
                new Node { X = currentNode.X - 1, Y = currentNode.Y }
            };

            // Check proposed nodes are in the maze and pathable
            var traversableAdjacentNodes = new List<Node>();
            foreach (Node n in adjacentNodes)
            {
                bool withinBounds = n.X < maze.Width && n.Y < maze.Height;
                if (withinBounds && maze.Description[n.Y][n.X] == '0')
                {
                    traversableAdjacentNodes.Add(n);
                }
            }
            return traversableAdjacentNodes;
        }

        private static bool NodeIsInList(Node node, List<Node> list)
        {
            return (list.FirstOrDefault(n => n.X == node.X && n.Y == node.Y) != null);
        }

        private static Node UpdateParentAndScores(Node node, Node parentNode, Node startNode, Node endNode)
        {
            node.Parent = parentNode;
            node.G = node.Parent.G + 1; // Using 1 for movement cost instead of the standard 10 as no diagonal movement makes 10/14 redundant
            node.H = ManhattanWithCrossProduct(node, startNode, endNode);
            node.F = node.G + node.H;
            return node;
        }

        private static List<Point> FindPath(List<Node> list, Maze maze)
        {
            var path = new List<Point>();
            Node node = list.FirstOrDefault(n => n.X == maze.EndX && n.Y == maze.EndY);
            while (node.Parent != null)
            {
                path.Add(new Point(node.X, node.Y));
                node = node.Parent;
            }
            path.Add(new Point(maze.StartX, maze.StartY)); // The starting point is also required in the path for output
            path.Reverse();
            return path;
        }
    }
}
