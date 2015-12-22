#pragma once
class myMath
{
public:
	static int test(double* source);
	myMath();
	~myMath();
	static void areaAdd(double* source, double* result, int* prowSection, int* pcolumnSection, int lenRow, int lenColumn,int division);
	static int* sector(double* data,int* section, int len,int division);
	static void rf(double* source, long len, double* mean, double* range, long* rfcount);
	static int pvSearch(double* source,long start,long end, int direction);
};

