#include "checkMove.h"

bool CheckMove::diagonalCheck(int newCord[2], int currCord[2])
{
	return (abs(*newCord - currCord[0]) != abs(*(newCord + 1) - currCord[1]));
}

bool CheckMove::lineCheck(int newCord[2], int currCord[2])
{
	return (newCord[0] != currCord[0] && newCord[1] != currCord[1]);
}

bool CheckMove::stepCheck(int newCord[2], int currCord[2])
{
	return (abs(*newCord - currCord[0]) > 1 || abs(*(newCord + 1) - currCord[1]) > 1);
}
