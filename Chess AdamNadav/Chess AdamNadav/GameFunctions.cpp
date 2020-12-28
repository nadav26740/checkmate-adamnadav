#include "GameFunctions.h"

using namespace std;

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
	bool ret = false;
	int i = 0;
	int j = 0;
	int* kingCord = new int(2);
	string currKing = "#";
	string singleString = "#";
	string gameBoardString = gameBoard->getBoardString();


	if (isWhite)
	{
		currKing = "K";
	}
	else
	{
		currKing = "k";
	}

	for (i = 0; i < 63; i++)
	{
		singleString = gameBoardString[i];
		if (singleString.compare(currKing))
		{
			kingCord[0] = (i % 7) - 1;
			kingCord[1] = i / 7;
		}
	}

	for (i = kingCord[0]; i < 7; i++)
	{
		if (/*cord[i][kingCord[1]] == my pice*/)
		{
			ret = false;
		}
		else if (/*cord[i][kingCord[1]] == not my but not dangorues*/)
		{
			ret = false;
		}
		else if (/*cord[i][kingCord[1]] == not my dangorous*/)
		{
			ret = true;
		}
		else// incase i missed something
		{
			ret = false;
		}
	}

	return ret
}

void GameFunctions::stringToCords(int oldCords[2], int newCords[2], std::string stringCords)
{
	// check the cords our valid
	if (stringCords[0] < 'a' || stringCords[0] > 'h'
		|| stringCords[2] < 'a' || stringCords[2] > 'h'
		|| stringCords[1] < '1' || stringCords[1] > '8'
		|| stringCords[3] < '1' || stringCords[3] > '8')
	{
		throw ChessEvents(INVALIED_OUT_OF_INDEX, "The cords out of index");
	}
	if (stringCords[0] == stringCords[2] && stringCords[1] == stringCords[3])
	{
		throw ChessEvents(INVALIED_SOURCE_EQL_DEST, "The source cords and the dest equal");
	}

	// converting the cords to cords we using
	oldCords[1] = stringCords[0] - 'a';
	oldCords[0] = 7 - (stringCords[1] - '1');
	newCords[1] = stringCords[2] - 'a';
	newCords[0] = 7 - (stringCords[3] - '1');
}

void GameFunctions::switchPlayer(bool& currect)
{
	if (currect)
	{
		currect = false;
	}
	else
	{
		currect = true;
	}
}
