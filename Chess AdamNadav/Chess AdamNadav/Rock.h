#pragma once
#include <iostream>
#include "GameFunctions.h"
#include "ChessPiece.h"

class Rock : public ChessPiece
{
private:
    bool checkPath(int* newCord);

public:
    Rock(int cords[2], bool white, Board* gameBoard);
    virtual void move(int* newCord);

};

