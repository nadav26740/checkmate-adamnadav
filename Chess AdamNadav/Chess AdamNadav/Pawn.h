#pragma once
#include <iostream>

#include "ChessPiece.h"
class Pawn :
    public ChessPiece
{
private:
    bool checkPath(int* newCord);

public:
    Pawn(int cords[2], bool white);
    virtual void move(int* newCord) override;

};

