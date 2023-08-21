using connect_4_core;
using Connect4AI;
using System.Diagnostics;


namespace connect_4
{
    enum BaseColScore : uint
    {
        Victory = 10000,
        Center = 6,          // piece center column 
        ThreeOutfOfFour = 4, // 3 pieces and 1 empty
        TwoOutOfFour = 2     // 2 pieces and 2 empty
    }

    public class Node : MiniMax.INode
    {
        Board board;

        public Node(Board board)
        {
            this.board = board;
        }

        private bool isTerminal()
        {
            var boardIsFull = Enumerable.Range(0, 7)
            .Select(i => board.IsColFull((uint)i)).All(x => x);

            var player1Wins = board.FindWinningCols(PlayerID.One).Count > 0;
            var player2Wins = board.FindWinningCols(PlayerID.Two).Count > 0;

            return boardIsFull || player1Wins || player2Wins;
        }

        private IEnumerable<MiniMax.INode> getChildren() {
            var children = new HashSet<Node>();

            
            for (uint i = 0; i < 7; i++)
            {
                if (board.IsColFull(i))
                {
                    continue;
                }

                var pieceCount = board.GetPieceCount();
                var player = pieceCount.Player1 == pieceCount.Player2 ? PlayerID.One : PlayerID.Two;

                var b = new Board(board);
                b.DropPiece(i, player);
                children.Add(new Node(b));
            }

            return children;
        }

        public bool Terminal => isTerminal();
            


        public IEnumerable<MiniMax.INode> Children => getChildren();

        public int CalculateScore(MiniMax.Player maximizingPlayer)
        {
            throw new NotImplementedException();
        }
    }



    public class MiniMaxAIPlayer : IPlayer, 
    {
        PlayerID aiPlayer;
        PlayerID human;
        uint lookAhead;
        public MiniMaxAIPlayer(PlayerID player, uint lookAhead)
        {
            aiPlayer = player;
            human = aiPlayer.Other();
            this.lookAhead = lookAhead;
        }

        Random rnd = new Random();

        public uint Play(Board board)
        {
            Thread.Sleep(1000);

            // Check if AI wins
            var cols = FindWinningCols(board, aiPlayer);
            if (cols.Count > 0)
            {
                return cols.First();
            }

            // Check if human wins
            cols = FindWinningCols(board, human);
            if (cols.Count > 0)
            {
                return cols.First();
            }

            // Use MiniMax algorithm to find best next move
            return 0;
        }


    }
}