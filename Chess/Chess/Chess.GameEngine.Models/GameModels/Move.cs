using Chess.GameEngine.Models.GameModels.ChessBoardModels;
using System;

namespace Chess.GameEngine.Models.GameModels
{
    public class Move
    {
        /// <summary>
        /// Represents the id of the player making the move
        /// </summary>
        public Guid PlayerId { get; set; }

        /// <summary>
        /// Represents the Piece being moved
        /// </summary>
        public Piece Piece { get; set; }

        /// <summary>
        /// Represents the new position of the moved pieced
        /// </summary>
        public Position NewPosition { get; set; }

        /// <summary>
        /// Represents the state of the board before the piece is moved
        /// </summary>
        public Board Board { get; set; }
    }
}