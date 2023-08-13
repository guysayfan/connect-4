using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace connect_4_core.Tests
{
    [TestClass()]
    public class VictoryCheckerTests
    {
        Board b = new Board();

        [TestInitialize()]
        public void TestInitialize()
        {
            b = new Board();
        }

        [TestMethod()]
        public void CheckVerticalWin_Test()
        {
            b.Set(0, 2, 0);
            b.Set(0, 3, 0);
            b.Set(0, 4, 0);
            b.Set(0, 5, 0);

            var result = VictoryChecker.CheckVictory(b, 0, 0);
            Assert.IsTrue(result);

            b.Set(1, 2, 0);
            b.Set(1, 3, 0);
            b.Set(1, 4, 0);
            b.Set(1, 5, 0);

            result = VictoryChecker.CheckVictory(b, 1, 0);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void CheckVerticalWin2_Test()
        {
            b.Set(0, 2, 0);
            b.Set(0, 3, PlayerID.Two);
            b.Set(0, 4, PlayerID.One);
            b.Set(0, 5, PlayerID.One);

            var result = VictoryChecker.CheckVictory(b, 0, PlayerID.One);
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void CheckVerticalWin3_Test()
        {
            b.Set(2, 1, PlayerID.One);
            b.Set(2, 2, PlayerID.One);
            b.Set(2, 3, PlayerID.One);

            var result = VictoryChecker.CheckVictory(b, 2, PlayerID.One);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void CheckVerticalWin4_Test()
        {
            b.Set(2, 2, PlayerID.One);
            b.Set(2, 3, PlayerID.One);
            b.Set(2, 4, PlayerID.Two);
            b.Set(2, 5, PlayerID.One);

            var result = VictoryChecker.CheckVictory(b, 2, PlayerID.One);
            Assert.IsFalse(result);
        }


        [TestMethod()]
        public void CheckHorizontalWin_Test()
        {
            b.Set(1, 5, PlayerID.One);
            b.Set(2, 5, PlayerID.One);
            b.Set(3, 5, PlayerID.One);
            b.Set(4, 5, PlayerID.One);

            var result = VictoryChecker.CheckVictory(b, 3, PlayerID.One);
            Assert.IsTrue(result);

            b.Set(2, 5, PlayerID.Two);

            result = VictoryChecker.CheckVictory(b, 3, PlayerID.One);
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void CheckWinTopLeftBottomRight_Test()
        {
            b.Set(6, 5, PlayerID.One);
            b.Set(5, 4, PlayerID.One);
            b.Set(4, 3, PlayerID.One);
            b.Set(3, 2, PlayerID.One);

            var result = VictoryChecker.CheckVictory(b, 6, PlayerID.One);
            Assert.IsTrue(result);

            b.Set(3, 2, PlayerID.Two);

            result = VictoryChecker.CheckVictory(b, 6, PlayerID.One);
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void CheckWinBottomLeftTopRight_Test()
        {
            b.Set(0, 5, PlayerID.One);
            b.Set(1, 4, PlayerID.One);
            b.Set(2, 3, PlayerID.One);
            b.Set(3, 2, PlayerID.One);

            var result = VictoryChecker.CheckVictory(b, 2, PlayerID.One);
            Assert.IsTrue(result);

            b.Set(3, 2, PlayerID.Two);

            result = VictoryChecker.CheckVictory(b, 2, PlayerID.One);
            Assert.IsFalse(result);
        }
    }
}