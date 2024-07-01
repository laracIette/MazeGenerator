using System;

namespace MazeGenerator.Utils
{
    public static class Time
    {
        /// <summary>
        /// Current exact UTC Time since Epoch in seconds.
        /// </summary>
        public static double ExactUTC => DateTime.UtcNow.Ticks / (double)TimeSpan.TicksPerSecond;
    }
}
