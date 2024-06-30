using System.Runtime.CompilerServices;

namespace MazeGenerator.Utils
{
    /// <summary>
    /// Class for exact time spans.
    /// </summary>
    public class Stopwatch
    {
        private double _startTime = Time.ExactUTC;

        /// <summary>
        /// Get the exact elapsed time since the Stopwatch started.
        /// </summary>
        public double ElapsedTime => Time.ExactUTC - _startTime;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Start() => _startTime = Time.ExactUTC;

        public override string ToString()
        {
            return $"Start Time: {_startTime}, Elapsed Time: {ElapsedTime}";
        }
    }
}
