using System.Runtime.CompilerServices;

namespace MazeGenerator.Utils
{
    public static class Random
    {
        private static readonly System.Random _random = new();

        /// <summary>
        /// Get an int in range [min, max).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Int(int min, int max)
        {
            return _random.Next(min, max);
        }

        /// <summary>
        /// Get a Point in range [min, max).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point PointI(Point min, Point max)
        {
            return new Point(
                Int(min.X, max.X),
                Int(min.Y, max.Y)
            );
        }
    }
}
