using Microsoft.VisualStudio.TestTools.UnitTesting;
using connect_4_core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connect_4_core.Tests
{
    [TestClass()]
    public class BoardTests
    {
        Board b = new Board();

        [TestInitialize()]
        public void TestInitialize()
        {
            b = new Board();
        }

        [TestMethod()]
        public void DropPieceSuccess_Test()
        {
            Assert.IsNull(b.GetPlayer(2, 5));
            b.DropPiece(2, 1);
            Assert.AreEqual(1, b.GetPlayer(2, 5));
        }

        [TestMethod()]
        public void DropPieceInFullCol_Test()
        {
            Assert.IsNull(b.GetPlayer(2, 5));
            for (int i = 0; i < 6; i++)
            {
                b.DropPiece(2, 1);
                for (int j = 0; j < 6; j++)
                {
                    if (j <= i)
                    {
                        Assert.AreEqual(1, b.GetPlayer(2, 5 - j));
                    } else
                    {
                        Assert.IsNull(b.GetPlayer(2, 5 - j));
                    }
                }
            }
        }

        [TestMethod()]
        public void CheckWinTopLeftBottomRight_Test()
        {
            var win = b.CheckTopLeftBotRightWin(0, 2, 0);
            Assert.IsFalse(win);
            // 

            List<int[]> p1List = new List<int[]>();
            p1List.Add(new int[] { 0, 0 });
            p1List.Add(new int[] { 1, 1 });
            p1List.Add(new int[] { 2, 2 });
            p1List.Add(new int[] { 3, 3 });

            List<int[]> p2List = new List<int[]>();
            b.Populate(p1List, p2List);

            win = b.CheckTopLeftBotRightWin(0, 0, 0);
            Assert.IsTrue(win);

            p2List.Add(new int[] { 2, 2 });
            b.Populate(p1List, p2List);
            win = b.CheckTopLeftBotRightWin(0, 0, 0);
            Assert.IsFalse(win);
        }

        [TestMethod()]
        public void CheckWinTopRightBottomLeft_Test()
        {
            var win = b.CheckTopRightBotLeftWin(6, 0, 0);
            Assert.IsFalse(win);
            // 

            List<int[]> p1List = new List<int[]>();
            p1List.Add(new int[] { 6, 0 });
            p1List.Add(new int[] { 5, 1 });
            p1List.Add(new int[] { 4, 2 });
            p1List.Add(new int[] { 3, 3 });

            List<int[]> p2List = new List<int[]>();
            b.Populate(p1List, p2List);

            win = b.CheckTopRightBotLeftWin(6, 0, 0);
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