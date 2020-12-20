#include "King.h"

/**the method will check if there is a piece of the same color in the new cord or the new cord doesnt mach the kings move
* input - pointer to int of new cord
* output - bool true if move alowed false if not
*/
bool King::checkPath(int* newCord)
{
	ChessPiece* destPiece = NULL;
	char destChar = this->_gameBoard->CheckCoard(newCord);// gets the name of the piece in the new cord

	if (destChar != '#')// checks if there is a piece in the place of the new cord
	{
		ChessPiece* destPiece = GameFunctions::checkPieceChar(destChar);
	}

	if (destPiece != NULL && destPiece->getWhite() == this->getWhite())// check if the piece is of the same color
	{
		return false;
	}
	else if (abs(*newCord - this->getLocation()[0]) > 1 || abs(*(newCord + 1) - this->getLocation()[1]) > 1)// check if legal move
	{
		return false;
	}
	else
	{
		return true;
	}
}

King::King(int cords[2], bool white, Board* gameBoard) : ChessPiece(cords, white, gameBoard)
{
}

void King::move(int* newCord)
{
	if (this->checkPath(newCord))
	{
		this->setLocation(newCord);
	}
	else
	{
		throw(ChessEvent(ChessEvent(INVALIED_PIECE_NOT_ALLOWED)));
	}
}
