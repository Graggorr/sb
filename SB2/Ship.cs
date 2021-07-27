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
        public bool installed = false;
        public int NumberOfSet = 0;

        public Ship(int largeOfShip)
        {
            LargeOfShip = largeOfShip;
            Coordinates = new Coordinates[LargeOfShip];
        }
        public void SetCoordinates(Field field, int i, int x, int y, bool player)
        {
            NumberOfSet++;
                Coordinates[i] = new Coordinates(x, y);
                if (player)
                {
                    field.CellHasShip(x, y);
                }
                else
                {
                    field.CellHasShipHidden(x, y);
                }
                field.map[y, x].NumberOfSet = NumberOfSet;
        }
        private void ClearCoordinates()
        {
            for (int i = 0; i < LargeOfShip; i++)
            {
                Coordinates[i] = new Coordinates(0, 0);
            }
            NumberOfSet--;
        }

        public void BlockCells(Field field)
        {
            for(int i = 0; i < LargeOfShip; i++)
            {
                var x = Coordinates[i].X;
                var y = Coordinates[i].Y;
                field.BlockShipCell(x + 1, y);
                field.BlockShipCell(x + 1, y + 1);
                field.BlockShipCell(x, y + 1);
                field.BlockShipCell(x - 1, y + 1);
                field.BlockShipCell(x - 1, y);
                field.BlockShipCell(x - 1, y - 1);
                field.BlockShipCell(x, y - 1);
                field.BlockShipCell(x + 1, y - 1);
            }
        }
        public void UnblockCells(Field field)
        {
            for (int i = 0; i < LargeOfShip; i++)
            {
                var x = Coordinates[i].X;
                var y = Coordinates[i].Y;
                field.UnblockDeletedShipCell(x, y);
                field.UnblockDeletedShipCell(x + 1, y);
                field.UnblockDeletedShipCell(x + 1, y + 1);
                field.UnblockDeletedShipCell(x, y + 1);
                field.UnblockDeletedShipCell(x - 1, y + 1);
                field.UnblockDeletedShipCell(x - 1, y);
                field.UnblockDeletedShipCell(x - 1, y - 1);
                field.UnblockDeletedShipCell(x, y - 1);
                field.UnblockDeletedShipCell(x + 1, y - 1);
            }
            ClearCoordinates();
        }
        public bool CheckDeadShip(Field field) 
        {
            for(int i = 0; i < 10; i++)
            {
                for(int j = 0; j < 10; j++)
                {
                    if (field.map[i, j].Status == CellStatus.HasShip && field.map[i, j].NumberOfSet == NumberOfSet)
                        return true;
                }
            }
            return false;
        }
    }
}
