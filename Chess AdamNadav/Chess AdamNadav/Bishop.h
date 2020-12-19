#pragma once
#include <iostream>

#include "ChessPiece.h"
class Bishop :
    public ChessPiece
{
private:
    bool checkPath(int* newCord);
public:
    Bishop(int cords[2], bool white, Board* gameBoard);
    virtual void move(int* newCord);

};

