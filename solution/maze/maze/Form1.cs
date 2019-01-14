using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace maze
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string maze;
        private int[] maze_width_height = { 0, 0 };
        private int row, col;
        private int[] maze_start_x_y = { 0, 0 };
        private int[] maze_end_x_y = { 0, 0 };
        private int[,] maze2D;
        private char[,] maze2Ds;
        private int[,] path;
        private int[,] passage;
        private int[,] visited;
        private string wall;
        List<List<int>> rawMaze = new List<List<int>>();
        
        bool deadEnd = false;

        private void sltFilebtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Text Files|*.txt";
            openFileDialog1.Title = "Select a Text File";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                maze = File.ReadAllText(openFileDialog1.FileName);
                URLTextField.Text = openFileDialog1.FileName;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {

            solveMaze();
            ShortestPath path = new ShortestPath();
            path.setSize(row, col);
            path.BFS(maze2D, maze_start_x_y[0], maze_start_x_y[1], maze_end_x_y[0], maze_end_x_y[1]);
            path.showmaze();
            //solve(maze_start_x_y[0], maze_start_x_y[1]);
            //toString();
            //checkMovements();
            //Console.WriteLine(path.showmaze());

        }

        public void solveMaze()
        {
            string aLine, aParagraph = null;
            StringReader strReader = new StringReader(maze);
            List<int> step = new List<int>();
            int lineIndex = 0;
            int x = 0;
            while (true)
            {
                aLine = strReader.ReadLine();
                if (aLine != null)
                {

                    string[] splitter = aLine.Split(' ');
                    aParagraph = aParagraph + aLine + " ";                    
                    if (lineIndex == 0)
                    {
                        //maze_width_height[0] = int.Parse(splitter[0]);
                        //maze_width_height[1] = int.Parse(aLine.Substring(splitter[0].Length));
                        col = int.Parse(splitter[0]);
                        row = int.Parse(aLine.Substring(splitter[0].Length));
                        maze2D = new int[row, col];
                        maze2Ds = new char[row, col];
                        //Console.WriteLine("width: " + maze_width_height[0] + " height: " + maze_width_height[1]);//for debugging reasons
                    }
                    else if (lineIndex == 1)
                    {                        
                        maze_start_x_y[1] = int.Parse(splitter[0]);
                        maze_start_x_y[0] = int.Parse(aLine.Substring(splitter[0].Length));
                        //Console.WriteLine("start x: " + maze_start_x_y[0] + " start y: " + maze_start_x_y[1]);//for debugging reasons
                    }
                    else if (lineIndex == 2)
                    {
                        maze_end_x_y[1] = int.Parse(splitter[0]);
                        maze_end_x_y[0] = int.Parse(aLine.Substring(splitter[0].Length));
                        //Console.WriteLine("end x: " + maze_end_x_y[0] + " end y: " + maze_end_x_y[1]);//for debugging reasons
                    }
                    else if (lineIndex >= 3)
                    {
                       string Line = aLine.Replace(" ", String.Empty);
                        //step = new List<int>();
                        int y = 0;

                        foreach (char bit in Line)
                        {
                            //step.Add(int.Parse(word));
                            //Console.WriteLine("Word: "+ word);//for debugging reasons
                            //maze2D[x, y] = int.Parse(bit.ToString());
                            maze2Ds[x, y] = bit;
                            maze2D[x,y] = (int)char.GetNumericValue(bit);
                            y++;
                        }
                        //rawMaze.Add(step);
                        x++;
                    }
                }
                else
                {
                    aParagraph = aParagraph + "\n";
                    break;
                }
                lineIndex++;

            }            
            //Console.WriteLine("Modified text:\n\n{0}", aParagraph);
        }

        public void checkMovements()
        {
            
            for (int i = 0; i < maze2D.GetLength(0); i++)
            {
                for (int j = 0; j < maze2D.GetLength(1); j++)
                {
                    Console.WriteLine("Word: " + maze2D[i, j]);//for debugging reasons
                }
            }
            visited = maze2D;
            int[] currentLocation = maze_start_x_y;
            string direction;
            while (currentLocation != maze_end_x_y)
            {
                if (deadEnd == true)
                {

                }

                if (isMoveRight(currentLocation)==true)
                {
                    currentLocation = MoveRight(currentLocation);
                }
                else if (isMoveLeft(currentLocation))
                {
                    currentLocation = MoveLeft(currentLocation);
                }
                else if (isMoveDown(currentLocation))
                {
                    currentLocation = MoveDown(currentLocation);
                }
                else if (isMoveUp(currentLocation))
                {
                    currentLocation = MoveUp(currentLocation);
                }
                else
                {
                    deadEnd = true;
                }
                visited[currentLocation[0], currentLocation[1]] = 1;

                
                Console.WriteLine(currentLocation[0]+" "+currentLocation[1]);

                //foreach (List<int> row in rawMaze)
                //{
                //    int block = 0;
                //    if (row.Count == maze_start_x_y[0])
                //    {
                //        foreach (int step in row)
                //        {

                //            block++;
                //            Console.WriteLine("step: " + step);
                //        }
                //        if (block == 5)
                //        {

                //        }
                //        Console.WriteLine("Maze: " + row);
                //    }

                //}
            }
        }

        private int[] MoveUp(int[] currentLocation)
        {            
            return checkWrapping("up", currentLocation); 
        }
        private int[] MoveDown(int[] currentLocation)
        {
            return checkWrapping("down", currentLocation);
        }
        private int[] MoveRight(int[] currentLocation)
        {
            return checkWrapping("right", currentLocation);
        }
        private int[] MoveLeft(int[] currentLocation)
        {
            return checkWrapping("left", currentLocation);
        }


        private bool isMoveUp(int[] currentLocation)
        {

            currentLocation = checkWrapping("up", currentLocation);
            if (isVisited(currentLocation) || isWall(currentLocation))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool isMoveDown(int[] currentLocation)
        {
            currentLocation = checkWrapping("down", currentLocation);

            if (isVisited(currentLocation) || isWall(currentLocation))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool isMoveRight(int[] currentLocation)
        {
            currentLocation = checkWrapping("right", currentLocation);
            //checks if visited or if there is a wall
            if (isVisited(currentLocation) || isWall(currentLocation))
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        private bool isMoveLeft(int[] currentLocation)
        {
            currentLocation = checkWrapping("left", currentLocation);
            if (isVisited(currentLocation) || isWall(currentLocation))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool isVisited(int[] currentLocation)
        {
            if (visited[currentLocation[0], currentLocation[1]] == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool isWall(int[] currentLocation)
        {
            if (maze2D[currentLocation[0], currentLocation[1]] == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int[] checkWrapping(string direction, int[] currentLocation)
        {
            switch (direction)
            {
                case "right":
                    //checks for wrapping
                    if (currentLocation[1] != col - 1)
                    {
                        currentLocation[1] = currentLocation[1] + 1;
                    }
                    else
                    {
                        currentLocation[1] = currentLocation[1] - (col - 1);//if we are on the right edge set wrapping
                    }
                    break;


                case "left":
                    //checks for wrapping
                    if (currentLocation[1] != 0)
                    {
                        currentLocation[1] = currentLocation[1] - 1;
                    }
                    else
                    {
                        currentLocation[1] = col - 1;//if we are on the edge set wrapping
                    }
                    break;


                case "up":
                    //checks for wrapping
                    if (currentLocation[0] != 0)
                    {
                        currentLocation[0] = currentLocation[0] - 1;
                    }
                    else
                    {
                        currentLocation[0] = row - 1;//if we are on the edge set wrapping
                    }
                    break;


                case "down":
                    //checks for wrapping
                    if (currentLocation[0] != row - 1)
                    {
                        currentLocation[0] = currentLocation[0] + 1;
                    }
                    else
                    {
                        currentLocation[0] = currentLocation[0] - (row - 1);//if we are on the edge set wrapping
                    }
                    break;
            }
            return currentLocation;

        }



        public int counter = 0;
        
        // Get the start location (x,y) and try to solve the maze
        public void solve(int x, int y)
        {
            if (step(x, y))
            {
                maze2Ds[x,y] = 'S';
            }
        }

        // Backtracking method
        public bool step(int x, int y)
        {

            counter++;

            //System.out.println(this.toString());

            /** Accept case - we found the exit **/
            if (maze_end_x_y[0] == x && maze_end_x_y[1] == y)
            {
                return true;
            }

            /** Reject case - we hit a wall or our path **/
            if (maze2Ds[x,y] == '1' || maze2Ds[x,y] == '*')
            {
                return false;
            }

            /** Backtracking Step **/

            // Mark this location as part of our path
            maze2Ds[x,y] = '*';
            bool result;

            // Try to go Right
            result = step(x, y + 1);
            if (result) { return true; }

            // Try to go Up
            result = step(x - 1, y);
            if (result) { return true; }

            // Try to go Left
            result = step(x, y - 1);
            if (result) { return true; }

            // Try to go Down
            result = step(x + 1, y);
            if (result) { return true; }


            /** Deadend - this location can't be part of the solution **/

            // Unmark this location
            maze2Ds[x,y] = ' ';

            // Go back
            return false;
        }
        
        
    }
}
