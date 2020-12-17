#pragma once
#include <iostream>

#include "ChessPiece.h"
class Rock :
    public ChessPiece
{
private:
    bool checkPath(int* newCord);

public:
    Rock(int cords[2], bool white);
    virtual void move(int* newCord) override;

};

