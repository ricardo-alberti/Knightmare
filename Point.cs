public class Point
{
    public int x { get; }
    public int y { get; }

    public Point() : this(0, 0) { }

    public Point(int X, int Y)
    {
        x = X;
        y = Y;
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
        if (obj == null || GetType() != obj.GetType())
            return false;

        Point other = (Point)obj;
        return x == other.x && y == other.y;
    }

    public Point Copy()
    {
        return new Point(x, y);
    }

    public void Print()
    {
        Console.WriteLine($"Point: ({x}, {y})");
        Console.Read();
    }
}
