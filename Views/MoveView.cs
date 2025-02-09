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
            string ret = "";

            foreach (Tile tile in _move.Tiles())
            {
                ret += Notation(tile.Position());
            }

            return ret;
        }

        public string Notation(Point _point)
        {
            string ret = "";

            switch (_point.x)
            {
                case 0:
                    ret += 'h';
                    break;
                case 1:
                    ret += 'g';
                    break;
                case 2:
                    ret += 'f';
                    break;
                case 3:
                    ret += 'e';
                    break;
                case 4:
                    ret += 'd';
                    break;
                case 5:
                    ret += 'c';
                    break;
                case 6:
                    ret += 'b';
                    break;
                case 7:
                    ret += 'a';
                    break;
            }

            switch (_point.y)
            {
                case 0:
                    ret += '1';
                    break;
                case 1:
                    ret += '2';
                    break;
                case 2:
                    ret += '3';
                    break;
                case 3:
                    ret += '4';
                    break;
                case 4:
                    ret += '5';
                    break;
                case 5:
                    ret += '6';
                    break;
                case 6:
                    ret += '7';
                    break;
                case 7:
                    ret += '8';
                    break;
            }

            return ret;
        }
    }
}
