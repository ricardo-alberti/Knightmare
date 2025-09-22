[Flags]
public enum MoveFlags : byte
{
    Quiet = 0,
    Capture = 1,
    PawnDoublePush = 2,
    KingCastle = 4,
    QueenCastle = 8,
    EnPassant = 16,
    Promotion = 32
}
