using Knightmare.Boards;

namespace Knightmare.Algorithm
{
    internal class SimpleEvaluation : IEvaluation
    {
        public int Execute(Board _position)
        {
            return _position.WhitePieces.Sum(pair => pair.Value.Value())
                   - _position.BlackPieces.Sum(pair => pair.Value.Value());
        }
    }
}
