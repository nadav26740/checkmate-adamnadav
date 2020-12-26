/*
This file servers as an example of how to use Pipe.h file.
It is recommended to use the following code in your project, 
in order to read and write information from and to the Backend
*/

#include "Board.h"
#include "GameFunctions.h"
#include "ChessPiece.h"
#include "ChessEvents.h"
#include "Pipe.h"
#include <iostream>
#include <thread>
#include <string>

using std::cout;
using std::endl;
using std::string;


void main()
{
	srand(time_t(NULL));
	bool whitePlaying = false;
	
	Pipe p;
	bool isConnect = p.connect();
	
	string ans;
	while (!isConnect)
	{
		cout << "cant connect to graphics" << endl;
		cout << "Do you try to connect again or exit? (0-try again, 1-exit)" << endl;
		std::cin >> ans;

		if (ans == "0")
		{
			cout << "trying connect again.." << endl;
			Sleep(5000);
			isConnect = p.connect();
		}
		else 
		{
			p.close();
			return;
		}
	}
	

	char msgToGraphics[1024];
	std::string tempString;
	ChessPiece* tempPiece;
	int newCords[2];
	int oldCords[2];

	// msgToGraphics should contain the board string accord the protocol
	Board gameBoard;
	tempString = gameBoard.getBoard() + '1';
	strcpy_s(msgToGraphics, tempString.c_str());
	
	p.sendMessageToGraphics(msgToGraphics);   // send the board string

	// get message from graphics
	string msgFromGraphics = p.getMessageFromGraphics();

	while (msgFromGraphics != "quit")
	{
		// should handle the string the sent from graphics
		// according the protocol. Ex: e2e4           (move e2 to e4)
		try
		{
			GameFunctions::stringToCords(oldCords, newCords, msgFromGraphics);
			tempPiece = GameFunctions::createPieceChar(gameBoard.CheckCoard(oldCords), oldCords, &gameBoard);

			if (tempPiece == nullptr) // check if there is piece
			{
				throw ChessEvents(INVALIED_PIECE_NOT_ALLOWED, "There is no piece in the location");
			}
			else if (tempPiece->getWhite() != whitePlaying) // check the piece belone to the currect player
			{
				throw ChessEvents(INVALIED_PIECE_NOT_ALLOWED, "You Doesn't own the piece");
			}
			
			tempPiece->move(newCords);
			msgToGraphics[0] = (char)(tempPiece->getDetails().eventType + '0');
			cout << tempPiece->getDetails().explain << endl;
		}
		catch (ChessEvents& e)
		{
			msgToGraphics[0] = (char)(e.getDetails().eventType + '0');
			cout << e.getDetails().explain << endl;
		}
		msgToGraphics[1] = 0;
		cout << "Start:" << oldCords[0] << " " << oldCords[1] << endl << newCords[0] << " " << newCords[1] << endl;
		cout << gameBoard.getBoardString() << endl;

		/******* JUST FOR EREZ DEBUGGING ******/
		// int r = rand() % 10; // just for debugging......
		// msgToGraphics[0] = (char)(1 + '0');
		// 
		/******* JUST FOR EREZ DEBUGGING ******/


		// return result to graphics		
		p.sendMessageToGraphics(msgToGraphics);   

		// get message from graphics
		msgFromGraphics = p.getMessageFromGraphics();
		GameFunctions::switchPlayer(whitePlaying);
	}

	p.close();
}