using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.GameEngine.Models.GameModels.ChessBoardModels
{
    public class Piece
    {
        public Position Position { get; set; }

        public PieceType Type { get; set; }
    }
}
