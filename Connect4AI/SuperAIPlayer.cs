using connect_4_core;
using Connect4AI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace connect_4
{
    enum BoardScore : uint
    {
        SureVictory, // 2 places to win
        PossibleVictory, // 1 place to win
        Unknown // Nothing
    }

    public class SuperAIPlayer : IPlayer
    {
        PlayerID aiPlayer;
        PlayerID human;
        uint lookAhead;
        public SuperAIPlayer(PlayerID player, uint lookAhead)
        {
            aiPlayer = player;
            human = aiPlayer.Other();
            this.lookAhead = lookAhead;
        }

        VictoryChecker victoryChecker = new VictoryChecker();
        BoardGenerator boardGenerator = new BoardGenerator();

        Random rnd = new Random();
        public uint Play(Board board)
        {
            Thread.Sleep(1000);

            // Check if AI wins
            var cols = FindWinningCols(board, aiPlayer);
            if (cols.Count > 0) {
                return cols.First();
            }

            // Check if human wins
            cols = FindWinningCols(board, human);
            if (cols.Count > 0)
            {
                return cols.First();
            }

            var pCols = FindPotentialCols(board);

            uint index = (uint)rnd.Next(pCols.Length);
            return pCols[index];
        }


        private uint[] FindPotentialCols(Board b)
        {
            var cols = new HashSet<uint>();
            // Generate all boards
            var boards = boardGenerator.GenerateAllBoards(b, new PlaySequenceSet(), lookAhead, aiPlayer);
            // Evaluate each board
            var allScores = new Dictionary<SuperBoard, BoardScore>();
            foreach (var e in boards)
            {
                var bo = e.Value.Board;
                var bs = EvaluateBoard(bo, aiPlayer);
                Debug.WriteLine(bo.ToString(), bs);
                Debug.WriteLine("-------");
                allScores.Add(e.Value, bs);
            }

            // Drop boards with unknown board score
            var scores = allScores.Where(pair => pair.Value != BoardScore.Unknown)
                                 .ToDictionary(pair => pair.Key,
                                               pair => pair.Value);
            // Find boards with sure victory
            var sureVictory = scores.Where(pair => pair.Value == BoardScore.SureVictory)
                                     .Select(pair => pair.Key).ToList();

            // If more than 0, extract all play sequences
            //     Return first col from each play sequence
            // Else, find boards with potential victory
            // If more than 0, extract all play sequence
            //     Return first col from each play sequence

            return cols.ToArray();
        }

        // Finds columns that lead to a sure victory for AI player or human player on their next turn
        private Dictionary<uint, Board> CheckBoards(HashSet<Board> boards, PlayerID player)
        {
            var result = new Dictionary<uint, Board>();
            foreach(Board b in boards)
            {
                var cols = FindWinningCols(b, player);
            }

            return result;
        }

        private HashSet<uint> FindWinningCols(Board board, PlayerID player)
        {
            var cols = new HashSet<uint>();
            for (uint i = 0; i < 7; i++)
            {
                //check if column is full
                if (board.IsColFull(i)) {
                    continue;
                }

                //clone board and drop piece in current col
                var b = new Board(board);
                b.DropPiece(i, player);

                //if top row is replaced with ai piece checks if the ai will win
                if (victoryChecker.CheckVictory(b, i, player))
                {
                    cols.Add(i);
                }
            }
            return cols;
        }

        private BoardScore EvaluateBoard(Board b, PlayerID p)
        {
            var sb = new SuperBoard(b, new PlaySequenceSet());
            var bg = new BoardGenerator();
            var vc = new VictoryChecker();
            var boards = bg.GenerateBoards(sb, p);
            uint counter = 0;

            foreach (var e in boards) {
                for (uint i = 0; i < 7; i++)
                {
                    if (vc.CheckVictory(e.Value.Board, i, p.Other()))
                    {
                        counter++;
                        break;
                    }
                }
            }

            if (counter == 0)
            {
                return BoardScore.Unknown;
            } else if (counter < boards.Count)
            {
                return BoardScore.PossibleVictory;
            } else
            {
                return BoardScore.SureVictory;
            }
        }
    }
}