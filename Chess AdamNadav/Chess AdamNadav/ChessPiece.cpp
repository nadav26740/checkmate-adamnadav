#include "ChessPiece.h"

ChessPiece::ChessPiece(int cords[2], bool white, Board* gameBoard)
{
	this->_white = white;
	this->_cords[0] = cords[0];
	this->_cords[1] = cords[1];
}

ChessPiece::~ChessPiece()
{
	
}

bool ChessPiece::getWhite()
{
	return _white;
}

char ChessPiece::getName()
{
	return this->_name;
}

