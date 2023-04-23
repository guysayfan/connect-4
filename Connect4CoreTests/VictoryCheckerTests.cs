using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace connect_4_core.Tests
{
    [TestClass()]
    public class VictoryCheckerTests
    {
        Board b = new Board();
        VictoryChecker v = new VictoryChecker();


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

            var result = v.CheckVictory(b, 0, 0);
            Assert.IsTrue(result);

            b.Set(1, 2, 0);
            b.Set(1, 3, 0);
            b.Set(1, 4, 0);
            b.Set(1, 5, 0);

            result = v.CheckVictory(b, 1, 0);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void CheckVerticalWin2_Test()
        {
            b.Set(0, 2, 0);
            b.Set(0, 3, 1);
            b.Set(0, 4, 0);
            b.Set(0, 5, 0);

            var result = v.CheckVictory(b, 0, 0);
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void CheckVerticalWin3_Test()
        {
            b.Set(2, 1, 0);
            b.Set(2, 2, 0);
            b.Set(2, 3, 0);

            var result = v.CheckVictory(b, 2, 0);
            Assert.IsTrue(result);
        }


        [TestMethod()]
        public void CheckHorizontalWin_Test()
        {
            b.Set(1, 5, 0);
            b.Set(2, 5, 0);
            b.Set(3, 5, 0);
            b.Set(4, 5, 0);

            var result = v.CheckVictory(b, 3, 0);
            Assert.IsTrue(result);

            b.Set(2, 5, 1);

            result = v.CheckVictory(b, 3, 0);
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void CheckWinTopLeftBottomRight_Test()
        {
            b.Set(6, 5, 0);
            b.Set(5, 4, 0);
            b.Set(4, 3, 0);
            b.Set(3, 2, 0);

            var result = v.CheckVictory(b, 6, 0);
            Assert.IsTrue(result);

            b.Set(3, 2, 1);

            result = v.CheckVictory(b, 6, 0);
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void CheckWinBottomLeftTopRight_Test()
        {
            b.Set(0, 5, 0);
            b.Set(1, 4, 0);
            b.Set(2, 3, 0);
            b.Set(3, 2, 0);

            var result = v.CheckVictory(b, 2, 0);
            Assert.IsTrue(result);

            b.Set(3, 2, 1);

            result = v.CheckVictory(b, 2, 0);
            Assert.IsFalse(result);
        }
    }
}