using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace travel
{
    public class Node
    {
        public string name;
        public int id;

        //public Link[] links;

    }
    //public class Link
    //{
    //    string name;
    //    float cost;
    //    TimeSpan deftime;
    //    Node[] nodes=new Node[2];
    //}



    public class City: Node,IEquatable<City>
    {
        bool isLarge;
        bool isStayed;
        public City()
        {
        }
        public City(int i,string name,bool isLarge)
        {
            id = i;
            this.name = name;
            this.isLarge = isLarge;
            isStayed = false;
        }

        public bool Equals(City other)
        {
            return (this.id == other.id);
        
        }
    }
    public class Resort : Node
    {
        public List<City> CityNear=new List<City>();
        public List<TimeSpan> CityNearTime= new List<TimeSpan>();
        public TimeSpan deftime;
        public TimeSpan test;
    }

    //    public class Highway : Link
    //    { }

    //public class Road : Link
    //    {

    //    }
}
