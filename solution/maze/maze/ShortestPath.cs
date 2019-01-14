using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maze
{
    class ShortestPath
    {
        // M x N matrix
        private int M;
        private int N;

        //needed arrays
        private char[,] maze2Ds;
        private Node[,] mazePrevNodes;
        private List<Node> path;
        
        //start end nodes
        private Node start;
        private Node end;

        // stores length of longest path from source to destination
        int min_dist = Int32.MaxValue;

        // Below arrays details all 4 possible movements from a cell
        private int[] row = { -1, 0, 0, 1 };
        private  int[] col = { 0, -1, 1, 0 };

        // Function to check if it is possible to go to position (row, col)
        // from current position. The function returns false if (row, col)
        // is not a valid position or has value 0 or it is already visited
        
        private bool isValid(int[,] mat, bool[,] visited, int row, int col)
        {
            return (row >= 0) && (row < M) && (col >= 0) && (col < N)
                            && mat[row, col] == 0 && !visited[row, col];

        }

        // Find Shortest Possible Route in a matrix mat from source
        // cell (i, j) to destination cell (x, y)
        public void BFS(int[,] mat, int i, int j, int x, int y)
        {
            //set the start node
            start = new Node(i, j, 0);
            
            //drawing the maze
            for (int a = 0; a < mat.GetLength(0); a++)
            {
                for (int s = 0; s < mat.GetLength(1); s++)
                {
                    if (mat[a, s] == 0)
                    {
                        maze2Ds[a, s] = ' ';
                    }
                    else if (mat[a, s] == 1)
                    {
                        maze2Ds[a, s] = '#';
                        
                    }
                }
            }
            //print the end point
            maze2Ds[x,y] = 'E';
            Console.WriteLine("x: " + x + " y: " + y);
            // construct a matrix to keep track of visited cells
            bool[,] visited = new bool[M,N];

            // create an empty queue
            Queue<Node> q = new Queue<Node>();
            // mark source cell as visited and enqueue the source node
            visited[i,j] = true;
            maze2Ds[i, j] = 'S';  
            q.Enqueue(new Node(i, j, 0));
                        
            // run till queue is empty
            while (q.Count != 0)
            {
                // pop front node from queue and process it
                Node node = q.Dequeue();

                // (i, j) represents current cell and dist stores its
                // minimum distance from the source
                i = node.x;
                j = node.y;
                int dist = node.dist;

                // if destination is found, update min_dist and stop
                if (i == x && j == y)
                {
                    min_dist = dist;
                    break;
                }

                // check for all 4 possible movements from current cell
                // and enqueue each valid movement
                for (int k = 0; k < 4; k++)
                {
                    //the following 4 if statements are to check for wrapping
                    int Row = i;
                    int Col = j;
                    if (Row == 0 && k == 0)
                    {
                        Row = mat.GetLength(0) - 1;
                    }
                    else if (Row == mat.GetLength(0) - 1 && k == 3)
                    {
                        Row = 0;
                    }
                    else if (Col == 0 && k == 1)
                    {
                        Col = mat.GetLength(1) - 1;
                    }
                    else if (Col == mat.GetLength(1) - 1 && k == 2)
                    {
                        Col = 0;
                    }
                    else
                    {
                        Row = i + row[k];
                        Col = j + col[k];
                    }
                    // check if it is possible to go to position
                    // (i + row[k], j + col[k]) from current position
                    //if (isValid(mat, visited, i + row[k], j + col[k]))
                    if (isValid(mat, visited, Row , Col))
                    {
                        // mark next cell as visited and enqueue it
                        visited[Row,Col] = true;
                        if (Row == x  && Col == y)
                        {
                            maze2Ds[Row, Col] = 'E';
                            end = new Node(Row, Col, min_dist);
                        }
                        q.Enqueue(new Node(Row, Col, dist + 1));
                        mazePrevNodes[Row, Col] = node;
                    }
                }
            }
            reconstructPath();
            Console.WriteLine(showmaze());
            if (min_dist != Int32.MaxValue)
            {
                Console.WriteLine("The shortest path from source to destination " +
                                         "has length " + min_dist);
            }
            else
            {
                Console.WriteLine("Destination can't be reached from given source");
                if (mat[x, y] == 1)
                {
                    Console.WriteLine("Because the exit is a wall");
                }
            }
        }
        public string showmaze()
        {
            string output = "";
            for (int x = 0; x < maze2Ds.GetLength(0); x++)
            {
                for (int y = 0; y < maze2Ds.GetLength(1); y++)
                {
                    output += maze2Ds[x, y] + " ";
                }
                output += "\n";
            }
            return output;
        }
        public void setSize(int row, int col)
        {
            M = row;
            N = col;
            maze2Ds = new char[row, col];
            mazePrevNodes= new Node[row, col];
        }

        public void reconstructPath()
        {
            path = new List<Node>();
            for(Node at = end; at !=null;at = mazePrevNodes[at.x, at.y])
            {
                path.Add(at);
            }
            path.Reverse();
            
            if(path.Count != 0)
            {
                if (path[0].x == start.x && path[0].y == start.y)
                {
                    foreach (Node node in path)
                    {
                        if (node.x == start.x && node.y == start.y)
                        {
                            maze2Ds[node.x, node.y] = 'S';
                        }
                        else if (node.x == end.x && node.y == end.y)
                        {
                            maze2Ds[node.x, node.y] = 'E';
                        }
                        else
                        {
                            maze2Ds[node.x, node.y] = '*';
                        }
                    }
                }
            }
            
        }
    }
}
