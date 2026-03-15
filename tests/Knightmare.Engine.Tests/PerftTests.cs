using Xunit;

public class PerftTests
{
    [Fact]
    public void Perft_Depth1_StartPosition()
    {
        var board = BoardParser.CreateBoardFromFEN();
        var moves = MoveGenerator.GenerateMoves(board);

        Assert.Equal(20, moves.Count);
    }
}
