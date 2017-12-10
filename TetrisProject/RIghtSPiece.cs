using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisProject
{
    /// <summary>
    /// Child class of piece that implement right S piece
    /// </summary>
    class RIghtSPiece : Piece
    {
        public RIghtSPiece(RotationStateClockwise newRotationSateClockwise, int newRow, int newColumn)  //constructor
         : base (newRotationSateClockwise, newRow, newColumn, GameConstants.numberRightSPiece)
        {
            PieceGrid = RIghtSPiece.ConfigureMap();
            for (Piece.RotationStateClockwise incrementRotationStateClockwise = RotationStateClockwise.CW0; incrementRotationStateClockwise < MyRotationSateClockwise; incrementRotationStateClockwise++)
            {
                PieceGrid = Piece.RotateClockwise(PieceGrid);
            }
        }

        public static int[,] ConfigureMap()        //the bit map for right S piece
        {
            int[,] newPieceGrid = { { 0, 0, 0, 0 }, { 0, 0, 1, 1 }, { 0, 1, 1, 0 }, { 0, 0, 0, 0 } };
            return newPieceGrid;
        }
    }
}
