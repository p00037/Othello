using Othello.Shared;
using System;

namespace Othello.AI
{
    public class AI : IAi
    {
        private Piece turnPiece;

        public AI(Piece turnPiece)
        {
            this.turnPiece = turnPiece;
        }

        public BoardPoint HitPiece(Piece[,] board)
        {
            var turn = new Turn(board, turnPiece);
            
            for (int i = 0; i < 100000; i++)
            {
                Random cRandom = new Random();
                int x = cRandom.Next(7);
                int y = cRandom.Next(7);
                if (turn.IsEffectiveJudgment(x, y) == true)
                {
                    return new BoardPoint(x, y);
                }
            }

            for (int x = 0; x < BoardState.MAX_POS_X; x++)
            {
                for (int y = 0; y < BoardState.MAX_POS_Y; y++)
                {
                    if (turn.IsEffectiveJudgment(x, y) == true)
                    {
                        return new BoardPoint(x, y);
                    }

                }
            }

            return new BoardPoint(-1, -1);
        }
    }
}
