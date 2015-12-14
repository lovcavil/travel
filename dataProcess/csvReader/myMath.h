#pragma once
class myMath
{
public:
	myMath();
	~myMath();
	static void areaAdd(double* source, double* result, int* rowSection, int* columnSection, int lenPerRow);
	static int test(double* source);
};

