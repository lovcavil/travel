// csvReader.cpp : �������̨Ӧ�ó������ڵ㡣
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
	long lenRow;
	long lenColumn;
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
	long lenRow;
	long lenColumn;
	int rowSection[100];
	int columnSection[100];
	csv::read("in.csv", row, column, data, &lenRow, &lenColumn);
}

#define BASE 80000l
void read(string filename) {
	double* row, *column, *data, *mean, *range;
	row = new double[BASE];
	column = new double[BASE];
	data = new double[BASE];
	long lenRow;
	long lenColumn;
	mean = new double[BASE];
	range = new double[BASE];
	long count;
	csv::read(filename, row, column, data, &lenRow, &lenColumn);
	myMath::rf(data, lenColumn, mean, range, &count);
	ofstream fout;
	fout.open(filename+".csv", ios::out);
	for (long i = 1; i < count; i++) {
		fout << mean[i-1] << "," << range[i-1] << endl;
	}
	fout.close();
}
int main()
{	
	string readLine;
	
	fstream fin("csv.ini"); //���ļ�
	while (getline(fin, readLine)) //���ж�ȡ��ֱ������
	{
		read("source\\"+readLine);
	}




    return 0;
}

