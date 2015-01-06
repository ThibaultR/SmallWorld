#include "Algo.h"
#include <time.h>

#define TILE_PLAIN 0
#define TILE_DESERT 1
#define TILE_MOUNTAIN 2
#define TILE_FOREST 3

Algo::Algo(){

}

Algo::~Algo(){

}

int Algo::computeFoo() { 
	return 1; 
}


int* Algo::mapCreation(int taille){
	srand((unsigned int)time(NULL));

	int* tabmap = new int[taille * taille];
	int n = taille * taille / 4;
	int counter[] = { n, n, n, n };

	int v;
	for (int i = 0; i < taille*taille; i++)
	{
		v = rand() % 4;
		while (counter[v] == 0){
			v = rand() % 4;
		}
		tabmap[i] = v;
		counter[v]--;
	}

	return (int*) tabmap;
}


int* Algo::findStartCoordinate(int taille){
	srand((unsigned int)time(NULL));

	// tabCoordinate[xPlayer1, yPlayer1, xPlayer2, yPlayer2]
	int * tabCoordinate = new int[4];
	int i;
	for (i = 0; i < 4; i++){
		tabCoordinate[i] = 0;
	}

	int v = rand() % 4;

	switch (v)
	{
	case 0:
		tabCoordinate[2] = taille - 1;
		tabCoordinate[3] = taille - 1;
		break;
	case 1:
		tabCoordinate[0] = taille - 1;
		tabCoordinate[3] = taille - 1;
		break;
	case 2:
		tabCoordinate[1] = taille - 1;
		tabCoordinate[2] = taille - 1;
		break;
	case 3:
		tabCoordinate[0] = taille - 1;
		tabCoordinate[1] = taille - 1;
		break;
	default:
		//TODO ERROR
		break;
	}

	return tabCoordinate;
}


bool * Algo::findPossibleMovement(int taille, bool isDwarf, int x, int y, int * tabMap){
	bool * reachableTiles = new bool[taille * taille];

	for (int j = 0; j < taille; j++)
	{
		for (int i = 0; i < taille; i++)
		{
			int tile = tabMap[j * taille + i];
			if (isDwarf && tile == TILE_MOUNTAIN){
				reachableTiles[j * taille + i] = true;
			}
			else if (j % 2 == 0){// ligne paire
				if (((i >= x - 1 && i <= x) && (j >= y - 1 && j <= y + 1)) || (i = x + 1 && j == y)){
					reachableTiles[j * taille + i] = true;
				}
			}
			else if (j % 2 == 1){// ligne impaire
				if (((i >= x && i <= x + 1) && (j >= y - 1 && j <= y + 1)) || (i = x - 1 && j == y)){
					reachableTiles[j * taille + i] = true;
				}
			}
			else {
				reachableTiles[j * taille + i] = false;
			}
		}
	}

	reachableTiles[y * taille + x] = false;
	return reachableTiles;
}