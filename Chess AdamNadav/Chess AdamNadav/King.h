#pragma once
#include <iostream>

#include "ChessPiece.h"
class King :
    public ChessPiece
{
private:
    bool checkPath(int* newCord);

public:
    King(int cords[2], bool white, Board* gameBoard);
    virtual void move(int* newCord);

};

