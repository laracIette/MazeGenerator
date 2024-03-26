using Kotono.Utils.Coordinates;
using Random = Kotono.Utils.Random;

namespace MazeGenerator
{
    public class Path
    {
        private int _index = 0;

        public List<PointI> Tiles { get; }

        public List<Path> SubPaths { get; } = [];

        public int Length => Tiles.Count;

        public Path(PointI start)
        {
            Tiles = [start];

            while (CanMove(Tiles.Last(), out PointI next))
            {
                Tiles.Add(next);
                Maze.Instance!.Tiles.Add(next);

                Maze.Instance!.Print();
            }



            static bool CanMove(in PointI current, out PointI next)
            {
                var neighbors = GetNeighbors(current);

                var available = new List<PointI>();

                foreach (var neighbor in neighbors)
                {
                    if (!Maze.Instance!.Tiles.Contains(neighbor) && (neighbor >= PointI.Zero) && (neighbor < Maze.Instance!.Size))
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
                        new PointI(point.X - 1, point.Y),
                        new PointI(point.X + 1, point.Y),
                        new PointI(point.X, point.Y - 1),
                        new PointI(point.X, point.Y + 1)
                    ];
                }
            }
        }

        public void CreateSubPaths()
        {
            while ((Maze.Instance!.Tiles.Count < Maze.Instance!.Size.Product) && (_index < Tiles.Count))
            {
                SubPaths.Add(new Path(Tiles[_index++]));
            }
        }
    }
}
