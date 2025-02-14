using Knightmare.Moves;
using Knightmare.Boards;

namespace Knightmare.Views
{
    internal class MoveView
    {
        public MoveView()
        {

        }

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

            switch (_point.x)
            {
                case 0:
                    ret += 'a';
                    break;
                case 1:
                    ret += 'b';
                    break;
                case 2:
                    ret += 'c';
                    break;
                case 3:
                    ret += 'd';
                    break;
                case 4:
                    ret += 'e';
                    break;
                case 5:
                    ret += 'f';
                    break;
                case 6:
                    ret += 'g';
                    break;
                case 7:
                    ret += 'h';
                    break;
            }

            switch (_point.y)
            {
                case 0:
                    ret += '8';
                    break;
                case 1:
                    ret += '7';
                    break;
                case 2:
                    ret += '6';
                    break;
                case 3:
                    ret += '5';
                    break;
                case 4:
                    ret += '4';
                    break;
                case 5:
                    ret += '3';
                    break;
                case 6:
                    ret += '2';
                    break;
                case 7:
                    ret += '1';
                    break;
            }

            return ret;
        }
    }
}
