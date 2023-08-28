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

                var pieceCount = board.CountPieces();
                var player = pieceCount % 2 == 0 ? PlayerID.One : PlayerID.Two;

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
            var score = 0;
            // score 100/-100 for each sure victory
            var win = VictoryChecker.CheckVictory(board, board.LastPiece, board.LastPlayer);
            if (win)
            {
                if ((int)board.LastPlayer == (int)maximizingPlayer)
                {
                    return 100;
                }
                else
                {
                    return -100;
                }
            } 
            
            // if the last piece was played in the center column, add 30
            if (board.LastPiece == 3)
            {
                score += 30;
            }

            // score 10/-10 for each 3-in-a-row with empty slot

            // in each direction (except up)
            // starting from the last played piece, go 3 steps in a direction if possible
            // if 3 steps are not possible, continue
            // if an enemy piece is encountered, continue
            // if the number of pieces is 3, add 10 points

            // check for center
            var score = 0;
            var center = cells[1, 1];
            if (center == (int)maximizingPlayer)
            {
                score += 6;
            }
            else if (center != 0) // other player got the center
            {
                score -= 6;
            }

            // check for potential victories
            var victoryCount = 0;
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    if (cells[j, i] == 0)
                    {
                        var node = new TicTacToe(displayState.ToString());
                        node.Place(j, i, maximizingPlayer);
                        if (node.checkVictory() == (int)maximizingPlayer)
                        {
                            victoryCount++;
                            if (victoryCount > 1) // multipl victory positions
                            {
                                return 1000;
                            }
                        }
                    }
                }
            }
            if (victoryCount > 0)
            {
                score += 2;
            }

            return score;
        }
    }

    public  CountPieces(Board b9)

    public class MiniMaxAIPlayer : IPlayer
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
            var cols = board.FindWinningCols(aiPlayer);
            if (cols.Count > 0)
            {
                return cols.First();
            }

            // Check if human wins
            cols = board.FindWinningCols(human);
            if (cols.Count > 0)
            {
                return cols.First();
            }

            // Use MiniMax algorithm to find best next move
            return 0;
        }


    }
}