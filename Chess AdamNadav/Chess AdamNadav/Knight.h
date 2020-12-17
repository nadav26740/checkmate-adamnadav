#pragma once
#include <iostream>

#include "ChessPiece.h"
class Knight :
    public ChessPiece
{
private:
    bool checkPath(int* newCord);

public:
    Knight(int cords[2], bool white);
    virtual void move(int* newCord) override;

};

