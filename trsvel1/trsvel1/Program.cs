using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace travel
{
    partial class Program
    {
        public static string[] Name = new string[300];
        public static City[] cities = new City[200];
        public static double[,] AdjectionArray= new double[200,200];
        public static Resort[] resorts = new Resort[300];

        static int Idcount = 0;
        public static List<Node> AllNodesButRoot = new List<Node>();

        static void Main(string[] args)
        {
            ReadCity();
            BuildCity();
            ReadBuildResort();
            Node root = cities[1];
            MakeNodeList( root);
            Console.WriteLine(AllNodesButRoot.Count);
            //var tp = new TravelPlan(cities[1]);
            //tp.Travel(tp.currentState, AllNodesButRoot.ElementAt(0));
            //tp.Travel(tp.currentState, AllNodesButRoot.ElementAt(29));
            //tp.showpastActs();

            TSP tsp = new TSP();
            tsp.Run(root);


            ;
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
            int i = 1;
            foreach(string cityName in Name)
            {
                
                if (cityName != null)
                {
                    cities[i] = new City(i,Name[i],false);
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
                    int uid = Idcount + counter-1;
                    array = line.Split(separator, StringSplitOptions.None);
                    Name[uid] = array[0];
                    resorts[counter] = new Resort();
                    resorts[counter].id=uid;
                    resorts[counter].name = array[0];
                    double t=0;
                    if (array[1] == "半天")
                    {
                        t = 0.5;
                        resorts[counter].deftime = TimeSpan.FromHours(t * 8);

                    }
                    if (array[1] == "一天")
                    {
                        t = 1;
                        resorts[counter].deftime = TimeSpan.FromHours(t * 8);

                    }
                    resorts[counter].test = resorts[counter].deftime;
                    if (array[1] == "两天")
                    {
                        t = 2;
                        resorts[counter].deftime = TimeSpan.FromHours(t * 8);
                        t = 1;
                        resorts[counter].test = TimeSpan.FromHours(t * 8);
                    }
                    
                    if (array[2] != "")
                    {
                        resorts[counter].CityNear.Add(cities[Array.LastIndexOf(Name, array[2])]);
                        double i = 0;
                        double.TryParse(array[3], out i);
                        resorts[counter].CityNearTime.Add(TimeSpan.FromHours(i));
                    }
                    if (array[4] != "")
                    {
                        resorts[counter].CityNear.Add(cities[Array.LastIndexOf(Name, array[4])]);
                        double i = 0;
                        if (double.TryParse(array[5], out i))
                        {
                            resorts[counter].CityNearTime.Add(TimeSpan.FromHours(i ));
                        }
                    }
                    if (array[6] != "")
                    {
                        resorts[counter].CityNear.Add(cities[Array.LastIndexOf(Name, array[6])]);
                        double i = 0;
                        if (double.TryParse(array[7], out i))
                        {
                            resorts[counter].CityNearTime.Add(TimeSpan.FromHours(i));
                        }
                    }


                }
                counter++;
            }
            file.Close();
        }
        public static void MakeNodeList(Node root)
        {
            AllNodesButRoot.Clear();
            foreach (City c in cities)
            {
                if (c != null && c.id != 0 && c.id != root.id)
                {
                    AllNodesButRoot.Add(c);
                }
            }
            foreach (Resort r in resorts)
            {
                if (r != null)
                {
                    AllNodesButRoot.Add(r);
                }
            }
            for(int i = 1; i <= 1; i++)
            {
                City dummy = new City();
                //          dummy.id = AllNodesButRoot.Count + 1+i;
                dummy.id = root.id  ;
                dummy.name = root.name;
                dummy.isDummy = true;
                AllNodesButRoot.Add(dummy);

            }
            return;
        }

    }



}
