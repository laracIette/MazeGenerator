using System.Collections;

namespace MazeGenerator.Utils
{
    public class BitMatrix
    {
        private readonly int _width;

        private readonly int _height;

        private readonly BitArray _bitArray;

        public bool this[int x, int y]
        {
            get => _bitArray[y * _width + x];
            set => _bitArray[y * _width + x] = value;
        }

        public BitMatrix(int width, int height)
        {
            _width = width;
            _height = height;

            _bitArray = new BitArray(width * height);
        }

        public BitMatrix(Point size) : this(size.X, size.Y) { }
    }
}
