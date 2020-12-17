#pragma once
#include <iostream>

class ChessPiece
{
private:
	int  _cords[2]; // { x, y }
	bool _white;
	char _name;
public:
	ChessPiece(int cords[2], bool white);
	virtual ~ChessPiece();
	int* getlocation(); // return _cords
	virtual void move(int* newCords) = 0; // virtual
	bool getWhite(); // return _White
	char getName();

};

