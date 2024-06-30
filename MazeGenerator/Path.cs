using MazeGenerator.Utils;
using Random = MazeGenerator.Utils.Random;

namespace MazeGenerator
{
    public class Path
    {
        private readonly Maze _maze;

        private int _subPathTileIndex = 0;

        private readonly List<PointI> _tiles;

        public PointI Last => _tiles[^1];

        public int Length => _tiles.Count;

        public List<Path> SubPaths { get; } = [];

        public static int Number { get; set; } = 0;

        public Path(PointI start, Maze maze)
        {
            Number++;

            _maze = maze;

            _tiles = [start];

            while (CanMove(_tiles[^1], out PointI next))
            {
                var between = next - (next - _tiles[^1]) / 2;

                _tiles.Add(next);

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

        public void CreateSubPaths()
        {
            while ((_maze.TilesCreated < _maze.Size.Product) && (_subPathTileIndex < Length))
            {
                SubPaths.Add(new Path(_tiles[_subPathTileIndex++], _maze));
            }
        }
    }
}
