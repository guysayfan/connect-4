using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace connect_4_core.Tests
{
    [TestClass()]
    public class BoardTests
    {
        Board b = new Board();
        Location loc00 = new Location(0, 0);
        Location loc25 = new Location(2, 5);
        Location loc02 = new Location(0, 2);
        Location loc60 = new Location(6, 0);



        [TestInitialize()]
        public void TestInitialize()
        {
            b = new Board();
        }

        [TestMethod()]
        public void DropPieceSuccess_Test()
        {
            Assert.IsNull(b.GetPlayer(loc25));
            b.DropPiece(2, 1);
            Assert.AreEqual(1U, b.GetPlayer(loc25));
        }

        [TestMethod()]
        public void DropPieceInFullCol_Test()
        {
            Assert.IsNull(b.GetPlayer(loc25));
            // Drop 6 pieces in column 2
            for (uint i = 0; i < 6; i++)
            {
                b.DropPiece(2, 1);
            }
            Assert.IsTrue(b.IsColFull(2));

            // Drop another price, expect an Exception
            try
            {
                b.DropPiece(2, 1);
                // Should never get here
                Assert.Fail();
            } catch (Exception ex)
            {
                
            }
        }

        [TestMethod()]
        public void CheckWinTopLeftBottomRight_Test()
        {
            var win = b.CheckTopLeftBotRightWin(loc02, 0);
            Assert.IsFalse(win);
            // 

            List<int[]> p1List = new List<int[]>();
            p1List.Add(new int[] { 0, 0 });
            p1List.Add(new int[] { 1, 1 });
            p1List.Add(new int[] { 2, 2 });
            p1List.Add(new int[] { 3, 3 });

            List<int[]> p2List = new List<int[]>();
            b.Populate(p1List, p2List);

            win = b.CheckTopLeftBotRightWin(loc00, 0);
            Assert.IsTrue(win);

            p2List.Add(new int[] { 2, 2 });
            b.Populate(p1List, p2List);
            win = b.CheckTopLeftBotRightWin(loc00, 0);
            Assert.IsFalse(win);
        }

        [TestMethod()]
        public void CheckWinTopRightBottomLeft_Test()
        {
            var win = b.CheckBotLeftTopRightWin(loc60, 0);
            Assert.IsFalse(win);
            // 

            List<int[]> p1List = new List<int[]>();
            p1List.Add(new int[] { 6, 0 });
            p1List.Add(new int[] { 5, 1 });
            p1List.Add(new int[] { 4, 2 });
            p1List.Add(new int[] { 3, 3 });

            List<int[]> p2List = new List<int[]>();
            b.Populate(p1List, p2List);

            win = b.CheckBotLeftTopRightWin(loc60, 0);
            Assert.IsTrue(win);

            p2List.Add(new int[] { 4, 2 });
            b.Populate(p1List, p2List);
            Assert.IsTrue(win);
        }
        //[TestMethod()]
        //public void IsColFullTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void GetPlayerTest()
        //{
        //    Assert.Fail();
        //}
    }
}