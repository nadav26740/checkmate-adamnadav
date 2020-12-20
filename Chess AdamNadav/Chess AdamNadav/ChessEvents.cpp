#include "ChessEvents.h"

ChessEvents::ChessEvents()
{
}

ChessEvents::ChessEvents(int eventNum, std::string explain)
{
	this->_details.eventType = eventNum;
	this->_details.explain = explain;
}

ChessEvents::~ChessEvents()
{
}

eventDetails ChessEvents::getDetails()
{
	return this->_details;
}

void ChessEvents::setEvent(int eventNum, std::string explain)
{
	this->_details.eventType = eventNum;
	this->_details.explain = explain;
}
