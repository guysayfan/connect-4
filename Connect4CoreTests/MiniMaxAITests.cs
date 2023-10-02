using Connect4AI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniMax;
using System.Diagnostics;

namespace connect_4_core.Tests
{
    [TestClass()]
    public class MiniMaxAITests
    {
        Board b = new Board();
        BoardGenerator bg = new BoardGenerator();
        MiniMaxAIPlayer n = new MiniMaxAIPlayer(PlayerID.Two, 2);

        [TestInitialize()]
        public void TestInitialize()
        {
            n = new MiniMaxAIPlayer(PlayerID.Two, 2);
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
        public void VictoryLookAheadZero_Test()
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
        public void BlockLookAheadZero_Test()
        {
            n = new MiniMaxAIPlayer(PlayerID.Two, 0);
            var b = $@"
            .......
            .......
            ...xo..
            ...ox..
            ...xxx.
            o..oox.";
            var bb = bg.BuildBoard(b);

            var result = n.Play(bb);
            Assert.AreEqual((uint)6, result);
        }



        [TestMethod()]
        public void BlockLookAheadOne_Test()
        {
            // supposed to place in col 3. otherwise player 1 wins in 2 turns;
            n = new MiniMaxAIPlayer(PlayerID.Two, 0);
            var b = $@"
            .......
            .......
            .......
            .....xx
            ....oxo
            ...oxox";
            var bb = bg.BuildBoard(b);
            bb.LastPiece = 3;

            var red = "\u1F534";
            var yellow = "\u1F7E1";
            Debug.WriteLine(red + yellow);

            var result = n.Play(bb);
            Assert.AreEqual((uint)3, result);
        }

        [TestMethod()]
        public void ChooseLookAheadTwo_Test()
        {
            // supposed to place in col 2 and win for sure in the next turn
            n = new MiniMaxAIPlayer(PlayerID.One, 2);
            var b = $@"
            .......
            .......
            ...o...
            ...xx..
            .ooxo..
            xoxox..";

            var bb = bg.BuildBoard(b);
            bb.LastPiece = 4;

            var result = n.Play(bb);
            Assert.AreEqual((uint)2, result);
        }


        [TestMethod()]
        public void Node_Test()
        {
            // sure victory
            var b = $@"
            .......
            .......
            .......
            .......
            ......o
            oxxxx.o";
            var bb = bg.BuildBoard(b);
            bb.LastPiece = 1;
            
            var n = new Node(bb);
            var score = n.CalculateScore(Player.One);
            Assert.AreEqual(100, score);

            score = n.CalculateScore(Player.Two);
            Assert.AreEqual(-100, score);
        }
    }
}