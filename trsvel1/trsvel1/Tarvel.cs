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
        public TimeSpan deftime;

        //public Link[] links;

    }
    //public class Link
    //{
    //    string name;
    //    float cost;
    //    TimeSpan deftime;
    //    Node[] nodes=new Node[2];
    //}



    public class City:Node
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
    }
    public class Resort : Node
    {
        public List<City> CityNear=new List<City>();
        public List<double> CityNearTime= new List<double>();
    }

//    public class Highway : Link
//    { }

//public class Road : Link
//    {

//    }
}
