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
                var between = next - (next - Tiles.Last()) / 2;

                Tiles.Add(next);

                Maze.Instance!.Tiles[between.X, between.Y] = true;
                Maze.Instance!.Tiles[next.X, next.Y] = true;
                Maze.Instance!.TilesCreated++;

                //Maze.Instance!.Print();
            }



            static bool CanMove(in PointI current, out PointI next)
            {
                var neighbors = GetNeighbors(current);

                var available = new List<PointI>();

                foreach (var neighbor in neighbors)
                {
                    if ((neighbor > PointI.Zero)
                     && (neighbor < Maze.Instance!.TotalSize)
                     && !Maze.Instance!.Tiles[neighbor.X, neighbor.Y])
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

        public void CreateSubPaths()
        {
            while ((Maze.Instance!.TilesCreated < Maze.Instance!.Size.Product) && (_index < Tiles.Count))
            {
                SubPaths.Add(new Path(Tiles[_index++]));
            }
        }
    }
}
