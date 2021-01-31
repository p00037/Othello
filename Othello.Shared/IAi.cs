using System;
using System.Collections.Generic;
using System.Text;

namespace Othello.Shared
{
    public interface IAi
    {
        BoardPoint HitPieceBoardPoint(Piece[,] myBord);
    }
}
