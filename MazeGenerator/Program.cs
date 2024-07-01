using MazeGenerator.Utils;

namespace MazeGenerator
{
    public static class Program
    {
        public static void Main()
        {
            var maze = new Maze(new Point(32, 18));

            maze.PrintStats();
            maze.Print();
        }
    }
}
