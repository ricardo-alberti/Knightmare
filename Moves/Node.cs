namespace Knightmare.Moves
{
    internal class Node
    {
        public Move? Value { get; set; }
        public int Eval { get; set; }
        public List<Node> Subnodes { get; set; } = new List<Node>();
    }
}
