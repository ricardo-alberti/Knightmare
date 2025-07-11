using Knightmare.Moves;
using Knightmare.Boards;

namespace Knightmare.Algorithm
{
    interface ITreeSearch
    {
        Node BestTree(Board _position, int level);
    }
}
