﻿using Kotono.Utils.Coordinates;
using Kotono.Utils.Timing;
using Random = Kotono.Utils.Random;

namespace MazeGenerator
{
    public class Maze
    {
        public bool[,] Tiles { get; }

        public PointI Size { get; }

        internal PointI TotalSize { get; }

        public PointI Start { get; }

        public PointI End { get; }

        internal int TilesCreated { get; set; }

        /// <summary>
        /// Initialize a <see cref="Maze"/> given a size.
        /// </summary>
        /// <param name="size"> The size of the <see cref="Maze"/>, each component of the <see cref="PointI"/> gets clamped in range [2, 100]. </param>
        public Maze(PointI size, PointI? start = null, PointI? end = null)
        {
            Size = PointI.Clamp(size, 2, 100);
            TotalSize = 2 * Size + 1;

            // The end can't be the same position as the start.
            if (end == start)
            {
                end = null;
            }

            if (start != null)
            {
                Start = 2 * PointI.Clamp((PointI)start, PointI.Zero, Size - 1) + 1;
            }
            else
            {
                Start = 2 * Random.PointI(PointI.Zero, Size) + 1;
            }

            if (end != null)
            {
                end = 2 * PointI.Clamp((PointI)end, PointI.Zero, Size - 1) + 1;
            }

            int attempts = 0;

            var stopwatch = new Stopwatch();

            Path? path;
            do
            {
                do
                {
                    Tiles = new bool[TotalSize.X, TotalSize.Y];
                    TilesCreated = 0;
                    Path.Number = 0;
                    attempts++;

                    path = new Path(Start, this);

                } while (path.Length < Size.Length);

                End = path.Tiles[path.Length];

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

            double elapsedTime = stopwatch.ElapsedTime;

            Console.Clear();
            Console.WriteLine(
                $"Elapsed time : {elapsedTime} seconds.\n" +
                $"Start : {(Start - 1) / 2}.\n" +
                $"End : {(End - 1) / 2}.\n" +
                $"Main path length : {path.Length}.\n" +
                $"Main path attempts : {attempts}.\n" +
                $"Paths : {Path.Number}.\n"
            );
            Console.WriteLine(this);
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
