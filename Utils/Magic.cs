using System.Collections.Generic;

public static class Magic
{
    public static readonly ulong[] RookMagics = {
        0x0080001020400080UL, 0x0040001000200040UL, 0x0080081000200080UL, 0x0080040800100080UL,
        0x0080020400080080UL, 0x0080010200040080UL, 0x0080008001000200UL, 0x0080002040800100UL,
        0x0000800020400080UL, 0x0000400020005000UL, 0x0000801000200080UL, 0x0000800800100080UL,
        0x0000800400080080UL, 0x0000800200040080UL, 0x0000800100020080UL, 0x0000800040800100UL,
        0x0000208000400080UL, 0x0000404000201000UL, 0x0000808010002000UL, 0x0000808008001000UL,
        0x0000808004000800UL, 0x0000808002000400UL, 0x0000010100020004UL, 0x0000020000408104UL,
        0x0000208080004000UL, 0x0000200040005000UL, 0x0000100080200080UL, 0x0000080080100080UL,
        0x0000040080080080UL, 0x0000020080040080UL, 0x0000010080800200UL, 0x0000800080004100UL,
        0x0000204000800080UL, 0x0000200040401000UL, 0x0000100080802000UL, 0x0000080080801000UL,
        0x0000040080800800UL, 0x0000020080800400UL, 0x0000020001010004UL, 0x0000800040800100UL,
        0x0000204000808000UL, 0x0000200040008080UL, 0x0000100020008080UL, 0x0000080010008080UL,
        0x0000040008008080UL, 0x0000020004008080UL, 0x0000010002008080UL, 0x0000004081020004UL,
        0x0000204000800080UL, 0x0000200040008080UL, 0x0000100020008080UL, 0x0000080010008080UL,
        0x0000040008008080UL, 0x0000020004008080UL, 0x0000800100020080UL, 0x0000800041000080UL,
        0x00FFFCDDFCED714AUL, 0x007FFCDDFCED714AUL, 0x003FFFCDFFD88096UL, 0x0000040810002101UL,
        0x0001000204080011UL, 0x0001000204000801UL, 0x0001000082000401UL, 0x0001FFFAABFAD1A2UL
    };

    public static readonly ulong[] BishopMagics = {
        0x0002020202020200UL, 0x0002020202020000UL, 0x0004010202000000UL, 0x0004040080000000UL,
        0x0001104000000000UL, 0x0000821040000000UL, 0x0000410410400000UL, 0x0000104104104000UL,
        0x0000040404040400UL, 0x0000020202020200UL, 0x0000040102020000UL, 0x0000040400800000UL,
        0x0000011040000000UL, 0x0000008210400000UL, 0x0000004104104000UL, 0x0000002082082000UL,
        0x0004000808080800UL, 0x0002000404040400UL, 0x0001000202020200UL, 0x0000800802004000UL,
        0x0000800400A00000UL, 0x0000200100884000UL, 0x0000400082082000UL, 0x0000200041041000UL,
        0x0002080010101000UL, 0x0001040008080800UL, 0x0000208004010400UL, 0x0000404004010200UL,
        0x0000840000802000UL, 0x0000404002011000UL, 0x0000808001041000UL, 0x0000404000820800UL,
        0x0001041000202000UL, 0x0000820800101000UL, 0x0000104400080800UL, 0x0000020080080080UL,
        0x0000404040040100UL, 0x0000808100020100UL, 0x0001010100020800UL, 0x0000808080010400UL,
        0x0000820820004000UL, 0x0000410410002000UL, 0x0000082088001000UL, 0x0000002011000800UL,
        0x0000080100400400UL, 0x0001010101000200UL, 0x0002020202000400UL, 0x0001010101000200UL,
        0x0000410410400000UL, 0x0000208208200000UL, 0x0000002084100000UL, 0x0000000020880000UL,
        0x0000001002020000UL, 0x0000040408020000UL, 0x0004040404040000UL, 0x0002020202020000UL,
        0x0000104104104000UL, 0x0000002082082000UL, 0x0000000020841000UL, 0x0000000000208800UL,
        0x0000000010020200UL, 0x0000000404080200UL, 0x0000040404040400UL, 0x0002020202020200UL
    };

