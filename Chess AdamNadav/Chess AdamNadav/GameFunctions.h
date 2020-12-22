#pragma once
#include "ChessPiece.h" 
#include "King.h"
#include "Bishop.h"
#include "Knight.h"
#include "Pawn.h"
#include "Queen.h"
#include "Rock.h"

class Rock;
class Queen;
class Pawn;
class Knight;
class Bishop;
class King;
class ChessPiece;
class Board;
class GameFunctions
{
public:
	static ChessPiece* createPieceChar(char type, int cords[2], Board* gameBoard); // getting char and creating Dynamic ChessPiece
	bool checkCheck(Board* gameBoard, bool isWhite);
};

