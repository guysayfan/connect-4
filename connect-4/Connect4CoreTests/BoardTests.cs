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