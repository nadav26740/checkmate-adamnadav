#include "Board.h"

void Board::createPiece(int cords[2], int pieceType)
{
    if (boardPieces[8 * cords[0] + cords[1]] != nullptr)
    {
        delete boardPieces[8 * cords[0] + cords[1]];
        boardPieces[8 * cords[0] + cords[1]] = nullptr;
    }

    switch (pieceType)
    {
    case 0:
        boardPieces[8 * cords[0] + cords[1]] = new Pawn;
        break;

    case 1:
        boardPieces[8 * cords[0] + cords[1]] = new Bishop;
        break;

    case 2:
        boardPieces[8 * cords[0] + cords[1]] = new King;
        break;

    case 3:
        boardPieces[8 * cords[0] + cords[1]] = new Queen;
        break;

    case 4:
        boardPieces[8 * cords[0] + cords[1]] = new Knight;
        break;

    case 5:
        boardPieces[8 * cords[0] + cords[1]] = new Rock;
        break;

    default:
        throw (std::string("Piece type wasn't found"));
    }
}

Board::Board()
{
    int i = 0;
    for (i = 0; i < 64; i++)
    {
        boardPieces[i] = nullptr;
    }
    this->resetBoard();
}

Board::~Board()
{
    int i = 0;
    for (i = 0; i < 64; i++)
    {
        if (boardPieces[i] != nullptr)
        {
            delete boardPieces[i];
            boardPieces[i] = nullptr;
        }
    }
}

ChessPiece** Board::getBoard()
{
    return boardPieces;
}

ChessPiece* Board::CheckCoard(int cords[2])
{
    return boardPieces[cords[0]*8 + cords[1]];
}

void Board::move(int oldCords[2], int newCords[2], ChessPiece* piece)
{
    boardPieces[oldCords[0] * 8 + oldCords[1]] = nullptr;
    if (boardPieces[newCords[0] * 8 + newCords[1]] != nullptr)
    {
        delete boardPieces[newCords[0] * 8 + newCords[1]];
    }
    boardPieces[newCords[0] * 8 + newCords[1]] = piece;
}

void Board::resetBoard()
{
    int i = 0;
    for (i = 0; i < 64; i++)
    {
        if (boardPieces[i] != nullptr)
        {
            delete boardPieces[i];
        }
        boardPieces[i] = nullptr;
    }
}
