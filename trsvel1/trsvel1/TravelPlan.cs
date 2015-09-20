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
        List<Act> pastActs=new List<Act>();
        public void showpastActs()
        {
            foreach(Act act in pastActs)
            {
                Console.WriteLine(act.ToString());
            }
        }

        public State Travel(State initialState, Node destination)
        {
///            State result = new State(initialState);

            State temp = new State(initialState);
            while (temp.Location!= destination)
            {
                temp.SetPossbileAct(destination);
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
                foreach (City cityNear in cityNeardestination)
                {
                    TimeSpan addition = TimeSpan.FromHours(0);
                    if(destination is Resort)
                    {
                        Resort r = (Resort)destination;
                        addition = r.CityNearTime.ElementAt(r.CityNear.LastIndexOf(cityNear));
                    }
                    foreach (Act act in temp.possbileAct)
                    {
                        TimeSpan newOptm = TimeSpan.FromHours(Length(temp.Run(act).Location, cityNear)/90)+addition;
                        if (newOptm< optm)
                        {
                            optm = newOptm;
                            optmA = act;
                        }
                     }
                }
                temp = Apply(temp,optmA);
            }
            if(destination is Resort)
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
            return result;
        }

        public double Length(Node a,Node b,Act act)
        {
            double result = 1;
            return result;
        }

        static public double Length(Node a, Node b)
        {
            double result = Program.AdjectionArray[a.id, b.id];
            return result;
        }
        public TravelPlan(Node root)
        {
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


        public State Run(Act act)
        {
            var result = new State(this);


            result.Location = act.to;
            result.timeLeft -= act.timecost;
            if (act.daypass == 1)
            {
                result.day += 1;
                result.timeLeft = TimeSpan.FromHours(12);
            }
            return result;
        }

        public void SetPossbileAct(Node destination)
        {
            possbileAct.Clear();
            if(this.Location is Resort)
            {
                Resort r = (Resort)this.Location;
                foreach (City city in r.CityNear)
                {
                    possbileAct.Add(new Act(this.Location, city, 0, r.CityNearTime.ElementAt(r.CityNear.LastIndexOf(city))));

                }
                return;
            }

            if (destination is Resort)
            {
                Resort r = (Resort)destination;
                if(r.CityNear.Contains(this.Location))
                {
                    TimeSpan timetrans = r.CityNearTime.ElementAt(r.CityNear.LastIndexOf((City)this.Location));
                    var timecompare = timetrans + timetrans + r.test;
                    if (this.timeLeft >= timecompare)
                    {
                        possbileAct.Add(new Act(this.Location, destination, 0, timetrans));
                    }
                    else
                    {
                        possbileAct.Add(new Act(this.Location, this.Location, 1, new TimeSpan()));
                    }
                    return;
                }
            }

            foreach (City to in Program.city)
            {
                if (to != null &&to.id>0&&to!=this.Location)
                {
                    TimeSpan timecost = TimeSpan.FromHours(TravelPlan.Length(this.Location, to) / 90);
                    if (this.timeLeft >= timecost)
                    {
                        possbileAct.Add(new Act(this.Location, to, 1, timecost));
                        bool a = (to.id == destination.id);
                        if (a)
                        {
                            possbileAct.Clear();
                            possbileAct.Add(new Act(this.Location, to, 0, timecost));

                            return;
                        }
                    }
                }

            }
            if (possbileAct.Count == 0)
            {
                possbileAct.Add(new Act(this.Location, this.Location, 1, new TimeSpan()));
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
