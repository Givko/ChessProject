using System.Collections.Generic;

namespace Chess.GameEngine.Models.GameModels.ChessBoardModels
{
    public class Board
    {
        public IEnumerable<Piece> Pieces { get; set; }
    }
}