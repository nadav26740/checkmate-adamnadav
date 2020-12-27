#include "Pawn.h"

bool Pawn::checkPath(int* newCord)
{
	ChessPiece* temp;
	bool firstmove = false;
	int side = 1;
	if (this->_white == true)
	{
		side *= -1;
	}

	if (this->_cords[0] == 1 || this->_cords[0] == 6)
	{
		firstmove = true;
	}

	temp = GameFunctions::createPieceChar(this->_gameBoard->CheckCoard(newCord), newCord, this->_gameBoard);
	if (temp != nullptr)
	{
		if (temp->getWhite() != this->_white && this->_cords[0] + side == newCord[0] && (this->_cords[1] + 1 == newCord[1] || this->_cords[1] - 1 == newCord[1]))
		{
			delete temp;
			return true;
		}
		else if (!(this->_cords[0] + side == newCord[0] && (this->_cords[1] + 1 == newCord[1] || this->_cords[1] - 1 == newCord[1])))
		{
			delete temp;
			throw ChessEvents(INVALIED_PIECE_MOVE, "The piece Can't eat like that");
		}
		else
		{
			delete temp;
			throw ChessEvents(INVALIED_DEST_PIECE_OWN, "in the dest there is your own piece");
		}
	}

	if (!(this->_cords[0] + side == newCord[0] || 
		((firstmove && this->_cords[0] + (2 * side) == newCord[0]) && this->_gameBoard->CheckCoard(newCord[0] - (side), newCord[1]) == '#')))
	{
		throw ChessEvents(INVALIED_PIECE_MOVE, "Pawn can't move like that");
	}
	
	if (this->_cords[1] != newCord[1])
	{
		throw ChessEvents(INVALIED_PIECE_MOVE, "Pawn can't move vartical");
	}
	return true;
}

Pawn::Pawn(int cords[2], bool white, Board* gameBoard) : ChessPiece(cords, white, gameBoard)
{

}

void Pawn::move(int* newCord)
{
	try
	{
		checkPath(newCord);
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
