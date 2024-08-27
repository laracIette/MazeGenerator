using MazeGenerator.Utils;

namespace MazeGenerator
{
    public static class Program
    {
        public static void Main()
        {
            double averageCreationTime = 0.0;

            const int N = 100;
            for (int i = 0; i < N; i++)
            {
                var maze = new Maze(new Point(100, 100));

                Console.WriteLine(maze.CreationTime);
                averageCreationTime += maze.CreationTime;

                //maze.PrintStats();
                //maze.Print();
            }
            Console.WriteLine($"Average: {averageCreationTime / N}");
        }
    }
}
