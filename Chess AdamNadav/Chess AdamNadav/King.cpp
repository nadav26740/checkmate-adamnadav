#include "King.h"

/**the method will check if there is a piece of the same color in the new cord or the new cord doesnt mach the kings move
* input - pointer to int of new cord
* output - bool true if move alowed false if not
*/
bool King::checkPath(int* newCord)
{
	bool destPiece = false;
	char destChar = this->_gameBoard->CheckCoard(newCord);// gets the name of the piece in the new cord

	if (destChar != '#')// checks if there is a piece in the place of the new cord
	{
		if (destChar <= 'Z')
		{
			destPiece = true;
		}
	}

	if (destChar != '#' && destPiece == this->_white)// check if the piece is of the same color
	{
		throw ChessEvents(INVALIED_DEST_PIECE_OWN, "In The Dest there is your own Piece");
	}
	else if (abs(*newCord - this->_cords[0]) > 1 || abs(*(newCord + 1) - this->_cords[1]) > 1)// check if legal move
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
	try
	{
	this->checkPath(newCord);
	this->_cords[0] = (newCord[0]);
	this->_cords[1] = (newCord[1]);
	}
	catch (ChessEvents& e )
	{
		throw e;
	}
}
