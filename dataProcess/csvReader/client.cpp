// csvReader.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include "csvReader.h"
#include "myMath.h"
#include "client.h"
void test() {
	double row[100];
	double column[100];
	double data[10000];
	const int division = 16;
	int lenRow;
	int lenColumn;
	int rowSection[100];
	int columnSection[100];
	csv::read("in.csv", row, column, data, &lenRow, &lenColumn);
	//myMath::sector(row, rowSection, lenRow, division);
	//myMath::sector(column, columnSection, lenColumn, division);
	//myMath::test(data);
}
void test2() {
	double row[100];
	double column[100];
	double data[10000];
	int lenRow;
	int lenColumn;
	int rowSection[100];
	int columnSection[100];
	csv::read("in.csv", row, column, data, &lenRow, &lenColumn);
}
int main()
{	
	//void test();
	double row[20000];
	double column[100];
	double data[20000];
	int lenRow;
	int lenColumn;
	double mean[20000];
	double range[20000];
	int count;
	csv::read("aaa.csv", row, column, data, &lenRow, &lenColumn);
	myMath::rf(data, lenColumn, mean, range, &count);
	ofstream fout;
	fout.open("file.csv", ios::out);
	for (int i = 1; i <= count; i++) {
		fout << mean[i] << "," << range[i] << endl;
	}
	fout.close();
    return 0;
}

