using System.Runtime.CompilerServices;

namespace MazeGenerator.Utils
{
    public static class Random
    {
        /// <summary>
        /// Get an int in range [min, max).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Int(int min, int max)
        {
            return System.Random.Shared.Next(min, max);
        }

        /// <summary>
        /// Get a Point in range [min, max).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Point(Point min, Point max)
        {
            return new Point(
                Int(min.X, max.X),
                Int(min.Y, max.Y)
            );
        }
    }
}
