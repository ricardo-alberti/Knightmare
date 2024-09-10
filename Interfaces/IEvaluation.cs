using Knightmare.Boards;

namespace Knightmare.Algorithm
{
    interface IEvaluation
    {
        int Execute(Board _position);
    }
}
