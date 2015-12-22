#include "stdafx.h"
#include "csvReader.h"
#include <fstream>
#include <iostream>
#include <string>
using namespace std;

csv::csv()
{
}

csv::~csv()
{
}

void csv::read(string fileName, double* ptr, double* ptc, double* ptd,long* lenRow,long* lenColumn)
{
	const char* split = ",";	
	string readLine;
	long ctrRow=0;
	long ctrColumn = 0;
	fstream fin(fileName); //打开文件
	while (getline(fin, readLine)) //逐行读取，直到结束
	{
		char *c_readLine;
		long len = readLine.length();
		c_readLine = (char *)malloc((len + 1)*sizeof(char));
		strcpy_s(c_readLine, len+1, readLine.c_str());
		char *next_token=NULL;
		char* c_split = strtok_s(c_readLine, split, &next_token);
		double rTitle;
		double cTitle;		
		while (c_split != NULL) {
			printf("%s\n", c_split);
			if (ctrRow == 0) {
				if (ctrColumn != 0){
					*(ptc+ ctrColumn-1) = atof(c_split);
					*lenRow = ctrColumn;
				}	
			}
			else
			{
				if (ctrColumn == 0){
					*(ptr + ctrRow - 1) = atof(c_split);
				}
				else
				{
					*(ptd + (ctrRow - 1)*(*lenRow) + ctrColumn - 1) = atof(c_split);
				}
			}
			c_split = strtok_s(NULL, split, &next_token);
			ctrColumn++;
		}
		ctrColumn = 0;
		ctrRow++;
		
	}
	*lenColumn = ctrRow-1;
	fin.close();
}
