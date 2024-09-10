using Knightmare.Boards;

namespace Knightmare.Algorithm
{
    internal class SimpleEvaluation : IEvaluation
    {
        public int Execute(Board _position)
        {
            int whiteQuality = 0;
            int blackQuality = 0;

            foreach (var pair in _position.WhitePieces())
            {
                whiteQuality += pair.Value.Value();
            }

            foreach (var pair in _position.BlackPieces())
            {
                blackQuality += pair.Value.Value();
            }

            return (whiteQuality) - (blackQuality);
        }
    }
}
