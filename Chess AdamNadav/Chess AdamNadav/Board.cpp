#include "Board.h"

/// <summary>
/// Getting Cords type and color and creating a piece by that
/// </summary>
/// <param name="cords"> Get int array pointer for where to create the piece</param>
/// <param name="pieceType"> Getting int to know what type of char to create </param>
/// <param name="isWhite"> Getting the color of the piece </param>
void Board::createPiece(int cords[2], int pieceType, bool isWhite)
{
    // checking if there already piece and deleting him
    if (boardPieces[8 * cords[0] + cords[1]] != nullptr)
    {
        delete boardPieces[8 * cords[0] + cords[1]]; 
        boardPieces[8 * cords[0] + cords[1]] = nullptr;
    }

    // switch caase check what piece type to create
    switch (pieceType)
    {
    case 0:
        boardPieces[8 * cords[0] + cords[1]] = new Pawn(cords, isWhite);
        break;

    case 1:
        boardPieces[8 * cords[0] + cords[1]] = new Bishop(cords, isWhite);
        break;

    case 2:
        boardPieces[8 * cords[0] + cords[1]] = new King(cords, isWhite);
        break;

    case 3:
        boardPieces[8 * cords[0] + cords[1]] = new Queen(cords, isWhite);
        break;

    case 4:
        boardPieces[8 * cords[0] + cords[1]] = new Knight(cords, isWhite);
        break;

    case 5:
        boardPieces[8 * cords[0] + cords[1]] = new Rock(cords, isWhite);
        break;

    default:
        throw (std::string("Piece type wasn't found"));
    }
}

/// <summary>
/// Constractor
/// Creating the piece and putting tham in place
/// </summary>
Board::Board()
{
    int i = 0;
    for (i = 0; i < 64; i++)
    {
        boardPieces[i] = nullptr;
    }
    this->resetBoard();
}

/// <summary>
/// Distractor 
/// delete all the piece that left on board
/// </summary>
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

std::string Board::getBoardString()
{
    int i = 0;
    std::string boardString = "";
    for (i = 0; i < 64; i++)
    {
        if (boardPieces[i] == nullptr)
        {
            boardString += '#';
        }
        else
        {
            boardString += boardPieces[i]->getName();
        }
    }
    return boardString;
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
