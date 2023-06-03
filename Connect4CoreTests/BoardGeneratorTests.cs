using connect_4;
using Connect4AI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace connect_4_core.Tests
{
    [TestClass()]
    public class BoardGeneratorTests
    {
        BoardGenerator bg = new BoardGenerator();


        [TestInitialize()]
        public void TestInitialize()
        {
            bg = new BoardGenerator();
        }

        [TestMethod()]
        public void BuildBoard_Test()
        {
            var input = $@"
            .......
            .......
            .......
            .......
            .......
            .......";

            var board = (Board)bg.BuildBoard(input);
            
            Assert.IsTrue(board.CountPieces() == 0);

            input = $@"
            .......
            .......
            .......
            .......
            .o.....
            .x..x..";

            board = (Board)bg.BuildBoard(input);

            Assert.IsTrue(board.CountPieces() == 3);
        }

        [TestMethod()]
        public void DisplayBoard_Test()
        {
            var b = $@"
            .......
            .......
            .......
            .......
            .o.....
            .x..x..";
            var bb = bg.BuildBoard(b);

            var result = bg.DisplayBoard(bb);
            var unindented = bg.UnindentString(b);
            Assert.AreEqual(unindented, result);
        }

        [TestMethod()]
        public void GenerateBoards_Test()
        {
            var b = $@"
            .......
            .......
            .......
            .......
            .......
            .......";
            var bb = bg.BuildBoard(b);
            var pSequences = new HashSet<List<uint>>();
            var result = bg.GenerateBoards(bb, 0, pSequences);

            Assert.AreEqual(result.Count, 7);

            foreach (var e in result.Select((e, index) => new {  e.Key, e.Value, index }))
            {
                var index = (uint)e.index;
                var board = e.Key;
                var seq = e.Value;
                var expectedSeq = JsonSerializer.Serialize(new HashSet<List<uint>> { new List<uint> { 0 } });

                // Verify board
                for (uint col = 0; col < 7; col++)
                {
                    for (uint row = 0; row < 6; row++)
                    {
                        var loc = new Location(col, row);
                        var player = board.GetPlayer(loc);

                        if (col == index && row == 5)
                        {
                            Assert.AreEqual((int)player, 0);
                        } else
                        {
                            Assert.AreEqual(player, null);
                        }
                    }
                }

                // Verify play sequences
                var actualSeq = JsonSerializer.Serialize(seq)
;                Assert.AreEqual(expectedSeq, actualSeq);
            }
        }
    }
}