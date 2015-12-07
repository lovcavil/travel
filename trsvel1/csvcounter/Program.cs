using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csvcounter
{
    class Program
    {
        public const int SQRT=70;
        public static double[] row = new double[SQRT];
        public static int[] rowGroup = new int[SQRT];
        public static double[] cum = new double[SQRT];
        public static int[] cumGroup = new int[SQRT];
        public static int[,] data = new int[SQRT, SQRT];
        public static double[,] sum = new double[18, 18];
        static void Main(string[] args)
        {
            ReadCsv();
            int a = data.GetLowerBound(0);
            int b = data.GetUpperBound(0);
            System.Console.WriteLine("{0}-{1}",a,b);
            AreaAdd();
            System.IO.StreamWriter file = new System.IO.StreamWriter("Sum.csv");
            for (int j = 1; j < 17; j++)
            {
                for (int i = 1; i < 17; i++)
                {                  
                        file.Write(sum[i, j]+",");                  
                }
                file.WriteLine();
            }
            file.Close();
            
        }

        public static void ReadCsv()
        {
            int counter = 0;
            double cumTitle = 0;
            double rowTitle = 0;
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(@"test1.csv");

            while ((line = file.ReadLine()) != null)
            {
                System.Console.WriteLine(line);
                string[] array;
                string[] separator = { "," };
                array = line.Split(separator, StringSplitOptions.None);
                if (counter == 0)
                {
                    int locali = 0;
                    foreach(string word in array)
                    {
                        cumTitle = 0;
                        double.TryParse(word, out cumTitle);
                        cum[locali] = cumTitle;
                        locali++;
                    }
                }
                else
                {
                    
                    int localj = 0;
                    foreach (string word in array)
                    {
                        if (localj == 0)
                        {
                            rowTitle = 0;
                            double.TryParse(word, out rowTitle);
                            row[counter] = rowTitle;
                           
                        }
                        else
                        {
                            int d=0;
                            int.TryParse(word, out d);
                            data[counter, localj] = d;
                            if (d != 0)
                            {
                                ;
                            }
                        }
                        localj++;
                        
                    }

                }

                counter++;
                ;
            }
        }

        public static void AreaAdd()
        {
            double rowSec;
            double cumSec;
            rowSec = ((row.Max() - row.Min()) / 15);
            cumSec =((cum.Max() - cum.Min()) / 15);
            for(int i = 1; i < row.Length; i++)
            {
                if (row[i] != 0)
                {
                    rowGroup[i] =7+(int) Math.Ceiling(row[i] / rowSec);
                }
            } 
            for (int i = 1; i < cum.Length; i++)
            {
                if (cum[i] != 0)
                {
                    cumGroup[i] = (int)Math.Ceiling(cum[i] / cumSec);
                }
            }
            for (int j = 1; j < cum.Length; j++)
            {
                for (int i = 1; i < row.Length; i++)
                {
                    if (data[i, j] != 0)
                    {
                        sum[rowGroup[i], cumGroup[j]] += data[i, j];
                    }
                }
            }
        }

        public static void Show()
        {
            foreach(int i in data)
            {
                System.Console.Write(i+",");
                
            }

            foreach (double i in row)
            {
                System.Console.Write(i + ",");
                System.Console.WriteLine();
            }

        }
    }


}
