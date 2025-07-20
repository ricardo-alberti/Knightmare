using Knightmare.Moves;
using Knightmare.Boards;

namespace Knightmare.Views
{
    internal class MoveView
    {
        public void Print(Move _move)
        {
            Console.WriteLine($"bestmove {MoveToString(_move)}");
        }

        public string MoveToString(Move _move)
        {
            if (_move == null) throw new ArgumentException();

            string ret = "";

            foreach (Tile tile in _move.Tiles())
            {
                ret += Notation(tile.Position);
            }

            return ret;
        }

        public string Notation(Point _point)
        {
            string ret = "";
            ret += (char)(_point.x + 'a');
            ret += (char)('8' - _point.y);
            return ret;
        }
    }
}
