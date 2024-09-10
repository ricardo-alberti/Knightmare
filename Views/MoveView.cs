using Knightmare.Moves;

namespace Knightmare.Views
{
    internal class MoveView
    {
        public MoveView()
        {

        }

        public void Print(Move _move)
        {
            string player = _move.Piece().Side() == PlayerSide.White ? "White" : "Black";

            Console.WriteLine(@$"{_move.Piece().Shape()} from {Notation(_move.Tiles()[0].Position().x, _move.Tiles()[0].Position().y)} to {Notation(_move.Tiles()[1].Position().x, _move.Tiles()[1].Position().y)}");
        }

        private string Notation(int y, int x)
        {
            string ret = "";

            switch (y)
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

            switch (x)
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
