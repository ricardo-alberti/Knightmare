using Knightmare.Moves;
using Knightmare.Boards;

namespace Knightmare.Algorithm
{
    interface ITreeSearch
    {
        public int TotalMoves { get; set; }
        public long ElapsedTime { get; set; }
        MoveTree Execute(Board _position, Robot me, Robot enemy, int level);
    }
}
