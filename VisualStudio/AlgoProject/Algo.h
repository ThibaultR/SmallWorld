#pragma once
#include <cstdlib>

class Algo {
public:
	Algo();
	~Algo();
	int computeFoo();
	int * mapCreation(int taille);
	int * findStartCoordinate(int taille);
	bool * findPossibleMovement(int taille, bool isDwarfOnMountain, int x, int y, int * tabMap);
};



