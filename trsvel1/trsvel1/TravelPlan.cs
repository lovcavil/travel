using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace travel
{
     class TravelPlan
    {
        //static List<SingleTrip> possibleST = new List<SingleTrip>();
        //static void Travel()
        //{
        //    List<State> activeStates=new List<State>();

        //}
        
        public State currentState;
        public List<Act> pastActs=new List<Act>();
        public int alldaypass = 0;
        public Node root = new Node();
        public void showpastActs()
        {
            foreach(Act act in pastActs)
            {
                Console.WriteLine(act.ToString());
            }
        }
        public void showpastActs(System.IO.StreamWriter file)
        {
            foreach (Act act in pastActs)
            {
                file.WriteLine(act.ToString());
            }
        }


        public State Travel(State initialState, Node destination)
        {
///            State result = new State(initialState);

            State temp = new State(initialState);

            if(temp.Location == destination&&destination ==root)
            {
                pastActs.Add(new Act(root, root, 1, new TimeSpan()));
                temp.day = 1;
                currentState = temp;
                return temp;
                
            }

            while (temp.Location!= destination)
            {
                ///debug
                if (this.pastActs.Count > 0)
                {
                    if (this.pastActs.Last().daypass > 2)
                    {  temp.day = 16;
                    currentState.day = 16;
                    currentState = temp;
                    return temp;
                }
                }

                if (alldaypass > 500)
                {
                    temp.day = 16;
                    currentState.day = 16;
                    currentState = temp;
                    return temp;
                }

                temp.SetPossbileAct(root,destination);
                TimeSpan optm = TimeSpan.FromHours(999);
                Act optmA=new Act();
                List<City> cityNeardestination=new List<City>();
                if(destination is City)
                {
                    cityNeardestination.Add((City)destination);
                }
                if (destination is Resort)
                {
                    Resort r = (Resort)destination;
                    cityNeardestination = r.CityNear;

                }
                City optmCity = new City();
                if (temp.Location is Resort ||destination is City)
                {
                    foreach (City cityNear in cityNeardestination)
                    {
                        TimeSpan addition = TimeSpan.FromHours(0);
                        if (destination is Resort)
                        {
                            Resort r = (Resort)destination;
                            addition = r.CityNearTime.ElementAt(r.CityNear.IndexOf(cityNear));
                        }
                        foreach (Act act in temp.possbileAct)
                        {

                            TimeSpan newOptm = TimeSpan.FromHours(Length(temp.Run(act).Location, cityNear) / 90) + addition;
                            if (newOptm < optm)
                            {
                                optm = newOptm;
                                optmCity = cityNear;
                                optmA = act;

                            }


                        }

                    }
                }



                if (destination is Resort && temp.Run(temp.possbileAct.First()).Location == destination)
                {
                    optmA = temp.possbileAct.First();
                    //temp = Apply(temp, optmA);
                    //currentState = temp;
                    //return temp;
                }
                else
                {
                    //debug
                    if (destination.name == "拉萨大昭寺景区")
                    {
                        ;
                    }


                    if (destination is Resort  &&  temp.Location is City)
                    {
                        foreach (City cityNear in cityNeardestination)
                        {
                            TimeSpan addition = TimeSpan.FromHours(0);
                            if (destination is Resort)
                            {
                                Resort r = (Resort)destination;
                                addition = r.CityNearTime.ElementAt(r.CityNear.IndexOf(cityNear));
                            }
                            foreach (Act act in temp.possbileAct)
                            {

                                TimeSpan newOptm = TimeSpan.FromHours(Length(temp.Run(act).Location, cityNear) / 90) + addition;
                                if (newOptm < optm)
                                {
                                    optm = newOptm;
                                    optmCity = cityNear;
                                    optmA = act;
                                }

                            }

                        }
                    }
                }



                if (optmA.timecost >= TimeSpan.FromHours(8))
                {
                    optmA = new Act(optmA.from, optmA.to, optmA.daypass+1, optmA.timecost - TimeSpan.FromHours(8));
                }
                if (optmA.timecost >= TimeSpan.FromHours(8))
                {
                    optmA = new Act(optmA.from, optmA.to, optmA.daypass+1, optmA.timecost - TimeSpan.FromHours(8));
                }

                if (temp.Location is City && destination is Resort&& temp.Run(optmA).Location is City && temp.Run(temp.possbileAct.First()).Location != destination)
                {
                    var r = (Resort)destination;
                    int better = 0;
                    Node bestD = optmA.to;
                    double record;
                    foreach (City c in r.CityNear)
                    {
                        if ( Length(temp.Run(optmA).Location, c) >= Length(temp.Location, c))
                        {
                            better = 1;
                            record = Length(temp.Run(optmA).Location, c);

                        }
                    }
                    if (better==0) {
                        TimeSpan best=new TimeSpan();
                        
                        TimeSpan compare;
                        foreach (City c in r.CityNear)
                        {
                            compare=TimeSpan.FromHours(Length(temp.Run(optmA).Location, c)/90)+  r.CityNearTime.ElementAt(r.CityNear.IndexOf(c));
                            if (best == null)
                            {
                                best = compare;
                                bestD = c;
                            }

                            if (compare < best)
                            {
                                best = compare;
                                bestD = c;
                            }
                        }


                        optmA = new Act(optmA.from, bestD, optmA.daypass, TimeSpan.FromHours(Length(temp.Location, bestD) / 90) );
                    }
                }

                if (temp.Location is City && destination is City)
                {
                    if (Length(temp.Run(optmA).Location, destination) > Length(temp.Location, destination))
                    {
                        optmA = new Act(optmA.from, destination, 1, TimeSpan.FromHours(Length(temp.Location, destination)/90) - TimeSpan.FromHours(8));
                        if (optmA.timecost >= TimeSpan.FromHours(8))
                        {
                            optmA = new Act(optmA.from, destination, optmA.daypass + 1, optmA.timecost - TimeSpan.FromHours(8));
                        }
                        if (optmA.timecost >= TimeSpan.FromHours(8))
                        {
                            optmA = new Act(optmA.from, destination, optmA.daypass + 1, optmA.timecost - TimeSpan.FromHours(8));
                        }
                    }

                }


                if (destination is Resort && temp.Run(temp.possbileAct.First()).Location == destination)
                {
                    optmA = temp.possbileAct.First();
                    //temp = Apply(temp, optmA);
                    //currentState = temp;
                    //return temp;
                }

                if (optmA.timecost >= TimeSpan.FromHours(8))
                {
                    optmA = new Act(optmA.from, optmA.to, optmA.daypass + 1, optmA.timecost - TimeSpan.FromHours(8));
                }
                if (optmA.timecost >= TimeSpan.FromHours(8))
                {
                    optmA = new Act(optmA.from, optmA.to, optmA.daypass + 1, optmA.timecost - TimeSpan.FromHours(8));
                }

                temp = Apply(temp,optmA);
            }
            if(temp.Location is Resort)
            {
                Resort r = (Resort)destination;
                if (r.deftime == TimeSpan.FromHours(2))
                {
                    temp = Apply(temp, new Act(destination, destination, 0, TimeSpan.FromHours(1)));
                    temp = Apply(temp, new Act(destination, destination, 1, new TimeSpan()));
                    temp = Apply(temp, new Act(destination, destination, 0, TimeSpan.FromHours(1)));
                }
                else
                {
                    temp = Apply(temp, new Act(destination, destination, 0, r.deftime));
                }

            }
            
            currentState = temp;
            return temp;
        }

        public State Apply(State temp, Act act)
        {
            State result = new State(temp);
            result = result.Run(act);
            pastActs.Add(act);
            if (act.daypass >= 1)
            {
                alldaypass += act.daypass;
            }
            return result;
        }

        //public double Length(Node a,Node b,Act act)
        //{
        //    double result = 1;
        //    return result;
        //}

        static public double Length(Node a, Node b)
        {
            double result = Program.AdjectionArray[a.id, b.id];
            return result;
        }
        public TravelPlan(Node root)
        {
            this.root = root;
            currentState = new State(root);
        }

    }


    public class State
    {
        public Node Location;
        public int day;
        public TimeSpan timeLeft;
        public List<Act> possbileAct = new List<Act>();

        public State(Node root)
        {
            Location = root;
            day = 1;
            timeLeft = TimeSpan.FromHours(12);
        }

        public State(State cpy)
        {
            Location = cpy.Location;
            day = cpy.day;
            timeLeft = cpy.timeLeft;
        }

        public override string ToString()
        {
            return "at"+Location.name+",allready"+day+"day,"+"today"+timeLeft.ToString()+"left";
        }

        public State Run(Act act)
        {
            var result = new State(this);


            result.Location = act.to;
            result.timeLeft -= act.timecost;
            if (act.daypass >= 1)
            {
                result.day += act.daypass;
                result.timeLeft = TimeSpan.FromHours(12);
                
            }
            return result;
        }

        public void SetPossbileAct(Node root,Node destination)
        {
            possbileAct.Clear();
            Node des = destination;
            //debug
            if (destination.name== "西安市")
            {
                ;
            }


            if(des is City)
            {
                var c = (City)des;
                if(c.isDummy == true)
                {
                    des = root;
                   // if()
                }
            }
            
            if (this.Location is Resort)
            {
                Resort r = (Resort)this.Location;
                foreach (City city in r.CityNear)
                {
                    possbileAct.Add(new Act(this.Location, city, 0, r.CityNearTime.ElementAt(r.CityNear.IndexOf(city))));

                }
                return;
            }

            if (des is Resort)
            {
                Resort r = (Resort)des;
                if (r.CityNear.Contains(this.Location))
                {
                    TimeSpan timetrans = r.CityNearTime.ElementAt(r.CityNear.IndexOf((City)this.Location));
                    var timecompare = timetrans + timetrans + r.test;
                    if (this.timeLeft >= timecompare)
                    {
                        possbileAct.Add(new Act(this.Location, des, 0, timetrans));
                        return;
                    }


                    else
                    {
                        possbileAct.Add(new Act(this.Location, this.Location, 1, new TimeSpan()));
                        return;
                    }

                    //}
                    //else
                    //{

                }
            }

            foreach (City to in Program.cities)
            {
                if (to != null &&to.id>0&&to.id!=this.Location.id)
                {
                    TimeSpan timecost = TimeSpan.FromHours(TravelPlan.Length(this.Location, to) / 90);
                    if (this.timeLeft >= timecost)
                    {
                        bool a = (to.id == des.id);
                        if (a)///neng dao zhi jie dao
                        {
                            possbileAct.Clear();
                            possbileAct.Add(new Act(this.Location, to, 0, timecost));

                            return;
                        }
                        possbileAct.Add(new Act(this.Location, to, 1, timecost));
                        
                    }
                }

            }
            if (possbileAct.Count != 0)
            {
                return;
            }

            if (possbileAct.Count == 0&& destination is City)
            {
                TimeSpan timecost = TimeSpan.FromHours(TravelPlan.Length(this.Location, destination) / 90);
                var optmA = new Act(this.Location, destination, 1, timecost - TimeSpan.FromHours(8));
                if (optmA.timecost >= TimeSpan.FromHours(8))
                {
                    optmA = new Act(optmA.from, destination, optmA.daypass + 1, optmA.timecost - TimeSpan.FromHours(8));
                }
                if (optmA.timecost >= TimeSpan.FromHours(8))
                {
                    optmA = new Act(optmA.from, destination, optmA.daypass + 1, optmA.timecost - TimeSpan.FromHours(8));
                }
                possbileAct.Add(optmA);
                
            }

            if (possbileAct.Count == 0)
            {
                Resort r =(Resort) destination;
                foreach (City city in r.CityNear)
                {
                   TimeSpan timecost = TimeSpan.FromHours(TravelPlan.Length(this.Location, city) / 90);
                    //possbileAct.Add(new Act(this.Location, city,1, timecost - TimeSpan.FromHours(8)));
                    var optmA = new Act(this.Location, city, 1, timecost - TimeSpan.FromHours(8));

                    if (optmA.timecost >= TimeSpan.FromHours(8))
                    {
                        optmA = new Act(optmA.from, city, optmA.daypass + 1, optmA.timecost - TimeSpan.FromHours(8));
                    }
                    if (optmA.timecost >= TimeSpan.FromHours(8))
                    {
                        optmA = new Act(optmA.from, city, optmA.daypass + 1, optmA.timecost - TimeSpan.FromHours(8));
                    }
                    possbileAct.Add(optmA);
                }
            //    possbileAct.Add(new Act(this.Location, this.Location, 1,new TimeSpan()));
            }
        }
    }




    public class Act
    {
        public Node from;
        public Node to;
        public int daypass;
        public TimeSpan timecost;

        public Act() {; }
        public Act(Node from,Node to,int daypass, TimeSpan timecost)
        {
            this.from = from;
            this.to = to;
            this.daypass = daypass;
            this.timecost = timecost;
        }

        public override string ToString()
        {
            return "from"+ from.name+"to"+to.name+",cost"+timecost.ToString()+",stop"+daypass+"day";
        }
    }
    
    //class SingleTrip
    //{
    //    int day = 1;
    //    List<Act> pastActs;

    //}
}
