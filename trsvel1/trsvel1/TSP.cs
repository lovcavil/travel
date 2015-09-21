using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace travel
{
    partial class Program
    {
        public class TSP
        {
            double a = 0.99;
            double t0 = 97;
            double tf = 3;
            double t = 97;
            double Ml = 10000;
            int allnodecount = AllNodesButRoot.Count;
            int[] routeNew = new int[400];
            int[] routeCur = new int[400];
            int[] routeBest = new int[400];
            public static TravelPlan bestTP;
            public static TravelPlan newTP;
            public void Run(Node root)
            {
                string line;
                System.IO.StreamReader file = new System.IO.StreamReader(@"nodelist.csv");
                
                    for (int i =0; i < allnodecount; i++)
                {
                    line = file.ReadLine();
                    int order;
                    routeNew[i] = i+1;
                    //if (line != null)
                    //{
                    //    int.TryParse(line, out order);
                    //    routeNew[i] =order ;
                    //}
                    

                }
                file.Close();
                routeNew[0] = root.id;///roooooot swap 

                routeNew.CopyTo(routeCur, 0);
                routeNew.CopyTo(routeBest, 0);
                Random rand = new Random();
                long Ecurrent = 999999999999;
                long Ebest =999999999999;
                long Enew = 999999999999;
                while (t > tf)
                {


                    for (int r = 1; r <= Ml; r++)
                    {
                        if (rand.NextDouble() < 1)
                        {
                            int ix1 = (int)Math.Ceiling(rand.NextDouble() * (allnodecount-1));
                            int ix2 = (int)Math.Ceiling(rand.NextDouble() *( allnodecount-1));

                            int tmp = routeNew[ix1];
                            routeNew[ix1] = routeNew[ix2];
                            routeNew[ix2] = tmp;

                        }
                        else
                        {

                        }


                        Enew = 0;
                        Enew = totalTime(root);
                        if (Enew < Ecurrent)
                        {
                            Ecurrent = Enew;
                            routeNew.CopyTo(routeCur, 0);
                            if (Enew < Ebest)
                            {
                                Ebest = Enew;
                                routeNew.CopyTo(routeBest, 0);
                                Console.WriteLine(Ebest);
                                bestTP = newTP;
                                

                            }
                        }
                        else
                        {
                            if (rand.NextDouble() < Math.Exp(-((Enew - Ecurrent) / t)))
                            {
                                Ecurrent = Enew;
                                routeNew.CopyTo(routeCur, 0);
                            }
                            else
                            {
                                routeCur.CopyTo(routeNew, 0);
                            }
                        }

                    }
                    Console.WriteLine(t);
                    t = t * a;
            }
                Console.WriteLine(Ebest);
        }

            public long totalTime(Node root)
            {
                
                long E =0;
                TravelPlan tp = new TravelPlan(root);
                //System.IO.StreamWriter file = new System.IO.StreamWriter(@"test.txt");
                for (int i = 1; i < AllNodesButRoot.Count&& AllNodesButRoot.ElementAt(routeNew[i]-1) !=null; i++)
                {
                    var des = AllNodesButRoot.ElementAt(routeNew[i]-1);
                    if(AllNodesButRoot.ElementAt(routeNew[i] - 1) is City)
                    {
                        var c = (City)AllNodesButRoot.ElementAt(routeNew[i] - 1);
                        if (c.isDummy == true)
                        {
                            des = root;
                        }

                    }
                    tp.Travel(tp.currentState, des);
                    
                  //  file.WriteLine( tp.currentState.ToString());
                    if (tp.currentState.day >= 16)
                    {
                        E = E +9999999;
                    ///    return E;
                    }
                }               
                tp.Travel(tp.currentState, root);
                E +=tp.alldaypass;
                // tp.showpastActs();
                newTP = tp;
               
                //tp.showpastActs(file);
                //file.Close();
                return E;
            }
        }
    }
}
