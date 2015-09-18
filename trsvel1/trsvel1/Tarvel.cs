using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace travel
{
    class Node
    {
        string name;
        TimeSpan deftime;

        Link[] links;

    }
    class Link
    {
        string name;
        float cost;
        TimeSpan deftime;
        Node[] nodes=new Node[2];
    }
    class City:Node
    {
           bool stayed;
    }
    class Resort : Node
    {
    }

    class Highway : Link
    {

    }
    class Road : Link
    {

    }
}
