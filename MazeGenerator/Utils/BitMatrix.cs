using System.Collections;

namespace MazeGenerator.Utils
{
    public class BitMatrix(int width, int height)
    {
        private readonly BitArray _bitArray = new(width * height);

        public bool this[int x, int y]
        {
            get => _bitArray[y * width + x];
            set => _bitArray[y * width + x] = value;
        }

        public BitMatrix(Point size) : this(size.X, size.Y) { }
    }
}
