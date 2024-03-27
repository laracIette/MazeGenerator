using Kotono.Utils.Coordinates;
using Random = Kotono.Utils.Random;

namespace MazeGenerator
{
    internal class Path
    {
        private int _index = 0;

        internal List<PointI> Tiles { get; }

        internal List<Path> SubPaths { get; } = [];

        internal int Length => Tiles.Count;

        internal static int Number { get; set; } = 0;

        private readonly Maze _maze;

        internal Path(PointI start, Maze maze)
        {
            Number++;

            _maze = maze;

            Tiles = [start];

            while (CanMove(Tiles.Last(), out PointI next))
            {
                var between = next - (next - Tiles.Last()) / 2;

                Tiles.Add(next);

                _maze.Tiles[between.X, between.Y] = true;
                _maze.Tiles[next.X, next.Y] = true;
                _maze.TilesCreated++;

                //_maze.Print();
            }



            bool CanMove(in PointI current, out PointI next)
            {
                var neighbors = GetNeighbors(current);

                var available = new List<PointI>();

                foreach (var neighbor in neighbors)
                {
                    if ((neighbor > PointI.Zero)
                     && (neighbor < _maze.TotalSize)
                     && !_maze.Tiles[neighbor.X, neighbor.Y])
                    {
                        available.Add(neighbor);
                    }
                }

                if (available.Count != 0)
                {
                    next = available[Random.Int(0, available.Count)];
                    return true;
                }

                next = PointI.Zero;
                return false;


                static PointI[] GetNeighbors(in PointI point)
                {
                    return
                    [
                        new PointI(point.X - 2, point.Y),
                        new PointI(point.X + 2, point.Y),
                        new PointI(point.X, point.Y - 2),
                        new PointI(point.X, point.Y + 2)
                    ];
                }
            }
        }

        internal void CreateSubPaths()
        {
            while ((_maze.TilesCreated < _maze.Size.Product) && (_index < Tiles.Count))
            {
                SubPaths.Add(new Path(Tiles[_index++], _maze));
            }
        }
    }
}
