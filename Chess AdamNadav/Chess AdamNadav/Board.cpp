#include "Board.h"

void Board::createPiece(int cords[2], int pieceType, bool isWhite)
{
	char newPiece = '#';
	switch (pieceType)
	{
	case 0:
		newPiece = 'P';
		break;

	case 1:
		newPiece = 'B';
		break;

	case 2:
		newPiece = 'K';
		break;

	case 3:
		newPiece = 'Q';
		break;

	case 4:
		newPiece = 'N';
		break;

	case 5:
		newPiece = 'R';
		break;

	default:
		throw "Iliegal type!";
	}
	
	if (isWhite == false)
	{
		newPiece = newPiece - 'A' + 'a';
	}

	boardPieces[8 * cords[0] + cords[1]] = newPiece;
}

Board::Board()
{
	resetBoard();
}

Board::~Board()
{
	boardPieces.clear();
}

void Board::setBoard(std::string newBoard)
{
	boardPieces = newBoard;
}

std::string Board::getBoard()
{
	return std::string(boardPieces);
}

std::string Board::getBoardString()
{
	std::string boardString;
	int i = 0, j = 0;
	for (i = 0; i < 8; i++)
	{
		for (j = 0; j < 8; j++)
		{
			boardString += boardPieces[i * 8 + j];
			boardString += ' ';
		}
		boardString += '\n';
	}
	return boardString;
}

char Board::CheckCoard(int cords[2])
{
	return boardPieces[8* cords[0] + cords[1]];
}

char Board::CheckCoard(int x, int y)
{
	int temp[2] = { x, y };
	return this->CheckCoard(temp);
}

void Board::move(int oldCord[2], int newCords[2])
{
	char temp = 0;
	temp = boardPieces[8 * oldCord[0] + oldCord[1]];
	boardPieces[8 * newCords[0] + newCords[1]] = temp;
	boardPieces[8 * oldCord[0] + oldCord[1]] = '#';
}

void Board::resetBoard()
{
	boardPieces = "rnbkqbnr#ppppppp################################PPPPPPPPRNBKQBNR";
}

