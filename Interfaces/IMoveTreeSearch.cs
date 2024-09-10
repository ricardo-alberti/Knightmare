using Knightmare.Moves;
using Knightmare.Boards;

namespace Knightmare.Algorithm
{
    interface IGraphSearch
    {
        MoveTree Execute(Board _position, Robot me, Robot enemy, int level);
    }
}
