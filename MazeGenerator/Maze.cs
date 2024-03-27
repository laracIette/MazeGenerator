using Kotono.Utils;
using Kotono.Utils.Coordinates;
using Random = Kotono.Utils.Random;

namespace MazeGenerator
{
    public class Maze
    {
        public static Maze? Instance { get; private set; }

        public bool[,] Tiles { get; }

        public PointI Size { get; }

        internal PointI TotalSize => Size * 2 + 1;

        public PointI Start { get; }

        public PointI End { get; }

        internal int TilesCreated { get; set; }

        /// <summary>
        /// Initialize a <see cref="Maze"/> given a size.
        /// </summary>
        /// <param name="size"> The size of the <see cref="Maze"/>, each component of the <see cref="PointI"/> gets clamped in range [2, 100]. </param>
        public Maze(PointI size)
        {
            Instance = this;

            Size = PointI.Clamp(size, 2, 100);
            Start = Random.PointI(0, Size.X, 0, Size.Y) * 2 + 1;

            var stopwatch = new Stopwatch();

            Path? path;
            do
            {
                Tiles = new bool[TotalSize.X, TotalSize.Y];
                TilesCreated = 0;

                path = new Path(Start);

            } while (path.Length < Size.Length);

            End = path.Tiles.Last();

            path.CreateSubPaths();

            var subPaths = path.SubPaths;

            while (TilesCreated < Size.Product)
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

            for (int y = 0; y < TotalSize.Y; y++)
            {
                for (int x = 0; x < TotalSize.X; x++)
                {
                    var pos = new PointI(x, y);

                    if (pos == Start)
                    {
                        result += 'O';
                    }
                    else if (pos == End)
                    {
                        result += 'X';
                    }
                    else if (Tiles[x, y])
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
