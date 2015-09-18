using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace travel
{
    static class TravelPlan
    {
        static List<SingleTrip> possibleST = new List<SingleTrip>();
        static void Travel()
        {
            List<State> activeStates=new List<State>();

        }
    }
    class SingleTrip
    {
        int day = 1;
        List<Act> pastActs;

    }

    class State
    {
        Node Location;
        int day;
        TimeSpan timeLeft;
        List<Act> possbileAct = new List<Act>();

        State(Node root)
        {
            Location = root;
            day = 1;
            timeLeft = TimeSpan.FromHours(12);
        }

        State(State cpy)
        {
            Location = cpy.Location;
            day = cpy.day;
            timeLeft = cpy.timeLeft;
            List<Act> possbileAct = new List<Act>();
        }

        State Run(Act act)
        {
            var result=new State(this);
            while (GetType().IsInstanceOfType(act))
            {
                ;
            }
            return result;
        }

       void SetPossbileAct()
        {            
            ;
        }
    }



    class Move : Act
    {
        Node from;
        Node to;
    }
    class Act
    {

    }
}
