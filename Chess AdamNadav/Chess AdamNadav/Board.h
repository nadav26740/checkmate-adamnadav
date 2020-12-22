#pragma once
#ifndef _BOARD_H
#define _BOARD_H

#include <iostream>
#include <string>

class Board
{
private:
	std::string boardPieces; // location on board 
	void createPiece(int cords[2], int pieceType, bool isWhite); // 0 - Pawn | 1 - Bishop | 2 - King | 3 - Queen | 4 - Knight | 5 - Rock

public:
	Board();
	~Board();
	void setBoard(std::string newBoard); // setBoard(oldBoard->getBoard())
	std::string getBoard(); // returning the boardPieces
	std::string getBoardString(); // returning the Board Pieces in string with endl
	char CheckCoard(int cords[2]); // returning the Piece in the cord (nullptr if no piece) // doesn't check vaild
	void move(int oldCord[2], int newCords[2]); // putting nullptr in old cords and put the piece in the new cord (if there was already piece deleting him)
	void resetBoard(); // reseting the board, creating the Chess Pieces and putting tham in the place 

};

#endif