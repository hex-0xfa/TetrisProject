using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisProject
{
    class SquarePiece : Piece
    {
        public SquarePiece(RotationStateClockwise newRotationSateClockwise, int newRow, int newColumn)
         : base (newRotationSateClockwise, newRow, newColumn, GameConstants.numberSquarePiece)
        {
            PieceGrid = SquarePiece.ConfigureMap();
            for (Piece.RotationStateClockwise incrementRotationStateClockwise = RotationStateClockwise.CW0; incrementRotationStateClockwise < MyRotationSateClockwise; incrementRotationStateClockwise++)
            {
                PieceGrid = Piece.RotateClockwise(PieceGrid);
            }
        }

        public static int[,] ConfigureMap()
        {
            int[,] newPieceGrid = { { 0, 0, 0, 0 }, { 0, 1, 1, 0 }, { 0, 1, 1, 0 }, { 0, 0, 0, 0 } };
            return newPieceGrid;
        }
    }
}
