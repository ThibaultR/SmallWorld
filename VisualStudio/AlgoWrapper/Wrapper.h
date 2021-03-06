#ifndef __WRAPPER__
#define __WRAPPER__

#include "..\AlgoProject\Algo.h" //TODO WTF

using namespace System;

namespace Wrapper {
	public ref class WrapperAlgo {
	private:
		Algo* algo;

	public:
		WrapperAlgo() {
			algo = new Algo();
		}

		~WrapperAlgo() {
			delete algo;
		}

		int computeFoo() { return algo->computeFoo(); }

		int* mapCreation(int taille){ return algo->mapCreation(taille); }
		int* findStartCoordinate(int taille){ return algo->findStartCoordinate(taille); }
		bool * findPossibleMovement(int taille, bool isDwarf, int x, int y, int * tabMap){ return algo->findPossibleMovement(taille, isDwarf, x, y, tabMap); }
	};
}
#endif