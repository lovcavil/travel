// csvReader.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include "csvReader.h"
#include "myMath.h"
int main()
{	
	double row[10];
	double column[10];
	double data[100];
	int len;
	csv::read("test.csv", row, column, data,&len);
	myMath::test(data);
    return 0;
}

