#include "Pawn.h"

bool Pawn::checkPath(int* newCord)
{
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
	if (this->_cords[0] + (1 * side) || ((this->_cords[0] == 1 || this->_cords[0] == 6) && this->_cords[0] + (2 * side)))
	{

	}
	return false;
}

Pawn::Pawn(int cords[2], bool white, Board* gameBoard) : ChessPiece(cords, white, gameBoard)
{

}

void Pawn::move(int* newCord)
{
}
