#include <iostream>
#include <string>
#include "Board.h"
#include "GameFunctions.h"
#include "ChessPiece.h"

int main()
{
	int oldCords[2] = { 0 , 0 };
	int newCords[2] = { 0 , 0 };
	Board* gameBoard = new Board();
	std::cout << gameBoard->getBoardString();
	
	delete gameBoard;
	return 0;
}