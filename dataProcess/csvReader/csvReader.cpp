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

void csv::read(string fileName, double* ptr, double* ptc, double* ptd,int* lenPerRow)
{
	const char* split = ",";	
	string readLine;
	int ctrRow=0;
	int ctrColumn = 0;int lenColumn;
	fstream fin(fileName); //打开文件
	while (getline(fin, readLine)) //逐行读取，直到结束
	{
		char *c_readLine;
		int len = readLine.length();
		c_readLine = (char *)malloc((len + 1)*sizeof(char));
		//readLine._Copy_s(c_readLine, len, 0);
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
					lenColumn = ctrColumn;
					*lenPerRow = lenColumn;
				}	
			}
			else
			{
				if (ctrColumn == 0){
					*(ptr + ctrRow - 1) = atof(c_split);
				}
				else
				{
					//double a=atof(c_split);
					//int ab = (ctrRow - 1)*lenColumn + ctrColumn - 1;
					*(ptd + (ctrRow - 1)*lenColumn + ctrColumn - 1) = atof(c_split);
				}
			}
			c_split = strtok_s(NULL, split, &next_token);
			ctrColumn++;
		}
		ctrColumn = 0;
		ctrRow++;
	}
	fin.close();
}
