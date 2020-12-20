#pragma once
#include <iostream>
#include "Board.h"
#include "ChessEvents.h"

class ChessPiece
{
protected:
	int  _cords[2]; // { x, y }
	bool _white;
	char _name;
	Board* _gameBoard;
	ChessEvents _gameEvent;
public:
	ChessPiece(int cords[2], bool white, Board* gameBoard);
	virtual ~ChessPiece();
	int* getlocation(); // return _cords
	virtual void move(int* newCords) = 0; // virtual
	bool getWhite(); // return _White
	char getName();
	eventDetails getDetails();
};