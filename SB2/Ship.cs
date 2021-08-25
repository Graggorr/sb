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
        public bool installed { get; set; }

        public Ship(int largeOfShip)
        {
            LargeOfShip = largeOfShip;
            Coordinates = new Coordinates[LargeOfShip];
            installed = false;
        }

        public void SetCoordinates(Field field, int x, int y, bool player, bool rightSide)
        {
            for (var i = 0; i < LargeOfShip; i++)
            {
                Coordinates[i] = new Coordinates(x, y);
                field.CellHasShip(x, y, player);

                if (rightSide)

                    x++;
                else
                    x--;
            }
        }
        private void ClearCoordinates()
        {
            for (var i = 0; i < LargeOfShip; i++)
            {
                Coordinates[i] = new Coordinates(-1, -1);
            }
        }

        public void ManageCells(Field field, bool blockCells, bool player, bool deadShip)
        {
            for (var i = 0; i < LargeOfShip; i++)
            {
                field.TakeCoordinates(Coordinates[i].X, Coordinates[i].Y, blockCells, player, deadShip);
            }

            if (!blockCells)
                ClearCoordinates();
        }
        public bool CheckDeadShip(Field field)
        {
            for (var i = 0; i < LargeOfShip; i++)
            {
                if (field.map[Coordinates[i].Y, Coordinates[i].X].Status == CellStatus.HasShip || field.map[Coordinates[i].Y, Coordinates[i].X].Status == CellStatus.HasShipHidden)
                    return true;
            }
            return false;
        }
    }
}
