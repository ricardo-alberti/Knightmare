using Knightmare.Moves;
using Knightmare.Boards;

namespace Knightmare.Algorithm
{
    interface ITreeSearch
    {
        MoveTree BestTree(Board _position, int level);
    }
}
