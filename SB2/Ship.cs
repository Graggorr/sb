using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace SB2
{
    public class Ship
    {
        public Coordinates[] Coordinates { get; set; }
        public int LargeOfShip { get; set; }
        public bool isAlive = true;
        public bool installed = false;
        public int NumberOfSet { get; set; }

        public Ship(int largeOfShip)
        {
            LargeOfShip = largeOfShip;
            Coordinates = new Coordinates[LargeOfShip];
        }

        public void ClearCoordinates()
        {
            for (int i = 0; i < LargeOfShip; i++)
            {
                Coordinates[i] = new Coordinates(0, 0);
            }
            NumberOfSet = 0;
        }

        public virtual void BlockCells(Field field)
        {

        }
    }
}
