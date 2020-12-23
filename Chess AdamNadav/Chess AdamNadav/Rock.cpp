#include "Rock.h"

/* enum { // events Defines
	VALIED_MOVE = 0,
	VALIED_CHESS, // 1
	INVALIED_PIECE_NOT_ALLOWED, // 2 v
	INVALIED_DEST_PIECE_OWN, // 3 v
	INVALIED_CHESS_WILL_BECOME, // 4
	INVALIED_OUT_OF_INDEX, // 5 v
	INVALIED_PIECE_MOVE, // 6 v
	INVALIED_SOURCE_EQL_DEST, // 7 v
	VALIED_CHESS_MATE, // 8
}; */

bool Rock::checkPath(int* newCord)
{
	ChessPiece* tempPiece = nullptr;
	int tempcords[2] = {this->_cords[0] , this->_cords[1]};

	if (newCord[0] == this->_cords[0] && newCord[1] == this->_cords[1])
	{
		throw ChessEvents(INVALIED_SOURCE_EQL_DEST, "xD The source and the dest is the same place");
		return false;
	}

	else if (newCord[0] > 7 || newCord[1] > 7 || newCord[0] < 0 || newCord[1] < 0) // check if the index is valid
	{
		throw ChessEvents(INVALIED_OUT_OF_INDEX, "The index that's insert is out of index");
		return false;

	}

	else if ((newCord[0] != this->_cords[0] && newCord[1] != this->_cords[1])) // check if the piece doesn't doing invalid move
	{
		throw ChessEvents(INVALIED_PIECE_MOVE, "The Rook Doing invalid move that he can't do!");
		return false;

	}

	else { // check that in dest there is no own chess Piece
		tempPiece = GameFunctions::createPieceChar(this->_gameBoard->CheckCoard(newCord), newCord, this->_gameBoard);
		if (tempPiece != nullptr && tempPiece->getWhite() == this->_white)
		{
			throw ChessEvents(INVALIED_DEST_PIECE_OWN, "In the dest there your own Piece");
			return false;

		}
	}

	while (tempcords[0] != newCord[0] || tempcords[1] != newCord[1]) // check if the rook doesn't jumping over other chess piece
	{
		if (tempcords[0] > newCord[0])
		{
			tempcords[0]--;
		}
		else if (tempcords[0] < newCord[0])
		{
			tempcords[0]++;
		}
		else if (tempcords[1] > newCord[1])
		{
			tempcords[1]--;
		}
		else if (tempcords[1] < newCord[1])
		{
			tempcords[1]++;
		}
		if (!(tempcords[0] == newCord[0] && tempcords[1] == newCord[1]) && this->_gameBoard->CheckCoard(tempcords) != '#')
		{
			throw ChessEvents(INVALIED_PIECE_MOVE, "The rook jumping over a piece");
			return false;
		}
	}

	return true;
}

Rock::Rock(int cords[2], bool white, Board* gameBoard) : ChessPiece(cords, white, gameBoard)
{
	this->_name = 'r';
}

void Rock::move(int* newCord)
{
	try {
		this->checkPath(newCord);
		this->_gameBoard->move(_cords, newCord);
		_cords[0] = newCord[0];
		_cords[1] = newCord[1];
		this->_gameEvent.setEvent(VALIED_MOVE, "Valid move!");
	}

	catch (ChessEvents& e)
	{
		throw e;
	}
}
