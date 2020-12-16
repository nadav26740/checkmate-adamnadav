#pragma once
class ChessPiece
{
private:
	int  _cords[2]; // { x, y }
	bool _white;

public:
	int* getlocation(); // return _cords
	virtual void move(int* newCords, bool playerWhite) = 0; // virtual
	bool getWhite(); // return _White

};

