#pragma once
#include <iostream>

#include "ChessPiece.h"
class Queen :
    public ChessPiece
{
private:
    bool checkPath(int* newCord);

public:
    Queen(int cords[2], bool white);
    virtual void move(int* newCord) override;

};

