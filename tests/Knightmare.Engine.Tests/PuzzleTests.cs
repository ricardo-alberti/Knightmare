using Xunit;

namespace Knightmare.Tests;

public sealed class PuzzleTests
{
    private const int DepthLevel = 7;

    public static IEnumerable<object[]> Puzzles =>
        new List<object[]>
        {
            new object[]
            {
                "r1bqkb1r/pppppppp/6n1/3P4/4PP2/2N5/PPPQ1nPP/R3KBNR w KQkq - 0 9",
                "r1bqkb1r/pppppppp/6n1/3P4/4PP2/2N5/PPP2QPP/R3KBNR b - - 0 0"
            },
            new object[]
            {
                "6k1/b4ppp/5nb1/2p1pq2/1pN5/pP3P1N/P1P3PP/B2rK2R w - - 0 0",
                "6k1/b4ppp/5nb1/2p1pq2/1pN5/pP3P1N/P1P3PP/B2K3R b - - 0 0"
            },
            new object[]
            {
                "rnb1kbnr/pppp1ppp/4p3/8/4P2P/2N5/PPPP1qP1/R1BQKBNR w - - 0 0",
                "rnb1kbnr/pppp1ppp/4p3/8/4P2P/2N5/PPPP1KP1/R1BQ1BNR b - - 0 0"
            }
        };

    [Theory]
    [MemberData(nameof(Puzzles))]
    public void Engine_Solves_Puzzle(string puzzleFen, string expectedFen)
    {
        // Arrange
        Board board = BoardParser.CreateBoardFromFEN(puzzleFen);
        ISearch alphaBeta = new AlphaBeta();

        // Act
        board.MakeMove(alphaBeta.FindBestMove(board, DepthLevel));

        string result = BoardParser.CreateFENFromBoard(board);

        // Assert
        Assert.Equal(expectedFen, result);
    }
}
