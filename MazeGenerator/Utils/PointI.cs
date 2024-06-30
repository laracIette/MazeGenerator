using System.Runtime.InteropServices;

namespace MazeGenerator.Utils
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PointI : IEquatable<PointI>
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

        public static PointI Zero => new(0, 0);

        public PointI()
        {
            X = 0;
            Y = 0;
        }

        public PointI(PointI p)
        {
            X = p.X;
            Y = p.Y;
        }

        public PointI(int i)
        {
            X = i;
            Y = i;
        }

        public PointI(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }

        public static PointI operator +(PointI p, int i)
        {
            p.X += i;
            p.Y += i;
            return p;
        }

        public static PointI operator -(PointI left, PointI right)
        {
            left.X -= right.X;
            left.Y -= right.Y;
            return left;
        }

        public static PointI operator -(PointI p, int i)
        {
            p.X -= i;
            p.Y -= i;
            return p;
        }

        public static PointI operator -(PointI p)
        {
            p.X = -p.X;
            p.Y = -p.Y;
            return p;
        }

        public static PointI operator *(PointI left, PointI right)
        {
            left.X *= right.X;
            left.Y *= right.Y;
            return left;
        }

        public static PointI operator *(int i, PointI p)
        {
            p.X *= i;
            p.Y *= i;
            return p;
        }

        [Obsolete("Reorder operands, use \"PointI.operator *(int, PointI)\" instead.")]
        public static PointI operator *(PointI p, int i)
        {
            return i * p;
        }

        public static PointI operator /(PointI left, PointI right)
        {
            left.X /= right.X;
            left.Y /= right.Y;
            return left;
        }

        public static PointI operator /(PointI p, int i)
        {
            p.X /= i;
            p.Y /= i;
            return p;
        }

        public static PointI operator /(PointI p, float f)
        {
            p.X = (int)(p.X / f);
            p.Y = (int)(p.Y / f);
            return p;
        }

        public static bool operator >(PointI left, PointI right)
        {
            return left.X > right.X
                && left.Y > right.Y;
        }

        public static bool operator <(PointI left, PointI right)
        {
            return left.X < right.X
                && left.Y < right.Y;
        }

        public static bool operator >=(PointI left, PointI right)
        {
            return left.X >= right.X
                && left.Y >= right.Y;
        }

        public static bool operator <=(PointI left, PointI right)
        {
            return left.X <= right.X
                && left.Y <= right.Y;
        }

        public static bool operator ==(PointI left, PointI right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PointI left, PointI right)
        {
            return !(left == right);
        }

        public override readonly bool Equals(object? obj)
        {
            return obj is PointI p && Equals(p);
        }

        public readonly bool Equals(PointI other)
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

        public static PointI Clamp(PointI p, int min, int max)
        {
            return Clamp(p, new PointI(min), new PointI(max));
        }

        public static PointI Clamp(PointI p, PointI min, PointI max)
        {
            p.X = Math.Clamp(p.X, min.X, max.X);
            p.Y = Math.Clamp(p.Y, min.Y, max.Y);
            return p;
        }
    }
}
