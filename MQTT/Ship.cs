using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTT
{
    class Ship
    {
        int x1;
        int x2;
        int y1;
        int y2;
        int length;
        ArrayList hits = new ArrayList();
        bool sunk =  false;


        public enum ShipType
        {
            Carrier,
            Battleship,
            Cruiser,
            Destroyer,
            Sub,

        }

        public Ship(int xPoint1, int xPoint2, int yPoint1, int yPoint2)
        {
            x1 = xPoint1;
            x2 = xPoint2;

            y1 = yPoint1;
            y2 = yPoint2;
            if(x1 == x2){
                length = y2 - y1 +1;
            }
            else
            {
               length = x2 - x1 +1;
            }
            
        }

        public bool testHit(int x,int y)
        {
            bool hit = false;
            if (x >= x1 && x <= x2 && y >= y1 && y <= y2)
            {
                hits.Add(Tuple.Create(x, y));
                hit = true;
            }
            return hit;
        }

        public bool testSunk()
        {
            if (!sunk)
            {
                int hitlength = hits.ToArray().Length;
                if (hitlength >= length)
                {
                    sunk = true;
                }
            }

            return sunk;
        }
    }
}