    public static int[] RookRelevantBits = new int[64]
    {
        12,11,11,11,11,11,11,12,
        11,10,10,10,10,10,10,11,
        11,10,10,10,10,10,10,11,
        11,10,10,10,10,10,10,11,
        11,10,10,10,10,10,10,11,
        11,10,10,10,10,10,10,11,
        11,10,10,10,10,10,10,11,
        12,11,11,11,11,11,11,12
    };

    public static int[] BishopRelevantBits = new int[64]
    {
        6,5,5,5,5,5,5,6,
        5,5,5,5,5,5,5,5,
        5,5,7,7,7,7,5,5,
        5,5,7,9,9,7,5,5,
        5,5,7,9,9,7,5,5,
        5,5,7,7,7,7,5,5,
        5,5,5,5,5,5,5,5,
        6,5,5,5,5,5,5,6
    };

    public static ulong[][] RookAttacks = new ulong[64][];
    public static ulong[][] BishopAttacks = new ulong[64][];
    public static readonly ulong[] RookMasks = new ulong[64];
    public static readonly ulong[] BishopMasks = new ulong[64];

    public static readonly ulong[][] RookAttackTable = new ulong[64][];
    public static readonly ulong[][] BishopAttackTable = new ulong[64][];
    public static int[] RookShifts = new int[64];
    public static int[] BishopShifts = new int[64];

    static Magic()
    {
        for (int sq = 0; sq < 64; sq++)
        {
            RookShifts[sq] = 64 - RookRelevantBits[sq];
            BishopShifts[sq] = 64 - BishopRelevantBits[sq];
            RookMasks[sq] = GetRookMask(sq);
            BishopMasks[sq] = GetBishopMask(sq);
        }

        InitializeRookAttacks();
        InitializeBishopAttacks();

        for (int sq = 0; sq < 64; sq++)
        {
            RookAttackTable[sq] = RookAttacks[sq];
            BishopAttackTable[sq] = BishopAttacks[sq];
        }
    }

    public static void InitializeRookAttacks()
    {
        for (int square = 0; square < 64; square++)
        {
            int relevantBits = RookRelevantBits[square];
            ulong mask = GetRookMask(square);
            var blockers = GenerateBlockerPermutations(mask);
            RookAttacks[square] = new ulong[1 << relevantBits];  // Allocate with relevantBits

            foreach (var blocker in blockers)
            {
                int shift = 64 - relevantBits;
                int index = (int)((blocker * RookMagics[square]) >> shift);
                RookAttacks[square][index] = RookAttacksFromBlockers(square, blocker);
            }
        }
    }

    public static void InitializeBishopAttacks()
    {
        for (int square = 0; square < 64; square++)
        {
            int relevantBits = BishopRelevantBits[square];
            ulong mask = BishopMasks[square];
            var blockers = GenerateBlockerPermutations(mask);
            BishopAttacks[square] = new ulong[1 << relevantBits];

            foreach (var blocker in blockers)
            {
                int shift = 64 - relevantBits;
                int index = (int)((blocker * BishopMagics[square]) >> shift);
                BishopAttacks[square][index] = BishopAttacksFromBlockers(square, blocker);
            }
        }
    }

    public static ulong BishopAttacksFromBlockers(int square, ulong blockers)
    {
        ulong attacks = 0UL;
        int rank = square / 8;
        int file = square % 8;

        for (int r = rank + 1, f = file + 1; r < 8 && f < 8; r++, f++)
        {
            int sq = r * 8 + f;
            attacks |= 1UL << sq;
            if (((blockers >> sq) & 1) != 0) break;
        }

        for (int r = rank + 1, f = file - 1; r < 8 && f >= 0; r++, f--)
        {
            int sq = r * 8 + f;
            attacks |= 1UL << sq;
            if (((blockers >> sq) & 1) != 0) break;
        }
        // Down-right
        for (int r = rank - 1, f = file + 1; r >= 0 && f < 8; r--, f++)
        {
            int sq = r * 8 + f;
            attacks |= 1UL << sq;
            if (((blockers >> sq) & 1) != 0) break;
        }
        // Down-left
        for (int r = rank - 1, f = file - 1; r >= 0 && f >= 0; r--, f--)
        {
            int sq = r * 8 + f;
            attacks |= 1UL << sq;
            if (((blockers >> sq) & 1) != 0) break;
        }

        return attacks;
    }


