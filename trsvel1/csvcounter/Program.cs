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
        public static double[] sum1 = new double[8];
        static void Main(string[] args)
        {
            ReadCsv();
            AreaAdd2();
            Save2();
            AreaAdd();
            Save();
        }

        public static void ReadCsv()
        {
            int counter = 0;
            double cumTitle = 0;
            double rowTitle = 0;
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(@"in.csv");

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
                            if (counter == 8&&localj==8)
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

        public static void AreaAdd2()
        {
            double rowSec;
            double cumSec;
            rowSec = ((row.Max() - row.Min()) / 16);
            cumSec =((cum.Max() - cum.Min()) / 16);
            for(int i = 1; i < row.Length; i++)
            {
                if (row[i] != 0)
                {
                    rowGroup[i] =(int) Math.Floor((row.Max()-row[i]) / rowSec);
                }
            } 
            for (int i = 1; i < cum.Length; i++)
            {
                if (cum[i] != 0)
                {
                    cumGroup[i] = (int)Math.Floor((cum[i]-cum.Min()) / cumSec);
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
        public static void AreaAdd()
        {
            double cumSec;
            
            cumSec = ((cum.Max() - cum.Min()) / 8);
            for (int i = 1; i < row.Length; i++)
            {
                if (row[i] != 0)
                {
                    rowGroup[i] = 1;
                }
            }
            for (int i = 1; i < cum.Length; i++)
            {
                if (cum[i] != 0)
                {
                    cumGroup[i] = (int)Math.Floor((cum[i] - cum.Min()) / cumSec);
                }


            }
            for (int j = 1; j < cum.Length; j++)
            {
                for (int i = 1; i < row.Length; i++)
                {
                    if (data[i, j] != 0)
                    {
                        sum1[ cumGroup[j]] += data[i, j];
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

        public static void Save2()
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter("Sum.csv");
            double sumr = 0;
            for (int i = 1; i < SQRT; i++)
            {
                sumr += row[i];
                if (i % 4 == 0)
                {
                    file.Write(sumr / 4 + ",");
                    sumr = 0;
                }
            }
            file.WriteLine();
            sumr = 0;
            for (int i = 1; i < SQRT; i++)
            {
                sumr += cum[i];
                if (i % 4 == 0)
                {
                    file.Write(sumr/4 + ",");
                    sumr = 0;
                }
            }
            file.WriteLine();
            for (int i = 0; i < 17; i++)
            {
                for (int j = 0; j < 17; j++)
                {
                    file.Write(sum[i, j] + ",");
                }
                file.WriteLine();
            }
            file.Close();
        }
        public static void Save()
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter("Sum1.csv");
            double sumr = 0;
            
            for (int i = 1; i < SQRT; i++)
            {
                sumr += cum[i];
                if (i % 8 == 0)
                {
                    file.Write(sumr / 8 + ",");
                    sumr = 0;
                }
            }
            file.WriteLine();
            for (int i = 0; i < 8; i++)
            {

                    file.Write(sum1[i] + ",");
                file.WriteLine();
            }
            file.Close();
        }
    }


}
