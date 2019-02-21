using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTT
{
    class Game
    {

        ArrayList battlefield = new ArrayList();

        public void playGame()
        {
            bool winner = false;
            while(winner != true)
            {

            }
        }

        public void placeShips(int x1, int x2, int y1, int y2)
        {
            Ship ship = new Ship(x1, x2, y1, y2);
            battlefield.Add(ship);

        }
        public bool testHit(int x, int y)
        {
            bool hit = false;
            Ship[] temp = (Ship[])battlefield.ToArray();
            for(int i = 0; i < temp.Length; i++)
            {
                if (temp[i].testHit(x, y)) {
                    hit = true;
                    temp[i].testSunk();
                }
            }
            return hit;
        }
        

    }
}
