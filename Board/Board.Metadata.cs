internal partial class Board
{
    public bool WhiteCanCastleKingSide
    {
        get => (Metadata & (1UL << 6)) != 0;
        set
        {
            if (value) Metadata |= (1UL << 6);
            else Metadata &= ~(1UL << 6);
        }
    }

    public bool WhiteCanCastleQueenSide
    {
        get => (Metadata & (1UL << 7)) != 0;
        set
        {
            if (value) Metadata |= (1UL << 7);
            else Metadata &= ~(1UL << 7);
        }
    }

    public bool BlackCanCastleKingSide
    {
        get => (Metadata & (1UL << 8)) != 0;
        set
        {
            if (value) Metadata |= (1UL << 8);
            else Metadata &= ~(1UL << 8);
        }
    }

    public bool BlackCanCastleQueenSide
    {
        get => (Metadata & (1UL << 9)) != 0;
        set
        {
            if (value) Metadata |= (1UL << 9);
            else Metadata &= ~(1UL << 9);
        }
    }

    public int HalfmoveClock
    {
        get => (int)((Metadata >> 10) & 0xFF);
        set
        {
            Metadata &= ~(0xFFUL << 10);
            Metadata |= ((ulong)value & 0xFFUL) << 10;
        }
    }

    public int FullmoveNumber
    {
        get => (int)((Metadata >> 18) & 0x3FF);
        set
        {
            Metadata &= ~(0x3FFUL << 18);
            Metadata |= ((ulong)value & 0x3FFUL) << 18;
        }
    }

    public bool WhiteToMove
    {
        get => (Metadata & (1UL << 28)) != 0;
        set
        {
            if (value) Metadata |= (1UL << 28);
            else Metadata &= ~(1UL << 28);
        }
    }
}
