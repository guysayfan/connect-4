using connect_4;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace connect_4_core.Tests
{
    [TestClass()]
    public class SuperAITests
    {
        Board b = new Board();
        SuperAIPlayer n = new SuperAIPlayer(1, 0, 3);


        [TestInitialize()]
        public void TestInitialize()
        {
            b = new Board();
        }
    }
}