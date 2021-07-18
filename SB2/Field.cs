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
        public ImageList ImageList { get; set; }

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

        public bool CheckCell(int x, int y)
        {
            if (x > 9 || x < 0 || y > 9 || y < 0)
            {
                 return false;
            }
            else
            {
                if(map[y,x].Status != CellStatus.Empty)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public void ChangeStatus(int x, int y, CellStatus status, int numberOfSet, bool player)
        {
            if (x > 9 || x < 0 || y > 9 || y < 0)
            {
                return;
            }

            map[y, x].NumberOfSet = numberOfSet;
            map[y, x].Status = status;
            ChangeColor(x, y, player);
        }

        void ChangeColor(int x, int y, bool player)
        {
            if(player == true)
            {
                if (map[y, x].Status == CellStatus.HasShip)
                    map[y, x].BackColor = Color.Blue;
                if (map[y, x].Status == CellStatus.Blocked)
                    map[y, x].BackColor = Color.Black;
            }

            //map[y, x].BackColor = map[y, x].Status switch
            //{
            //    CellStatus.HasShip => Color.Blue,
            //    CellStatus.Blocked => Color.Black
            //};
        }
        public void BattleColors(Cell cell)
        {
            if (cell.Status == CellStatus.Blocked)
            {
                cell.BackColor = Color.LightGray;
                cell.Status = CellStatus.Empty;
            }
        }
        public void BlockCells(Cell cell, Ship ship, CellStatus shipStatus, CellStatus cellStatus, bool player)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (cell.Coordinates.X == map[i, j].Coordinates.X && cell.Coordinates.Y == map[i, j].Coordinates.Y &&
                        CheckCell(j, i) == true)
                    {


                        switch (ship.LargeOfShip)
                        {
                            case 1:
                                ChangeStatus(j, i, shipStatus, ship.NumberOfSet, player);
                                ChangeStatus(j + 1, i, cellStatus, ship.NumberOfSet, player);
                                ChangeStatus(j + 1, i + 1, cellStatus, ship.NumberOfSet, player);
                                ChangeStatus(j, i + 1, cellStatus, ship.NumberOfSet, player);
                                ChangeStatus(j - 1, i + 1, cellStatus, ship.NumberOfSet, player);
                                ChangeStatus(j - 1, i, cellStatus, ship.NumberOfSet, player);
                                ChangeStatus(j - 1, i - 1, cellStatus, ship.NumberOfSet, player);
                                ChangeStatus(j, i - 1, cellStatus, ship.NumberOfSet, player);
                                ChangeStatus(j + 1, i - 1, cellStatus, ship.NumberOfSet, player);
                                ship.Coordinates[0] = new Coordinates(j, i);
                                return;
                            case 2:
                                if (CheckCell(j + 1, i) == true)
                                {
                                    ChangeStatus(j, i, shipStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j + 1, i, shipStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j + 2, i, cellStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j + 2, i + 1, cellStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j + 1, i + 1, cellStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j, i + 1, cellStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j - 1, i + 1, cellStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j - 1, i, cellStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j - 1, i - 1, cellStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j, i - 1, cellStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j + 1, i - 1, cellStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j + 2, i - 1, cellStatus, ship.NumberOfSet, player);
                                    ship.Coordinates[0] = new Coordinates(j, i);
                                    ship.Coordinates[1] = new Coordinates(j + 1, i);
                                    return;
                                }
                                else
                                {
                                    if (CheckCell(j - 1, i) == true)
                                    {
                                        ChangeStatus(j, i, shipStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j - 1, i, shipStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j - 2, i, cellStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j - 2, i + 1, cellStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j - 1, i + 1, cellStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j, i + 1, cellStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j + 1, i + 1, cellStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j + 1, i, cellStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j + 1, i - 1, cellStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j, i - 1, cellStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j - 1, i - 1, cellStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j - 2, i - 1, cellStatus, ship.NumberOfSet, player);
                                        ship.Coordinates[0] = new Coordinates(j, i);
                                        ship.Coordinates[1] = new Coordinates(j - 1, i);
                                    }
                                }
                                return;
                            case 3:
                                if (CheckCell(j + 1, i) == true && CheckCell(j + 2, i) == true)
                                {
                                    ChangeStatus(j, i, shipStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j + 1, i, shipStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j + 2, i, shipStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j + 3, i, cellStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j + 3, i + 1, cellStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j + 2, i + 1, cellStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j + 1, i + 1, cellStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j, i + 1, cellStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j - 1, i + 1, cellStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j - 1, i, cellStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j - 1, i - 1, cellStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j, i - 1, cellStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j + 1, i - 1, cellStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j + 2, i - 1, cellStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j + 3, i - 1, cellStatus, ship.NumberOfSet, player);
                                    ship.Coordinates[0] = new Coordinates(j, i);
                                    ship.Coordinates[1] = new Coordinates(j, i + 1);
                                    ship.Coordinates[2] = new Coordinates(j, i + 2);
                                    return;
                                }
                                else
                                {
                                    if (CheckCell(j - 1, i) == true && CheckCell(j - 2, i) == true)
                                    {
                                        ChangeStatus(j, i, shipStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j - 1, i, shipStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j - 2, i, shipStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j - 3, i, cellStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j - 3, i + 1, cellStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j - 2, i + 1, cellStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j - 1, i + 1, cellStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j, i + 1, cellStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j + 1, i + 1, cellStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j + 1, i, cellStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j + 1, i - 1, cellStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j, i - 1, cellStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j - 1, i - 1, cellStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j - 2, i - 1, cellStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j - 3, i - 1, cellStatus, ship.NumberOfSet, player);
                                        ship.Coordinates[0] = new Coordinates(j, i);
                                        ship.Coordinates[1] = new Coordinates(j, i - 1);
                                        ship.Coordinates[2] = new Coordinates(j, i - 2);
                                    }
                                }
                                return;
                            case 4:
                                if (CheckCell(j + 1, i) == true && CheckCell(j + 2, i) == true && CheckCell(j + 3, i) == true)
                                {
                                    ChangeStatus(j, i, shipStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j + 1, i, shipStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j + 2, i, shipStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j + 3, i, shipStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j + 4, i, cellStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j + 4, i + 1, cellStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j + 3, i + 1, cellStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j + 2, i + 1, cellStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j + 1, i + 1, cellStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j, i + 1, cellStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j - 1, i + 1, cellStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j - 1, i, cellStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j - 1, i - 1, cellStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j, i - 1, cellStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j + 1, i - 1, cellStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j + 2, i - 1, cellStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j + 3, i - 1, cellStatus, ship.NumberOfSet, player);
                                    ChangeStatus(j + 4, i - 1, cellStatus, ship.NumberOfSet, player);
                                    ship.Coordinates[0] = new Coordinates(j, i);
                                    ship.Coordinates[1] = new Coordinates(j, i + 1);
                                    ship.Coordinates[2] = new Coordinates(j, i + 2);
                                    ship.Coordinates[3] = new Coordinates(j, i + 3);
                                    return;
                                }

                                else
                                {
                                    if (CheckCell(j - 1, i) == true && CheckCell(j - 2, i) == true && CheckCell(j - 3, i) == true)
                                    {
                                        ChangeStatus(j, i, shipStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j - 1, i, shipStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j - 2, i, shipStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j - 3, i, shipStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j - 4, i, cellStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j - 4, i + 1, cellStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j - 3, i + 1, cellStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j - 2, i + 1, cellStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j - 1, i + 1, cellStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j, i + 1, cellStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j + 1, i + 1, cellStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j + 1, i, cellStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j + 1, i - 1, cellStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j, i - 1, cellStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j - 1, i - 1, cellStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j - 2, i - 1, cellStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j - 3, i - 1, cellStatus, ship.NumberOfSet, player);
                                        ChangeStatus(j - 4, i - 1, cellStatus, ship.NumberOfSet, player);
                                        ship.Coordinates[0] = new Coordinates(j, i);
                                        ship.Coordinates[1] = new Coordinates(j, i - 1);
                                        ship.Coordinates[2] = new Coordinates(j, i - 2);
                                        ship.Coordinates[3] = new Coordinates(j, i - 3);
                                    }
                                }
                                return;
                        }
                    }
                }
            }
        }
        public void UnblockCells(Ship ship)
        {
            for(int i = 0; i < 10; i++)
            {
                for(int j = 0; j < 10; j++)
                {
                    if(map[i,j].NumberOfSet == ship.NumberOfSet)
                    {
                        map[i, j].ClearCell();
                        map[i, j].BackColor = Color.LightGray;
                    }
                }
            }
        }
        public void BlockDeadShipCell(Ship ship)
        {
            for(int i = 0; i < 10; i++)
            {
                for(int j = 0; j < 10; j++)
                {
                    if(ship.NumberOfSet == map[i,j].NumberOfSet && (map[i,j].Status == CellStatus.EmptyStriked || map[i,j].Status == CellStatus.Empty))
                    {
                        map[i,j].BackColor = Color.Black;
                    }
                }
            }
        }
    } 
}
