#pragma once
#include<fstream>
#include<iostream>
#include<string>
using namespace std;
class csv
{
public:
	csv();
	~csv();
	static void csv::read(string fileName, double* ptr, double* ptc, double* ptd,int* lenRow);


};

