using Knightmare.Moves;
using Knightmare.Boards;

namespace Knightmare.Algorithm
{
    interface ITreeSearch
    {
        MoveTree BestTree(Board _position, Robot me, Robot enemy, int level);
    }
}
