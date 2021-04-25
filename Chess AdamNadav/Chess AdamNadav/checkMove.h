#pragma once
#include <iostream>

class CheckMove
{
public:
	static bool diagonalCheck(int newCord[2], int currCord[2]);
	static bool lineCheck(int newCord[2], int currCord[2]);
	static bool stepCheck(int newCcord[2], int currCord[2]);
};