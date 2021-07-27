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
            if (player == true)
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        map[i, j] = new Cell(j, i, new Point(((j + 1) * 29) + 100, ((i + 1) * 29) + 10), new Size(30, 30));
                    }
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        map[i, j] = new Cell(j, i, new Point(((j + 1) * 29) + 500, ((i + 1) * 29) + 10), new Size(30, 30));
                    }
                }
            }
        }

        private bool CheckCoordinates(int x, int y)
        {
            if (x > 9 || x < 0 || y > 9 || y < 0)
            {
                return false;
            }
            return true;
        }

        private bool CheckCell(int x, int y)
        {
            if (!CheckCoordinates(x, y))
                return false;

            if (map[y, x].Status != CellStatus.Empty)
            {
                return false;
            }
            return true;
        }

        public void BlockCells(Cell cell, Ship ship, bool player)
        {
            int x = cell.Coordinates.X;
            int y = cell.Coordinates.Y;
            if (CheckCell(x, y))
            {
                switch (ship.LargeOfShip)
                {
                    case 1:
                        ship.SetCoordinates(this, 0, x, y, player);
                        ship.BlockCells(this);
                        break;
                    case 2:
                        if (CheckCell(x + 1, y))
                        {
                            ship.SetCoordinates(this, 0, x, y, player);
                            ship.SetCoordinates(this, 1, x + 1, y, player);
                            ship.BlockCells(this);
                            return;
                        }
                        if (CheckCell(x - 1, y))
                        {
                            ship.SetCoordinates(this, 0, x, y, player);
                            ship.SetCoordinates(this, 1, x - 1, y, player);
                            ship.BlockCells(this);
                        }
                        break;
                    case 3:
                        if (CheckCell(x + 1, y) && CheckCell(x + 2, y))
                        {
                            ship.SetCoordinates(this, 0, x, y, player);
                            ship.SetCoordinates(this, 1, x + 1, y, player);
                            ship.SetCoordinates(this, 2, x + 2, y, player);
                            ship.BlockCells(this);
                            return;
                        }
                        if (CheckCell(x - 1, y) && CheckCell(x - 2, y))
                        {
                            ship.SetCoordinates(this, 0, x, y, player);
                            ship.SetCoordinates(this, 1, x - 1, y, player);
                            ship.SetCoordinates(this, 2, x - 2, y, player);
                            ship.BlockCells(this);
                        }
                        break;
                    case 4:
                        if (CheckCell(x + 1, y) && CheckCell(x + 2, y) && CheckCell(x + 3, y))
                        {
                            ship.SetCoordinates(this, 0, x, y, player);
                            ship.SetCoordinates(this, 1, x + 1, y, player);
                            ship.SetCoordinates(this, 2, x + 2, y, player);
                            ship.SetCoordinates(this, 3, x + 3, y, player);
                            ship.BlockCells(this);
                            return;
                        }
                        if (CheckCell(x - 1, y) && CheckCell(x - 2, y) && CheckCell(x - 3, y))
                        {
                            ship.SetCoordinates(this, 0, x, y, player);
                            ship.SetCoordinates(this, 1, x - 1, y, player);
                            ship.SetCoordinates(this, 2, x - 2, y, player);
                            ship.SetCoordinates(this, 3, x - 3, y, player);
                            ship.BlockCells(this);
                            return;
                        }
                        break;
                }
            }
        }
        public void BlockShipCell(int x, int y)
        {
            if (!CheckCoordinates(x, y))
                return;

            if (map[y, x].Status == CellStatus.Empty)
            {
                map[y, x].Status = CellStatus.Blocked;
                map[y, x].BackColor = Color.Black;
            }
        }

        public void UnblockDeletedShipCell(int x, int y)
        {
            if (!CheckCoordinates(x, y))
                return;

            if (map[y, x].Status == CellStatus.HasShip || map[y, x].Status == CellStatus.Blocked)
            {
                map[y, x].Status = CellStatus.Empty;
                map[y, x].BackColor = Button.DefaultBackColor;
            }
        }

        public void ChangeColor(Cell cell, CellStatus status, Color color)
        {
            cell.Status = status;
            cell.BackColor = color;
        }

        public void CellHasShip(int x, int y)
        {
            if (!CheckCell(x, y))
                return;
            map[y, x].Status = CellStatus.HasShip;
            map[y, x].BackColor = Color.Blue;
        }
        public void CellHasShipHidden(int x, int y)
        {
            if (!CheckCell(x, y))
                return;
            map[y, x].Status = CellStatus.HasShipHidden;
            map[y, x].BackColor = Color.Green;
        }
        public void BattleColors(Cell cell)
        {
            if (cell.Status != CellStatus.HasShip && cell.Status != CellStatus.HasShipHidden)
            {
                cell.BackColor = Button.DefaultBackColor;
                cell.Status = CellStatus.Empty;
            }
        }

        public bool CheckShipCell(Cell cell, Ship ship)
        {
            int x = cell.Coordinates.X;
            int y = cell.Coordinates.Y;
            if (CheckCell(x, y))
            {
                switch (ship.LargeOfShip)
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
        public bool CheckCellForShoot(Cell cell)
        {
            if (!CheckCoordinates(cell.Coordinates.X, cell.Coordinates.Y))
                return false;
            if (cell.Status == CellStatus.EmptyStriked || cell.Status == CellStatus.ShipDamaged || cell.Status == CellStatus.Blocked)
                return false;
            return true;
        }
    }
}