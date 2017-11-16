namespace Chess
{
	public class ChessProblem
	{
		private static Board board;
		public static ChessStatus ChessStatus { get; set; }

		public static void LoadFrom(string[] lines)
		{
			board = new BoardParser().ParseBoard(lines);
		}

		// Определяет мат, шах или пат белым.
	    public static void CalculateChessStatus()
	    {
	        var isCheck = IsCheckFor(PieceColor.White);
	        var hasMoves = false;
	        foreach (var locFrom in board.GetPieces(PieceColor.White))
	        {
	            foreach (var locTo in board.GetPiece(locFrom).GetMoves(locFrom, board))
	            {
	                var old = board.GetPiece(locTo);
	                MakeMove(locTo, locFrom);
	                if (!IsCheckFor(PieceColor.White))
	                    hasMoves = true;
	                MakeMove(locFrom, locTo, old);
                }
	        }
	        if (isCheck)
	            if (hasMoves)
	                ChessStatus = ChessStatus.Check;
	            else ChessStatus = ChessStatus.Mate;
	        else if (hasMoves) ChessStatus = ChessStatus.Ok;
	        else ChessStatus = ChessStatus.Stalemate;
	    }

	    private static void MakeMove(Location to, Location from, Piece piece=null)
	    {
	        board.Set(to, board.GetPiece(from));
	        board.Set(from, piece);
        }

	    private static PieceColor CheckChess()
	    {
	        
	    } 
	    // check — это шах
		private static bool IsCheckFor(PieceColor pieceColor)
		{
			var isCheck = false;
		    var opponentColor = pieceColor.GetOpponentColor();
			foreach (var loc in board.GetPieces(opponentColor))
			{
				var piece = board.GetPiece(loc);
				var moves = piece.GetMoves(loc, board);
				foreach (var destination in moves)
				{
					if (board.GetPiece(destination).Is(pieceColor, PieceType.King))
						isCheck = true;
				}
			}
			if (isCheck) return true;
			return false;
		}
	}
}