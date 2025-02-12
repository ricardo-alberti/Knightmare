public readonly struct Point
{
    public int x { get; }
    public int y { get; }

    public Point(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + x.GetHashCode();
            hash = hash * 23 + y.GetHashCode();
            return hash;
        }
    }

    public override bool Equals(object obj)
    {
        return obj is Point other && x == other.x && y == other.y;
    }
}
