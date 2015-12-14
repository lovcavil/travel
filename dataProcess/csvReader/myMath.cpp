#include "stdafx.h"
#include "myMath.h"
#include <amp.h>
using namespace concurrency;

myMath::myMath()
{
}


myMath::~myMath()
{
}


void myMath::areaAdd(double* source, double* result, int* rowSection, int* columnSection, int lenPerRow)
{
	int aMatrix[] = { 1, 4, 2, 5, 3, 6 };
	int bMatrix[] = { 7, 8, 9, 10, 11, 12 };
	int productMatrix[] = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
	array_view<double, 1> source1(3, source);
	array_view<int, 2> a(3, 2, aMatrix);
	array_view<int, 2> b(2, 3, bMatrix);
	array_view<int, 2> product(3, 3, productMatrix);
}


int myMath::test(double* source)
{
	array_view<double, 1> source1(5, source);
	return 0;
}
