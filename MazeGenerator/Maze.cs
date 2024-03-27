using Kotono.Utils;
using Kotono.Utils.Coordinates;
using Random = Kotono.Utils.Random;

namespace MazeGenerator
{
    public class Maze
    {
        public static Maze? Instance { get; private set; }

        public List<PointI> Tiles { get; }

        public PointI Size { get; }

        public int Width => Size.X;

        public int Height => Size.Y;

        public PointI Start { get; }

        public PointI End { get; }

        /// <summary>
        /// Initialize a <see cref="Maze"/> given a size.
        /// </summary>
        /// <param name="size"> The size of the <see cref="Maze"/>, each component of the <see cref="PointI"/> gets clamped in range [2, 100]. </param>
        public Maze(PointI size)
        {
            var stopwatch = new Stopwatch();

            Instance = this;

            Size = PointI.Clamp(size, 2, 100);
            Start = Random.PointI(0, Width, 0, Height);

            Path? path;
            do
            {
                Tiles = [];
                path = new Path(Start);
            } while (path.Length < Size.Length);

            End = path.Tiles.Last();

            path.CreateSubPaths();

            var subPaths = path.SubPaths;

            while (Tiles.Count < Size.Product)
            {
                var copy = subPaths.ToArray();
                subPaths.Clear();
                foreach (var subPath in copy)
                {
                    subPath.CreateSubPaths();
                    subPaths.AddRange(subPath.SubPaths);
                }
            }

            Console.WriteLine($"Elapsed Time : {stopwatch.ElapsedTime} seconds.");
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

                    var pos = new PointI(x, y);

                    if (Tiles.Contains(pos))
                    {
                        if (pos == Start)
                        {
                            result += 'O';
                        }
                        else if (pos == End)
                        {
                            result += 'X';
                        }
                        else
                        {
                            result += ' ';
                        }
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
