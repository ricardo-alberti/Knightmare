using Knightmare.Moves;
using Knightmare.Boards;

namespace Knightmare.Algorithm
{
    interface ITreeSearch
    {
        MoveTree BestTree(Board _position, int level);
        List<MoveTree> GetInitialMoveTrees(Board _position, int depth);
    }
}
