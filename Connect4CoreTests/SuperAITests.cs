using connect_4;
using Connect4AI;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace connect_4_core.Tests
{
    [TestClass()]
    public class SuperAITests
    {
        Board b = new Board();
        SuperAIPlayer n = new SuperAIPlayer(PlayerID.Two, 3);
        BoardGenerator bg = new BoardGenerator();


        [TestInitialize()]
        public void TestInitialize()
        {
            SuperAIPlayer n = new SuperAIPlayer(PlayerID.Two, 3);
            b = new Board();
        }

        [TestMethod()]
        public void Play_Test()
        {
            b.Set(4, 3, PlayerID.One);
            b.Set(4, 4, PlayerID.One);
            b.Set(4, 5, PlayerID.One);

            var result = n.Play(b);
            Assert.AreEqual(result, (uint)4);

            b = new Board();

            b.Set(6, 3, PlayerID.One);
            b.Set(6, 4, PlayerID.One);
            b.Set(6, 5, PlayerID.One);

            result = n.Play(b);
            Assert.AreEqual(result, (uint)6);
        }

        [TestMethod()]
        public void VictoryLookAheadOne_Test()
        {
            var b = $@"
            .......
            .......
            .......
            ..ox...
            .oxx...
            oxoxx..";
            var bb = bg.BuildBoard(b);

            var result = n.Play(bb);
            Assert.AreEqual(result, (uint)3);
        }

        [TestMethod()]
        public void VictoryLookAheadTwo_Test()
        {
            var b = $@"
            .......
            .......
            ...ox..
            ...xo..
            x..ooo.
            x..xxo.";
            var bb = bg.BuildBoard(b);

            var result = n.Play(bb);
            Assert.AreEqual(result, (uint)6);
            result = n.Play(bb);
            Assert.AreEqual(result, (uint)6);
        }

        [TestMethod()]
        public void BlockLookAheadTwo_Test()
        {
            n = new SuperAIPlayer(PlayerID.Two, 2);
            var b = $@"
            .......
            .......
            ....o..
            ...ox..
            ...xxx.
            o..oox.";
            var bb = bg.BuildBoard(b);

            var result = n.Play(bb);
            Assert.AreEqual(result, (uint)3);
        }

        [TestMethod()]
        public void FindPotentialCols_Test()
        {
            var ai = new SuperAIPlayer(PlayerID.Two, 1);
            // Test cases:
            // No potential cols
            var b = $@"
            xxxxxxo
            xxxxxxo
            xxxxxxx
            xxxoxxx
            xxxxxxo
            oxxooxo";
            var bb = bg.BuildBoard(b);
            var result = ai.FindPotentialCols(bb);
            Assert.AreEqual(result.Length, 0);

            // 1 sure victory
            b = $@"
            xxx.xxo
            xxx.xxo
            xxx.xxx
            xxx.xxx
            xxx.xxo
            oxxooxo";
            bb = bg.BuildBoard(b);
            result = ai.FindPotentialCols(bb);
            Assert.AreEqual(result.Length, 1);
            Assert.AreEqual(result[0], (uint)3);

            // 1 potential
            ai = new SuperAIPlayer(PlayerID.One, 1);
            b = $@"
            xxx.xxo
            xxx.xxo
            xxx.xxx
            xxx.xxx
            xxx.xxo
            oxxooxo";
            bb = bg.BuildBoard(b);
            result = ai.FindPotentialCols(bb);
            Assert.AreEqual(result.Length, 1);
            Assert.AreEqual(result[0], (uint)3);            

            // 1 potential 1 sure

            ai = new SuperAIPlayer(PlayerID.Two, 2);
            // Test cases:
            // No potential cols
            // 1 sure victory
            // 1 potential
            // 1 potential 1 sure
        }

        [TestMethod()]
        public void FindPotentialCols_SureLoss_Test()
        {
            var ai = new SuperAIPlayer(PlayerID.One, 0);
            var b = $@"
            .xxx.xo
            .xxx.xo
            .xxx.xx
            .xxx.xx
            .xxx.xo
            oxxooxo";
            var bb = bg.BuildBoard(b);
            var result = ai.FindPotentialCols(bb);
            Assert.AreEqual(2, result.Length);
            Assert.AreEqual((uint)0, result[0]);
            Assert.AreEqual((uint)3, result[1]);
        }
    }
}