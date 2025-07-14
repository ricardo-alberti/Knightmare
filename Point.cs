public readonly record struct Point
{
    public int x { get; }
    public int y { get; }

    public Point(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
}
