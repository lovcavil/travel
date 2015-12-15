// csvReader.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include "csvReader.h"
#include "myMath.h"
int main()
{	
	double row[100];
	double column[100];
	double data[10000];
	const int division = 16;
	int lenRow;
	int lenColumn;
	int rowSection[100];
	int columnSection[100];
	csv::read("in.csv", row, column, data,&lenRow,&lenColumn);
	myMath::sector(row, rowSection, lenRow, division);
	myMath::sector(column, columnSection, lenColumn, division);
	myMath::test(data);
    return 0;
}

