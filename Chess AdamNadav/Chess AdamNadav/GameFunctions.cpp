#include "GameFunctions.h"

ChessPiece* GameFunctions::createPieceChar(char type, int cords[2], Board* gameBoard)
{
	bool isWhite = false;
	ChessPiece* newPiece = nullptr;

	if (type >= 'A' && type <= 'Z') // check if white or black
	{
		isWhite = true;
	}
	else
	{
		type = type - 'a' + 'A';
		isWhite = false;
	} 
	 
	switch (type) // P - Pawn | B - Bishop | K - King | Q - Queen | N - Knight | R - Rock
	{
	case 'P':
		newPiece = new Pawn(cords, isWhite, gameBoard);
		break;

	case 'B':
		newPiece = new Bishop(cords, isWhite, gameBoard);
		break;

	case 'K':
		newPiece = new King(cords, isWhite, gameBoard);
		break;

	case 'Q':
		newPiece = new Queen(cords, isWhite, gameBoard);
		break;

	case 'N':
		newPiece = new Knight(cords, isWhite, gameBoard);
		break;

	case 'R':
		newPiece = new Rock(cords, isWhite, gameBoard);
		break;

	default:
		newPiece = nullptr;
	}
	return newPiece;
}

bool GameFunctions::checkCheck(Board* gameBoard, bool isWhite)
{
	char king = '#';

	if (isWhite)
	{
		king = 'K';
	}
	else
	{
		king = 'k'
	}

	for (i = 0; i < 8; i++)
	{

	}
	return false;
}
