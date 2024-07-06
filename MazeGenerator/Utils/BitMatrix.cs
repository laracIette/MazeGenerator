using System.Collections;
using System.Runtime.CompilerServices;

namespace MazeGenerator.Utils
{
    public class BitMatrix(int width, int height)
    {
        private readonly BitArray _bitArray = new(width * height);

        public bool this[int x, int y]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _bitArray[y * width + x];
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _bitArray[y * width + x] = value;
        }

        public BitMatrix(Point size) : this(size.X, size.Y) { }
    }
}
