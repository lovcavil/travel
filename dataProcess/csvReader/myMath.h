#pragma once
class myMath
{
public:
	static int test(double* source);
	myMath();
	~myMath();
	static void areaAdd(double* source, double* result, int* prowSection, int* pcolumnSection, int lenRow, int lenColumn,int division);
	static int* sector(double* data,int* section, int len,int division);
};

