namespace WarTactics.Shared.Helpers
{
    using Microsoft.Xna.Framework;

    public struct IntPoint
    {
        public static IntPoint Zero = new IntPoint(0, 0);

        public IntPoint(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; }

        public int Y { get; }

        public static bool operator ==(IntPoint p1, IntPoint p2)
        {
            return p1.X == p2.X && p1.Y == p2.Y;
        }

        public static bool operator !=(IntPoint p1, IntPoint p2)
        {
            return !(p1 == p2);
        }

        public static implicit operator Vector2(IntPoint p)
        {
            return new Vector2(p.X, p.Y);
        }

        public static implicit operator PointD (IntPoint p)
        {
            return new PointD(p.X, p.Y);
        }

        public static implicit operator IntPoint(Vector2 v)
        {
            return new IntPoint((int)v.X, (int)v.Y);
        }

        public static implicit operator IntPoint(PointD p)
        {
            return new IntPoint((int)p.x, (int)p.y);
        }

        public static implicit operator OffsetCoord(IntPoint p)
        {
            return new OffsetCoord(p.X, p.Y);
        }

        public static implicit operator IntPoint(OffsetCoord oc)
        {
            return new IntPoint(oc.col, oc.row);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is IntPoint point && point == this;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.X * 397) ^ this.Y;
            }
        }
    }
}
