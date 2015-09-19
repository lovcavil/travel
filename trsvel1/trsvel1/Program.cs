using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace travel
{
    class Program
    {
        public static string[] Name = new string[300];
        public static City[] city = new City[100];
        public static double[,] AdjectionArray= new double[300,300];
        public static Resort[] resort = new Resort[300];

        static int Idcount = 0;
        static void Main(string[] args)
        {
            ReadCity();
            BuildCity();
            ReadBuildResort();

            var tp = new TravelPlan(city[1]);
            tp.Travel(tp.currentState, resort[20]);
        }

        static public void ReadCity()
        {
            int counter=0;
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(@"cc.csv");
            while ((line = file.ReadLine()) != null)
            {
                System.Console.WriteLine(line);
                string[] array;
                string[] separator= { "," };

                if (counter > 0)
                {
                    
                    array = line.Split(separator, StringSplitOptions.None);
                    Name[counter] = array[0];
                    int datacount = 0;
                    foreach (string data in array)
                    {
                        

                        int i = 0;
                        if (int.TryParse(data, out i))
                        {
                            AdjectionArray[counter, datacount] = i;
                            AdjectionArray[datacount,counter ] = i;
                        }
                        datacount += 1;
                    }
                    
                    
                }
                counter++;
            }

            file.Close();
            System.Console.ReadLine();
        }
        static public void BuildCity()
        {
            int i = 0;
            foreach(string cityName in Name)
            {
                
                if (cityName != null)
                {
                    city[i] = new City(i,Name[i],false);
                    i = i + 1;
                }
                
            }
            Idcount = i;
        }

        static public void ReadBuildResort()
        {
            int counter = 1;
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(@"rc.csv");
            while ((line = file.ReadLine()) != null)
            {
                System.Console.WriteLine(line);
                string[] array;
                string[] separator = { "," };
                if (counter > 0)
                {
                    int uid = Idcount + counter;
                    array = line.Split(separator, StringSplitOptions.None);
                    Name[uid] = array[0];
                    resort[counter] = new Resort();
                    resort[counter].id=uid;
                    resort[counter].name = array[0];
                    if (array[2] != "")
                    {
                        resort[counter].CityNear.Add(city[Array.LastIndexOf(Name, array[2])]);
                        double i = 0;
                        double.TryParse(array[3], out i);
                        resort[counter].CityNearTime.Add(i);
                    }
                    if (array[4] != "")
                    {
                        resort[counter].CityNear.Add(city[Array.LastIndexOf(Name, array[4])]);
                        double i = 0;
                        if (double.TryParse(array[5], out i))
                        {
                            resort[counter].CityNearTime.Add(i);
                        }
                    }
                    if (array[6] != "")
                    {
                        resort[counter].CityNear.Add(city[Array.LastIndexOf(Name, array[6])]);
                        double i = 0;
                        if (double.TryParse(array[7], out i))
                        {
                            resort[counter].CityNearTime.Add(i);
                        }
                    }


                }
                counter++;
            }
            file.Close();
        }
    }



}
