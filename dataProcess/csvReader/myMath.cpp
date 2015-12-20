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


void myMath::rf(double* source, int len, double* mean, double* range, int* rfcount)
{
	int count = 1;
	int current = 1;
	int target;
	int direction = (*(source + 1) - *source) ==0? 1 : ((*(source + 1) - *source) / abs(*(source + 1) - *source));
	target = pvSearch(source,1,len,direction);
	while (current < len) {
		double crange = abs(*(source + target - 1) - *(source + current - 1));
		double cmean = (*(source + target - 1) + *(source + current - 1)) / 2;
		*(mean + count - 1) = cmean;
		*(range + count - 1) = crange;
		count++;
		direction *= -1;
		current = target;
		target= pvSearch(source, target, len, direction);
	}
	*rfcount = count;
}


int myMath::pvSearch(double* source,int start, int end ,int direction)
{
	
	for (int c = start; c < end; c++) {
		bool t = (*(source + c) - *(source + c-1 ))*direction < 0;//fanxiang
		if (t) {
			return(c);
		}
	}
	return end;
}
