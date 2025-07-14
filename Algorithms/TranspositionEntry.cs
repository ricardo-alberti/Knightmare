using Knightmare.Moves;

internal enum BoundType { Exact, LowerBound, UpperBound }

internal class TranspositionEntry
{
    public int Eval { get; }
    public int Depth { get; }
    public BoundType Bound { get; }
    public Move? BestMove { get; }

    public TranspositionEntry(int eval, int depth, BoundType bound, Move? bestMove)
    {
        Eval = eval;
        Depth = depth;
        Bound = bound;
        BestMove = bestMove;
    }
}

internal static class TranspositionTable
{
    private static readonly Dictionary<ulong, TranspositionEntry> table = new();

    public static void Store(ulong key, TranspositionEntry entry)
    {
        if (!table.ContainsKey(key) || table[key].Depth <= entry.Depth)
        {
            table[key] = entry;
        }
    }

    public static bool TryGet(ulong key, out TranspositionEntry? entry)
    {
        return table.TryGetValue(key, out entry);
    }

    public static void Clear() => table.Clear();
}