    public static ulong RookAttacksFromBlockers(int square, ulong blockers)
    {
        ulong attacks = 0UL;
        int rank = square / 8;
        int file = square % 8;

        // Up
        for (int r = rank + 1; r < 8; r++)
        {
            int sq = r * 8 + file;
            attacks |= 1UL << sq;
            if (((blockers >> sq) & 1) != 0) break;
        }
        // Down
        for (int r = rank - 1; r >= 0; r--)
        {
            int sq = r * 8 + file;
            attacks |= 1UL << sq;
            if (((blockers >> sq) & 1) != 0) break;
        }
        // Right
        for (int f = file + 1; f < 8; f++)
        {
            int sq = rank * 8 + f;
            attacks |= 1UL << sq;
            if (((blockers >> sq) & 1) != 0) break;
        }
        // Left
        for (int f = file - 1; f >= 0; f--)
        {
            int sq = rank * 8 + f;
            attacks |= 1UL << sq;
            if (((blockers >> sq) & 1) != 0) break;
        }

        return attacks;
    }

    public static List<ulong> GenerateBlockerPermutations(ulong mask)
    {
        List<ulong> permutations = new();
        int bitCount = CountBits(mask);
        int permutationsCount = 1 << bitCount;

        for (int i = 0; i < permutationsCount; i++)
        {
            ulong blocker = SetBitsFromIndex(mask, i);
            permutations.Add(blocker);
        }

        return permutations;
    }

    public static ulong SetBitsFromIndex(ulong mask, int index)
    {
        ulong result = 0;
        int bitPosition = 0;

        for (int i = 0; i < 64; i++)
        {
            if (((mask >> i) & 1) == 1)
            {
                if (((index >> bitPosition) & 1) == 1)
                    result |= 1UL << i;

                bitPosition++;
            }
        }

        return result;
    }

    public static int CountBits(ulong b)
    {
        int count = 0;
        while (b != 0)
        {
            b &= b - 1;
            count++;
        }
        return count;
    }

    public static ulong GetRookMask(int square)
    {
        ulong mask = 0UL;
        int rank = square / 8;
        int file = square % 8;

        // Horizontal (left and right, excluding edge squares)
        for (int f = file + 1; f <= 6; f++)
            mask |= 1UL << (rank * 8 + f);
        for (int f = file - 1; f >= 1; f--)
            mask |= 1UL << (rank * 8 + f);

        // Vertical (up and down, excluding edge squares)
        for (int r = rank + 1; r <= 6; r++)
            mask |= 1UL << (r * 8 + file);
        for (int r = rank - 1; r >= 1; r--)
            mask |= 1UL << (r * 8 + file);

        return mask;
    }

    public static ulong GetBishopMask(int square)
    {
        ulong mask = 0UL;
        int rank = square / 8;
        int file = square % 8;

        // Up-right
        for (int r = rank + 1, f = file + 1; r <= 6 && f <= 6; r++, f++)
            mask |= 1UL << (r * 8 + f);

        // Up-left
        for (int r = rank + 1, f = file - 1; r <= 6 && f >= 1; r++, f--)
            mask |= 1UL << (r * 8 + f);

        // Down-right
        for (int r = rank - 1, f = file + 1; r >= 1 && f <= 6; r--, f++)
            mask |= 1UL << (r * 8 + f);

        // Down-left
        for (int r = rank - 1, f = file - 1; r >= 1 && f >= 1; r--, f--)
            mask |= 1UL << (r * 8 + f);

        return mask;
    }
}
