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
	int kingCord = 0;
	string currKing = "#";
	string myR = "#";
	string myN = "#";
	string myB = "#";
	string myQ = "#";
	string myP = "#";
	string enemyR = "#";
	string enemyN = "#";
	string enemyB = "#";
	string enemyQ = "#";
	string enemyP = "#";
	string enemyK = "#";
	string singleString = "#";
	string gameBoardString = gameBoard->getBoardString();


	if (isWhite)
	{
		currKing = "K";
		myR = "R";
		myN = "N";
		myB = "B";
		myQ = "Q";
		myP = "P";
		enemyR = "r";
		enemyN = "n";
		enemyB = "b";
		enemyQ = "q";
		enemyP = "p";
		enemyK = "k";
	}
	else
	{
		currKing = "k";
		myR = "r";
		myN = "n";
		myB = "b";
		myQ = "q";
		myP = "p";
		enemyR = "R";
		enemyN = "N";
		enemyB = "B";
		enemyQ = "Q";
		enemyP = "P";
		enemyK = "K";
	}

	for (i = 0; i < 63; i++)
	{
		singleString = gameBoardString[i];
		if (singleString.compare(currKing))
		{
			kingCord = i;
		}
	}

	for (i = kingCord; i < 64; i += 8)// checking above king
	{
		singleString = gameBoardString[i];
		if (singleString.compare(myR) || singleString.compare(myN) || singleString.compare(myB) || singleString.compare(myQ) || singleString.compare(myP))
		{
			ret = false;
		}
		else if (singleString.compare(enemyB) || singleString.compare(enemyN) || singleString.compare(enemyP) || (singleString.compare(enemyK) && i - kingCord != 1))
		{
			ret = false;
		}
		else if ((singleString.compare(enemyK) && i - kingCord == 1))
		{
			ret = true;
		}
		else// incase i missed something
		{
			ret = false;
		}
	}

	return ret;
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
