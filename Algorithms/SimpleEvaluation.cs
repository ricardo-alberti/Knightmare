using Knightmare.Boards;

namespace Knightmare.Algorithm
{
    internal class SimpleEvaluation : IEvaluation
    {
        public int Execute(Board _position)
        {
            int eval = 0;

            foreach (var piece in _position.WhitePieces.Values)
                eval += piece.Value;

            foreach (var piece in _position.BlackPieces.Values)
                eval -= piece.Value;

            return eval;
        }
    }
}
