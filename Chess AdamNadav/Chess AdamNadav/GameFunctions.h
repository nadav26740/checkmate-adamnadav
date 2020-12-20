#pragma once
#include "ChessPiece.h" 
#include "King.h"
#include "Bishop.h"
#include "King.h"
class GameFunctions
{
public:
	static ChessPiece* createPieceChar(char type); // getting char and creating Dynamic ChessPiece	
};

