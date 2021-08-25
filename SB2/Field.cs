using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace SB2
{
    public class Field
    {
        public Cell[,] map = new Cell[10, 10];

        public Field(bool player)
        {
            int x;
            if (player)
                x = 100;

            else
                x = 500;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    map[i, j] = new Cell(j, i, new Point(((j + 1) * 29) + x, ((i + 1) * 29) + 10), new Size(30, 30));
                }
            }
        }

        public bool CheckCoordinates(int x, int y)
        {
            if (x > 9 || x < 0 || y > 9 || y < 0)
                return false;

            return true;
        }

        private bool CheckCell(int x, int y)
        {
            if (!CheckCoordinates(x, y) || map[y, x].Status != CellStatus.Empty)
                return false;

            return true;
        }

        public void BlockCells(Cell cell, Ship ship, bool player)
        {
            var x = cell.Coordinates.X;
            var y = cell.Coordinates.Y;

            if (CheckCell(x, y))
            {
                switch (ship.LargeOfShip)
                {
                    case 1:
                        ship.SetCoordinates(this, x, y, player, true);
                        ship.ManageCells(this, true, player, false);
                        break;

                    case 2:
                        if (CheckCell(x + 1, y))
                        {
                            ship.SetCoordinates(this, x, y, player, true);
                            ship.ManageCells(this, true, player, false);
                            return;
                        }
                        if (CheckCell(x - 1, y))
                        {
                            ship.SetCoordinates(this, x, y, player, false);
                            ship.ManageCells(this, true, player, false);
                            return;
                        }
                        break;

                    case 3:
                        if (CheckCell(x + 1, y) && CheckCell(x + 2, y))
                        {
                            ship.SetCoordinates(this, x, y, player, true);
                            ship.ManageCells(this, true, player, false);
                            return;
                        }
                        if (CheckCell(x - 1, y) && CheckCell(x - 2, y))
                        {
                            ship.SetCoordinates(this, x, y, player, false);
                            ship.ManageCells(this, true, player, false);
                            return;
                        }
                        break;

                    case 4:
                        if (CheckCell(x + 1, y) && CheckCell(x + 2, y) && CheckCell(x + 3, y))
                        {
                            ship.SetCoordinates(this, x, y, player, true);
                            ship.ManageCells(this, true, player, false);
                            return;
                        }
                        if (CheckCell(x - 1, y) && CheckCell(x - 2, y) && CheckCell(x - 3, y))
                        {
                            ship.SetCoordinates(this, x, y, player, false);
                            ship.ManageCells(this, true, player, false);
                            return;
                        }
                        break;
                }
            }
        }

        public void TakeCoordinates(int x, int y, bool blockCells, bool player, bool deadShip)
        {
            Coordinates[] coordinates = new Coordinates[9];
            coordinates[0] = new Coordinates(x, y);
            coordinates[1] = new Coordinates(x + 1, y);
            coordinates[2] = new Coordinates(x + 1, y + 1);
            coordinates[3] = new Coordinates(x, y + 1);
            coordinates[4] = new Coordinates(x - 1, y + 1);
            coordinates[5] = new Coordinates(x - 1, y);
            coordinates[6] = new Coordinates(x - 1, y - 1);
            coordinates[7] = new Coordinates(x, y - 1);
            coordinates[8] = new Coordinates(x + 1, y - 1);

            if (blockCells && !deadShip)
            {
                BlockShipCell(coordinates, player);
                return;
            }

            if(blockCells && deadShip)
            {
                BlockDeadShipCells(coordinates);
                return;
            }

            UnblockDeletedShipCell(coordinates);
        }

        private void BlockShipCell(Coordinates[] coordinates, bool player)
        {
            for (var i = 0; i < coordinates.Length; i++)
            {
                var x = coordinates[i].X;
                var y = coordinates[i].Y;

                if (CheckCoordinates(x, y) && map[y, x].Status == CellStatus.Empty)
                {
                    map[y, x].Status = CellStatus.Blocked;

                    if (player)
                        map[y, x].BackColor = Color.Black;
                }
            }
        }

        private void UnblockDeletedShipCell(Coordinates[] coordinates)
        {
            for (var i = 0; i < coordinates.Length; i++)
            {
                var x = coordinates[i].X;
                var y = coordinates[i].Y;

                if (CheckCoordinates(x, y) && (map[y, x].Status == CellStatus.HasShip || map[y, x].Status == CellStatus.Blocked))
                {
                    map[y, x].Status = CellStatus.Empty;
                    map[y, x].BackColor = Button.DefaultBackColor;
                }
            }
        }

        private void BlockDeadShipCells(Coordinates[] coordinates)
        {
            for(var i = 0; i < coordinates.Length; i++)
            {
                var x = coordinates[i].X;
                var y = coordinates[i].Y;
                
                if(CheckCoordinates(x, y) && map[y, x].Status != CellStatus.ShipDamaged)
                {
                    map[y, x].Status = CellStatus.Blocked;
                    map[y, x].BackColor = Color.Black;
                }

            }
        }

        public void ChangeColor(Cell cell, CellStatus status, Color color)
        {
            cell.Status = status;
            cell.BackColor = color;
        }

        public void CellHasShip(int x, int y, bool player)
        {
            if (player)
            {
                map[y, x].Status = CellStatus.HasShip;
                map[y, x].BackColor = Color.Blue;
            }
            else
            {
                map[y, x].Status = CellStatus.HasShipHidden;
            }
        }

        public void BattleColors(Cell cell)
        {
            if (cell.Status != CellStatus.HasShip && cell.Status != CellStatus.HasShipHidden)
            {
                cell.BackColor = Button.DefaultBackColor;
                cell.Status = CellStatus.Empty;
            }
        }

        public bool CheckShipCell(Cell cell, int largeOfShip)
        {
            var x = cell.Coordinates.X;
            var y = cell.Coordinates.Y;

            if (CheckCell(x, y))
            {
                switch (largeOfShip)
                {
                    case 1:
                        return true;

                    case 2:
                        if (CheckCell(x + 1, y))
                        {
                            return true;
                        }
                        if (CheckCell(x - 1, y))
                        {
                            return true;
                        }
                        return false;

                    case 3:
                        if (CheckCell(x + 1, y) && CheckCell(x + 2, y))
                        {
                            return true;
                        }
                        if (CheckCell(x - 1, y) && CheckCell(x - 2, y))
                        {
                            return true;
                        }
                        return false;

                    case 4:
                        if (CheckCell(x + 1, y) && CheckCell(x + 2, y) && CheckCell(x + 3, y))
                        {
                            return true;
                        }
                        if (CheckCell(x - 1, y) && CheckCell(x - 2, y) && CheckCell(x - 3, y))
                        {
                            return true;
                        }
                        return false;
                }
            }
            return false;
        }
    }
}