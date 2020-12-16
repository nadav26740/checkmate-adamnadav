#pragma once
#include <iostream>
#include <string>
#include "ChessPiece.h"
#include "Pawn.h"
#include "Bishop.h"
#include "King.h"
#include "Queen.h"
#include "Knight.h"
#include "Rock.h"

class Board
{
private:
	ChessPiece* boardPieces[64]; // location on board 
	void createPiece(int cords[2], int pieceType); // 0 - Pawn | 1 - Bishop | 2 - King | 3 - Queen | 4 - Knight | 5 - Rock
	std::string names[2];
public:
	Board();
	~Board();
	ChessPiece** getBoard(); // returning the boardPieces
	ChessPiece* CheckCoard(int cords[2]); // returning the Piece in the cord (nullptr if no piece) // doesn't check vaild
	void move(int oldCords[2], int newCords[2], ChessPiece* piece); // putting nullptr in old cords and put the piece in the new cord (if there was already piece deleting him)
	void resetBoard(); // reseting the board, creating the Chess Pieces and putting tham in the place 
};

