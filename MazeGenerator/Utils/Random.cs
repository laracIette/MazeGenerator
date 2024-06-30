using System.Runtime.CompilerServices;

namespace MazeGenerator.Utils
{
    public static class Random
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static System.Random GetRandom(int? seed)
        {
            return seed.HasValue ? new System.Random(seed.Value) : System.Random.Shared;
        }

        /// <summary>
        /// Get an int in range [min, max).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Int(int min, int max, int? seed = null)
        {
            return GetRandom(seed).Next(min, max);
        }

        /// <summary>
        /// Get a Point in range [min, max).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointI PointI(PointI min, PointI max, int? seed = null)
        {
            return new PointI(
                Int(min.X, max.X, seed),
                Int(min.Y, max.Y, seed)
            );
        }
    }
}
