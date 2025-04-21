using System.Runtime.InteropServices;

namespace MazeGenerator.Utils
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Point : IEquatable<Point>
    {
        /// <summary> 
        /// The X component of the PointI. 
        /// </summary>
        public int X = 0;

        /// <summary>
        /// The Y component of the PointI. 
        /// </summary>
        public int Y = 0;

        /// <summary> 
        /// The length component of the PointI. 
        /// </summary>
        public readonly float Length => float.Sqrt(X * X + Y * Y);

        /// <summary>
        /// The X * Y product of the PointI.
        /// </summary>
        public readonly int Product => X * Y;

        public static Point Zero => new(0, 0);

        public Point()
        {
            X = 0;
            Y = 0;
        }

        public Point(Point p)
        {
            X = p.X;
            Y = p.Y;
        }

        public Point(int i)
        {
            X = i;
            Y = i;
        }

        public Point(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }

        public static Point operator +(Point p, int i)
        {
            p.X += i;
            p.Y += i;
            return p;
        }

        public static Point operator -(Point left, Point right)
        {
            left.X -= right.X;
            left.Y -= right.Y;
            return left;
        }

        public static Point operator -(Point p, int i)
        {
            p.X -= i;
            p.Y -= i;
            return p;
        }

        public static Point operator -(Point p)
        {
            p.X = -p.X;
            p.Y = -p.Y;
            return p;
        }

        public static Point operator *(Point left, Point right)
        {
            left.X *= right.X;
            left.Y *= right.Y;
            return left;
        }

        public static Point operator *(int i, Point p)
        {
            p.X *= i;
            p.Y *= i;
            return p;
        }

        [Obsolete("Reorder operands, use \"PointI.operator *(int, PointI)\" instead.")]
        public static Point operator *(Point p, int i)
        {
            return i * p;
        }

        public static Point operator /(Point left, Point right)
        {
            left.X /= right.X;
            left.Y /= right.Y;
            return left;
        }

        public static Point operator /(Point p, int i)
        {
            p.X /= i;
            p.Y /= i;
            return p;
        }

        public static Point operator /(Point p, float f)
        {
            p.X = (int)(p.X / f);
            p.Y = (int)(p.Y / f);
            return p;
        }

        public static bool operator >(Point left, Point right)
        {
            return left.X > right.X
                && left.Y > right.Y;
        }

        public static bool operator <(Point left, Point right)
        {
            return left.X < right.X
                && left.Y < right.Y;
        }

        public static bool operator >=(Point left, Point right)
        {
            return left.X >= right.X
                && left.Y >= right.Y;
        }

        public static bool operator <=(Point left, Point right)
        {
            return left.X <= right.X
                && left.Y <= right.Y;
        }

        public static bool operator ==(Point left, Point right)
        {
            return left.X == right.X
                && left.Y == right.Y;
        }

        public static bool operator !=(Point left, Point right)
        {
            return left.X != right.X
                || left.Y != right.Y;
        }

        public override readonly bool Equals(object? obj)
        {
            return obj is Point p && Equals(p);
        }

        public readonly bool Equals(Point other)
        {
            return X == other.X
                && Y == other.Y;
        }

        public override readonly string ToString()
        {
            return $"X: {X}, Y: {Y}";
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static Point Clamp(Point p, int min, int max)
        {
            return Clamp(p, new Point(min), new Point(max));
        }

        public static Point Clamp(Point p, Point min, Point max)
        {
            p.X = Math.Clamp(p.X, min.X, max.X);
            p.Y = Math.Clamp(p.Y, min.Y, max.Y);
            return p;
        }
    }
}
