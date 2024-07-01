using MazeGenerator.Utils;
using System;
using System.Collections.Generic;
using System.Threading;
using Random = MazeGenerator.Utils.Random;

namespace MazeGenerator
{
    public class Path
    {
        private readonly Maze _maze;

        private int _subPathTileIndex = 0;

        private readonly List<Point> _tiles = new();

        public Point Last => _tiles[^1];

        public int Length => _tiles.Count;

        public List<Path> SubPaths { get; } = new();

        public static int Number { get; set; } = 0;

        public Path(Point start, Maze maze)
        {
            Number++;

            _maze = maze;

            _tiles.Add(start);

            while (CanMove(_tiles[^1], out Point next))
            {
                var between = next - (next - _tiles[^1]) / 2;

                _tiles.Add(next);

                _maze.Tiles[between.X, between.Y] = true;
                _maze.Tiles[next.X, next.Y] = true;
                _maze.TilesCreated++;

                //PrintStep();
            }

            bool CanMove(in Point current, out Point next)
            {
                var neighbors = new Point[4]
                {
                    new(current.X - 2, current.Y),
                    new(current.X + 2, current.Y),
                    new(current.X, current.Y - 2),
                    new(current.X, current.Y + 2)
                };

                var available = new Point[4];
                int availableNumber = 0;

                foreach (var neighbor in neighbors)
                {
                    if (neighbor > default(Point)
                     && neighbor < _maze.TotalSize
                     && !_maze.Tiles[neighbor.X, neighbor.Y])
                    {
                        available[availableNumber++] = neighbor;
                    }
                }

                if (availableNumber > 0)
                {
                    next = available[Random.Int(0, availableNumber)];
                    return true;
                }

                next = default;
                return false;
            }

            void PrintStep()
            {
                Console.Clear();
                _maze.Print();
                Thread.Sleep(10);
            }
        }

        public void CreateSubPaths()
        {
            while ((_maze.TilesCreated < _maze.Size.Product) && (_subPathTileIndex < Length))
            {
                SubPaths.Add(new Path(_tiles[_subPathTileIndex++], _maze));
            }
        }
    }
}
