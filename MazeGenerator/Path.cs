using Kotono.Utils.Coordinates;
using Random = Kotono.Utils.Random;

namespace MazeGenerator
{
    internal class Path
    {
        private readonly Maze _maze;

        private int _subPathTileIndex = 0;

        internal PointI[] Tiles { get; }

        internal List<Path> SubPaths { get; } = [];

        internal int Length { get; } = 0;

        internal static int Number { get; set; } = 0;

        internal Path(PointI start, Maze maze)
        {
            Number++;

            _maze = maze;

            Tiles = new PointI[maze.Size.Product];
            Tiles[0] = start;

            while (CanMove(Tiles[Length], out PointI next))
            {
                var between = next - (next - Tiles[Length]) / 2;

                Length++;
                Tiles[Length] = next;

                _maze.Tiles[between.X, between.Y] = true;
                _maze.Tiles[next.X, next.Y] = true;
                _maze.TilesCreated++;

                //_maze.Print();
            }

            bool CanMove(in PointI current, out PointI next)
            {
                var neighbors = new PointI[4]
                {
                    new(current.X - 2, current.Y),
                    new(current.X + 2, current.Y),
                    new(current.X, current.Y - 2),
                    new(current.X, current.Y + 2)
                };

                var available = new PointI[4];
                int availableNumber = 0;

                foreach (var neighbor in neighbors)
                {
                    if (neighbor > default(PointI)
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
        }

        internal void CreateSubPaths()
        {
            while ((_maze.TilesCreated < _maze.Size.Product) && (_subPathTileIndex < Length))
            {
                SubPaths.Add(new Path(Tiles[_subPathTileIndex++], _maze));
            }
        }
    }
}
