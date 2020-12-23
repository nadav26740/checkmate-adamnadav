#include <iostream>
#include <string>
#include "Board.h"
#include "GameFunctions.h"
#include "ChessPiece.h"

int main()
{
	int oldCords[2] = { 0 , 0 };
	int newCords[2] = { 6 , 0 };
	Board* gameBoard = new Board();
	ChessPiece *temp = nullptr;
	std::cout << gameBoard->getBoardString();
	temp = GameFunctions::createPieceChar(gameBoard->CheckCoard(0, 0), oldCords, gameBoard);
	std::cout << temp->getName() << "|" << temp->getWhite() << std::endl;
	temp->move(newCords);
	std::cout << gameBoard->getBoardString();
	delete temp;
	delete gameBoard;
	return 0;
}