using connect_4;
using Connect4AI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection.Metadata;


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
        }

        [TestMethod()]
        public void GenerateBoard_Test()
        {
            
        }
    }
}