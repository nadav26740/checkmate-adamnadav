#include "Queen.h"

bool Queen::checkPath(int* newCord)
{
	bool destPieceWhite = false;
	char destChar = this->_gameBoard->CheckCoard(newCord);// gets the name of the piece in the new cord

	if (destChar != '#')// checks if there is a piece in the place of the new cord
	{
		if (destChar <= 'Z')
		{
			destPieceWhite = true;
		}
	}


	if (destChar != '#' && destPieceWhite == this->_white)// check if the piece is of the same color
	{
		throw ChessEvents(INVALIED_DEST_PIECE_OWN, "In The Dest there is your own Piece");
		return false;
	}
	else if ((abs(*newCord - this->_cords[0]) != abs(*(newCord + 1) - this->_cords[1])) || (newCord[0] != this->_cords[0] && newCord[1] != this->_cords[1]))// check if legal move
	{
		throw ChessEvents(INVALIED_PIECE_MOVE, "The King Doing invalid move that he can't do!");
		return false;
	}
	else if (*(newCord) > 7 || *(newCord + 1) > 7 || *(newCord) < 0 || *(newCord + 1) < 0)// check if new cord is out the board 
	{
		throw ChessEvents(INVALIED_OUT_OF_INDEX, "The index that's insert is out of index");
		return false;
	}
	else if (*(newCord) == this->_cords[0] && *(newCord + 1) == this->_cords[1])// check if the new cord is the old cord
	{
		throw ChessEvents(INVALIED_SOURCE_EQL_DEST, "xD The source and the dest are the same place");
		return false;
	}
	else
	{
		this->_gameEvent.setEvent(VALIED_MOVE, "valid move");
		return true;
	}
}

Queen::Queen(int cords[2], bool white, Board* gameBoard) : ChessPiece(cords, white, gameBoard)
{
}

void Queen::move(int* newCord)
{
	try
	{
		this->checkPath(newCord);
		this->_gameBoard->move(this->_cords, newCord);
		this->_cords[0] = (newCord[0]);
		this->_cords[1] = (newCord[1]);
	}
	catch (ChessEvents& e)
	{
		throw e;
	}
}
