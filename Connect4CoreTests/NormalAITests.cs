using connect_4;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace connect_4_core.Tests
{
    [TestClass()]
    public class NormalAITests
    {
        Board b = new Board();
        NormalAIPlayer n = new NormalAIPlayer(PlayerID.Two);


        [TestInitialize()]
        public void TestInitialize()
        {
            b = new Board();
        }

        [TestMethod()]
        public void Play_Test()
        {
            b.Set(4, 2, 0);
            b.Set(4, 3, 0);
            b.Set(4, 4, 0);
            b.Set(4, 5, 0);

            var result = n.Play(b);
            Assert.AreEqual(result, (uint)4);

            b = new Board();

            b.Set(6, 3, 0);
            b.Set(6, 4, 0);
            b.Set(6, 5, 0);

            result = n.Play(b);
            Assert.AreEqual(result, (uint)6);
        }
    }
}