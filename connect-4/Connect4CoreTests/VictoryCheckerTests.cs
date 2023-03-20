using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace connect_4_core.Tests
{
    [TestClass()]
    public class VictoryCheckerTests
    {
        Board b = new Board();
        Location loc00 = new Location(0, 0);
        Location loc15 = new Location(1, 5);
        Location loc25 = new Location(2, 5);
        Location loc02 = new Location(0, 2);
        Location loc60 = new Location(6, 0);
        Location loc35 = new Location(3, 5);
        Location loc44 = new Location(4, 4);
        Location loc05 = new Location(0, 5);
        Location loc32 = new Location(3, 2);

        VictoryChecker v = new VictoryChecker();


        [TestInitialize()]
        public void TestInitialize()
        {
            b = new Board();
        }

        //[TestMethod()]
        //public void CheckVerticalWin_Test()
        //{
        //    b.Set(0, 2, 0);
        //    b.Set(0, 3, 0);
        //    b.Set(0, 4, 0);
        //    b.Set(0, 5, 0);

        //    var result = v.CheckVerticalWin(b, 0, 0);
        //    Assert.IsTrue(result);
        //}

        [TestMethod()]
        public void CheckVerticalWinTWO_Test()
        {
            b.Set(0, 2, 0);
            b.Set(0, 3, 1);
            b.Set(0, 4, 0);
            b.Set(0, 5, 0);

            var result = v.CheckVerticalWin(b, 0, 0);
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void CheckHorizontalWin_Test()
        {
            b.Set(0, 5, 0);
            b.Set(1, 5, 0);
            b.Set(2, 5, 0);
            b.Set(3, 5, 0);

            var result = v.CheckHorizontalWin(b, 3, 0);
            Assert.IsTrue(result);

            b.Set(2, 5, 1);

            result = v.CheckVerticalWin(b, 3, 0);
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void CheckWinTopLeftBottomRight_Test()
        {
            b.Set(6, 5, 0);
            b.Set(5, 4, 0);
            b.Set(4, 3, 0);
            b.Set(3, 2, 0);

            var result = v.CheckTopLeftBotRightWin(b, 6, 0);
            Assert.IsTrue(result);

            b.Set(3, 2, 1);

            result = v.CheckTopLeftBotRightWin(b, 6, 0);
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void CheckWinBottomLeftTopRight_Test()
        {
            b.Set(0, 5, 0);
            b.Set(1, 4, 0);
            b.Set(2, 3, 0);
            b.Set(3, 2, 0);

            var result = v.CheckBotLeftTopRightWin(b, 4, 0);
            Assert.IsTrue(result);

            b.Set(2, 3, 1);

            result = v.CheckBotLeftTopRightWin(b, 4, 0);
            Assert.IsFalse(result);
        }
    }
}