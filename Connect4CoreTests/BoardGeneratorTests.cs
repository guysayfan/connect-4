using Connect4AI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

using Debug = System.Diagnostics.Debug;

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
        public void GenerateAllBoards_Test()
        {
            var b = $@"
            .......
            .......
            .......
            .......
            .......
            .......";
            var bb = bg.BuildBoard(b);
            var pSequences = new PlaySequenceSet();
            uint player = 0;
            var b23 = $@"
            .......
            .......
            .......
            .......
            .......
            ..10...";

            var allBoards = bg.GenerateAllBoards(bb, pSequences, 2, player);
            
            
            foreach (var e in allBoards)
            {
                var board = e.Key;
                var pieceCount = board.CountPieces();
                uint expected = 2;

                Assert.AreEqual(expected, pieceCount);
            }

            allBoards = bg.GenerateAllBoards(bb, pSequences, 3, player);
            Console.WriteLine(allBoards);
        }

        [TestMethod()]
        public void GenerateAllBoardsDuplicates_Test()
        {
            var b = $@"
            ...1001
            ...0110
            ...1001
            ...0110
            ...1001
            ...0110";
            var bb = bg.BuildBoard(b);
            var pSequences = new PlaySequenceSet();
            uint player = 0;
            uint lookahead = 3;

            var allBoards = bg.GenerateAllBoards(bb, pSequences, lookahead, player);

            foreach (var e in allBoards)
            {
                var board = e.Key;
                var pieceCount = board.CountPieces();
                uint expected = 8;

                Assert.AreEqual(expected, pieceCount);
            }
            var output = "";
            foreach (var e in allBoards.Select((e, index) => new { e.Key, e.Value, index }))
            {
                var index = (uint)e.index;
                var seqs = e.Value;
                
                foreach (var seq in seqs)
                {
                    output += $"{index.ToString()}, {seqs.Count}, sequence: ";
                    foreach (var play in seq)
                    {
                        output += $"{play}, ";
                    }
                    output += "\n";
                }

            }
            Debug.WriteLine(allBoards);
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
            var pSequences = new PlaySequenceSet();
            var result = bg.GenerateBoards(bb, 0, pSequences);

            Assert.AreEqual(result.Count, 7);

            foreach (var e in result.Select((e, index) => new {  e.Key, e.Value, index }))
            {
                var index = (uint)e.index;
                var board = e.Key;
                var seq = e.Value;
                var expectedSeq = JsonSerializer.Serialize(new HashSet<List<uint>> { new List<uint> { index } });

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
                var actualSeq = JsonSerializer.Serialize(seq);
                Assert.AreEqual(expectedSeq, actualSeq);
            }
        }
    }
}