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


        public State Travel(State initialState, Node destination)
        {
///            State result = new State(initialState);

            State temp = new State(initialState);
            while (temp.Location!= destination)
            {
                temp.SetPossbileAct();
                double optm = 999;
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
                    double addition = 0;
                    if(destination is Resort)
                    {
                        Resort r = (Resort)destination;
                        addition = r.CityNearTime.ElementAt(r.CityNear.LastIndexOf(cityNear));
                    }
                    foreach (Act act in temp.possbileAct)
                    {
                        double newOptm = Length(temp.Run(act).Location, cityNear)+addition;
                        if (newOptm< optm)
                        {
                            optm = newOptm;
                            optmA = act;
                        }
                     }
                }
                pastActs.Add(optmA);
                temp = temp.Run(optmA);
            }
            return temp;
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

            if (act.daypass == 1)
            {
                result.day += 1;
                result.timeLeft = TimeSpan.FromHours(12);
            }
            result.Location = act.to;
            result.timeLeft -= act.timecost;
            return result;
        }

        public void SetPossbileAct()
        {
            possbileAct.Clear();


            foreach (City to in Program.city)
            {
                if (to != null &&to.id>0&&to!=this.Location)
                {
                    TimeSpan timecost = TimeSpan.FromHours(TravelPlan.Length(this.Location, to) / 90);
                    if (this.timeLeft >= timecost)
                    {
                        possbileAct.Add(new Act(this.Location, to, 0, timecost));
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
    }
    
    //class SingleTrip
    //{
    //    int day = 1;
    //    List<Act> pastActs;

    //}
}
