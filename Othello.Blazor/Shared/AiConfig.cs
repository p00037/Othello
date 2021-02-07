using Othello.Shared;
using System.Collections.Generic;

namespace Othello.Blazor.Shared
{
    public class AiConfig
    {
        public AiConfig() { }

        public AiConfig(Piece[,] board, Piece turnPiece, string aiName)
        {
            PieceList = GetPieceList(board);
            TurnPiece = turnPiece;
            AiName = aiName;
        }

        private List<Piece> GetPieceList(Piece[,] board)
        {
            var pieces = new List<Piece>();
            for (int x = 0; x <= BoardState.MAX_POS_X - 1; x++)
            {
                for (int y = 0; y <= BoardState.MAX_POS_Y - 1; y++)
                {
                    pieces.Add(board[x, y]);
                }
            }

            return pieces;
        }

        public List<Piece> PieceList { get; set; } 
        public Piece TurnPiece { get; set; }
        public string AiName { get; set; }

        public Piece[,] GetBoard()
        {
            Piece[,] board = new Piece[BoardState.MAX_POS_X, BoardState.MAX_POS_Y]; ;
            for (int x = 0; x <= BoardState.MAX_POS_X - 1; x++)
            {
                for (int y = 0; y <= BoardState.MAX_POS_Y - 1; y++)
                {
                    board[x, y] = PieceList[x * 8 + y];
                }
            }

            return board;
        }
    }
}
