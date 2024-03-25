using Kotono.Utils.Coordinates;
using Random = Kotono.Utils.Random;

namespace MazeGenerator
{
    public class Maze
    {
        public static Maze? Instance { get; private set; }

        public List<PointI> Tiles { get; } = [];

        public PointI Size { get; }

        public int Width => Size.X;

        public int Height => Size.Y;

        public PointI Start { get; }

        public PointI End { get; }


        public Maze(PointI size)
        {
            Instance = this;

            Size = size;
            Start = Random.PointI(0, Width, 0, Height);

            new Path(Start).CreateSubPaths();
        }

        public void Print()
        {
            Console.Clear();
            Console.WriteLine(this);
            Thread.Sleep(10);
        }

        public override string ToString()
        {
            string result = "";

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (Tiles.Contains(new PointI(x, y))) 
                    {
                        result += ' ';
                    }
                    else
                    {
                        result += '#';
                    }

                    result += ' ';
                }

                result += '\n';
            }

            return result;
        }
    }
}
