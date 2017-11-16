using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public static class Extansoins
    {
        public static PieceColor GetOpponentColor(this PieceColor pieceColor)
        {
            return pieceColor == PieceColor.Black ? PieceColor.White : PieceColor.Black;
        }
    }
}
