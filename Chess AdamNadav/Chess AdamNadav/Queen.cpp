#include "Queen.h"

bool Queen::checkPath(int* newCord)
{
	bool destPieceWhite = false;
	char destChar = this->_gameBoard->CheckCoard(newCord);// gets the name of the piece in the new cord
	int tempcords[2] = { this->_cords[0] , this->_cords[1] };
	bool tempBool;

	if (destChar != '#')// checks if there is a piece in the place of the new cord
	{
		if (destChar <= 'Z')
		{
			destPieceWhite = true;
		}
	}


	if (destChar != '#' && destPieceWhite == this->_white)// check if the piece is of the same color
	{
		throw ChessEvents(INVALIED_DEST_PIECE_OWN, "In The Dest there is your own Piece");
		return false;
	}
	else if ((abs(*newCord - this->_cords[0]) != abs(*(newCord + 1) - this->_cords[1])) && (*newCord == this->_cords[0] && *(newCord + 1) == this->_cords[1]) && (*newCord != this->_cords[0] && *(newCord + 1) != this->_cords[1]))// check if legal move
	{
		throw ChessEvents(INVALIED_PIECE_MOVE, "The Queen Doing invalid move that he can't do!");
		return false;
	}
	else if (*(newCord) > 7 || *(newCord + 1) > 7 || *(newCord) < 0 || *(newCord + 1) < 0)// check if new cord is out the board 
	{
		throw ChessEvents(INVALIED_OUT_OF_INDEX, "The index that's insert is out of index");
		return false;
	}
	else if (*(newCord) == this->_cords[0] && *(newCord + 1) == this->_cords[1])// check if the new cord is the old cord
	{
		throw ChessEvents(INVALIED_SOURCE_EQL_DEST, "xD The source and the dest are the same place");
		return false;
	}
	else
	{
		// check cross move path
		if (abs(*(newCord) - this->_cords[0]) == abs(*(newCord + 1) -this->_cords[1]))
		{
			// is x is the positive
			tempBool = ((*(newCord) - this->_cords[0]) > 0);

			if ((*(newCord)-this->_cords[0]) - (*(newCord + 1) - this->_cords[1]))
			{
				for (int i = 1; i < abs(*(newCord)-this->_cords[0]); i++)
				{
					if (tempBool)
					{
						tempcords[0] = *(newCord) - i;
						tempcords[1] = *(newCord + 1) + i;
					}
					else
					{
						tempcords[0] = *(newCord) + i;
						tempcords[1] = *(newCord + 1) - i;
					}

					if (this->_gameBoard->CheckCoard(tempcords) != '#')
					{
						throw ChessEvents(INVALIED_PIECE_MOVE, "The Queen jumping over a piece");
						return false;
					}
				}
			}

			else
			{
				for (int i = 1; i < abs(*(newCord)-this->_cords[0]); i++)
				{
					tempcords[0] = std::min(this->_cords[0], *(newCord)) + i;
					tempcords[1] = std::min(this->_cords[1], *(newCord + 1)) + i;

					if (this->_gameBoard->CheckCoard(tempcords) != '#')
					{
						throw ChessEvents(INVALIED_PIECE_MOVE, "The Queen jumping over a piece");
						return false;
					}
				}
			}
		}

		// check linear move path
		else if ((*(newCord) != this->_cords[0] && *(newCord + 1) == this->_cords[1]) ||
			(*(newCord) == this->_cords[0] && *(newCord + 1) != this->_cords[1]))
		{
			tempcords[1] = this->_cords[1];

			for (int i = std::min(this->_cords[0], *(newCord)) + 1;
				i < std::max(this->_cords[0], *(newCord)); i++)
			{
				tempcords[0] = i;
				
				if (this->_gameBoard->CheckCoard(tempcords) != '#')
				{
					throw ChessEvents(INVALIED_PIECE_MOVE, "The Queen jumping over a piece");
					return false;
				}
			}

			tempcords[0] = this->_cords[0];

			for (int i = std::min(this->_cords[1], *(newCord + 1)) + 1;
				i < std::max(this->_cords[1], *(newCord + 1)); i++)
			{
				tempcords[1] = i;

				if (this->_gameBoard->CheckCoard(tempcords) != '#')
				{
					throw ChessEvents(INVALIED_PIECE_MOVE, "The Queen jumping over a piece");
					return false;
				}
			}
		}
		else
		{
			throw ChessEvents(INVALIED_PIECE_MOVE, "The Queen Doing invalid move that he can't do!");
			return false;
		}

		this->_gameEvent.setEvent(VALIED_MOVE, "valid move");
		return true;
	}
}

Queen::Queen(int cords[2], bool white, Board* gameBoard) : ChessPiece(cords, white, gameBoard)
{
}

void Queen::move(int* newCord)
{
	try
	{
		this->checkPath(newCord);
		this->_gameBoard->move(this->_cords, newCord);
		this->_cords[0] = (newCord[0]);
		this->_cords[1] = (newCord[1]);
	}
	catch (ChessEvents& e)
	{
		throw e;
	}
}
