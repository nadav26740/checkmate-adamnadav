#pragma once
#include <iostream>

enum { // events Defines
	VALIED_MOVE = 0,
	VALIED_CHESS, // 1
	INVALIED_PIECE_NOT_ALLOWED, // 2
	INVALIED_DEST_PIECE_OWN, // 3
	INVALIED_CHESS_WILL_BECOME, // 4
	INVALIED_OUT_OF_INDEX, // 5
	INVALIED_PIECE_MOVE, // 6
	INVALIED_SOURCE_EQL_DEST, // 7
	VALIED_CHESS_MATE // 8
};


typedef struct eventDetails
{
	int eventType;
	std::string explain;
} eventDetails;


class ChessEvents : public std::exception
{
private:
	eventDetails _details;

public:
	ChessEvents();
	ChessEvents(int eventNum, std::string explain);
	~ChessEvents();
	struct eventDetails getDetails();
	void setEvent(int eventNum, std::string explain);
};

