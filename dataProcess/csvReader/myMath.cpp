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


void myMath::areaAdd(double* source, double* result, int* prowSection, int* pcolumnSection, int lenRow,int lenColumn,int division)
{
	int aMatrix[] = { 1, 4, 2, 5, 3, 6 };
	int bMatrix[] = { 7, 8, 9, 10, 11, 12 };
	int productMatrix[] = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
	array_view<double, 1> source1(3, source);
	array_view<int, 2> a(3, 2, aMatrix);
	array_view<int, 2> b(2, 3, bMatrix);
	array_view<int, 2> product(3, 3, productMatrix);

	array_view<int, 1> rowSection(lenRow, prowSection);
	array_view<int, 1> columnSection(lenColumn, pcolumnSection);
	array_view<double, 2> data(lenRow, lenColumn, source);
	array_view<double, 2> result_view(division, division, result);

}


int myMath::test(double* source)
{
	array_view<double, 1> source1(5, source);
	return 0;
}


int* myMath::sector(double* data, int* section,int len,int division)
{
	double v = (*(data + len - 1) - *data) / division;
	for (int i = 0; i < len; i++) {
		*(section + i) = int((*(data+i)-*data) / v);
	}
	*(section + len - 1) = division - 1;
	return section;
}
