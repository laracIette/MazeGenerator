using MazeGenerator.Utils;
using System.Diagnostics;
using System.IO;
using System.Text;
using Random = MazeGenerator.Utils.Random;
using Stopwatch = MazeGenerator.Utils.Stopwatch;

namespace MazeGenerator
{
    public class Maze
    {
        public BitMatrix Tiles { get; }

        public Point Size { get; }

        public Point TotalSize { get; }

        public Point Start { get; }

        public Point End { get; }

        public int TilesCreated { get; set; }

        public double CreationTime { get; }

        public long MemoryUsed { get; }

        public Path MainPath { get; }

        /// <summary>
        /// Initialize a <see cref="Maze"/> given a size.
        /// </summary>
        /// <param name="size"> The size of the <see cref="Maze"/>, each component of the <see cref="Point"/> gets clamped in range [2, 100]. </param>
        public Maze(Point size, Point? start = null, Point? end = null)
        {
            Size = Point.Clamp(size, 2, 100);
            TotalSize = 2 * Size + 1;

            // The end can't be the same position as the start.
            if (end == start)
            {
                end = null;
            }

            if (start != null)
            {
                Start = 2 * Point.Clamp((Point)start, Point.Zero, Size - 1) + 1;
            }
            else
            {
                Start = 2 * Random.Point(Point.Zero, Size) + 1;
            }

            if (end != null)
            {
                end = 2 * Point.Clamp((Point)end, Point.Zero, Size - 1) + 1;
            }

            var stopwatch = new Stopwatch();

            Path? path;
            do
            {
                do
                {
                    Tiles = new BitMatrix(TotalSize);
                    TilesCreated = 0;
                    Path.Number = 0;

                    path = new Path(Start, this);

                } while (path.Length < Size.Length);

                End = path.Last;

            } while ((end != null) && (End != end));

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

            CreationTime = stopwatch.ElapsedTime;
            MemoryUsed = Process.GetCurrentProcess().WorkingSet64;
            MainPath = path;
        }

        public void Print()
        {
            Console.WriteLine(this);
        }

        public void PrintStats()
        {
            Console.Clear();
            Console.WriteLine(
                $"Elapsed time : {CreationTime} seconds.\n" +
                $"Size : {Size}.\n" +
                $"Start : {(Start - 1) / 2}.\n" +
                $"End : {(End - 1) / 2}.\n" +
                $"Main path length : {MainPath.Length}.\n" +
                $"Paths : {Path.Number}.\n" +
                $"Memory Used : {MemoryUsed / 8000000.0f} MB.\n"
            );
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            for (int y = 0; y < TotalSize.Y; y++)
            {
                for (int x = 0; x < TotalSize.X; x++)
                {
                    var pos = new Point(x, y);

                    if (pos == Start)
                    {
                        result.Append('O');
                    }
                    else if (pos == End)
                    {
                        result.Append('X');
                    }
                    else if (Tiles[x, y])
                    {
                        result.Append(' ');
                    }
                    else
                    {
                        result.Append('#');
                    }

                    result.Append(' ');
                }

                result.Append('\n');
            }

            return result.ToString();
        }
    }
}
