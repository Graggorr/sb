using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SB2
{
    public class Bot : Player
    {
        private int ShotX;
        private int ShotY;
        private bool RememberedShot = false;

        public Bot()
        {
            field = new Field(false);
            Count = 0;
            yourTurn = false;
            Initialize();
        }
        public void SetShips(Cell cell)
        {
            foreach (var warships in Warships)
            {
                foreach (var ship in warships.Value)
                {
                    if (!ship.installed && field.CheckShipCell(cell, ship))
                    {
                        Stack(ship, cell, false);
                        return;
                    }
                }
            }
        }

        public void Strike(Player player)
        {
            Random rng = new Random();

            if (yourTurn == true)
            {
                int x = 0, y = 0, x1 = 0;

                if (RememberedShot)
                {
                    RememberCoordinates(x, y);
                    goto RememberedShot;
                }

            NewCoordinates:
                x = rng.Next(0, 9);
                y = rng.Next(0, 9);

            RememberedShot:
                x1 = x++;

                if (field.CheckCellForShoot(player.field.map[y, x]))
                {
                    if (player.field.map[y, x].Status == CellStatus.HasShip)
                    {
                        field.ChangeColor(player.field.map[y, x], CellStatus.ShipDamaged, Color.Red);

                        if (!DeadShip(player.field.map[y, x]) && field.CheckCellForShoot(player.field.map[y, x1]))
                        {
                        RepeatShot:

                            field.ChangeColor(player.field.map[y, x1], CellStatus.ShipDamaged, Color.Red);
                            x1++;

                            if (!DeadShip(player.field.map[y, x]) && field.CheckCellForShoot(player.field.map[y, x1]) && player.field.map[y, x1].Status == CellStatus.HasShip)
                            {
                                goto RepeatShot;
                            }
                            else
                            {

                                if (!DeadShip(player.field.map[y, x]) && field.CheckCellForShoot(player.field.map[y, x1]) && player.field.map[y, x1].Status == CellStatus.Empty)
                                {
                                    field.ChangeColor(player.field.map[y, x], CellStatus.EmptyStriked, Color.Black);
                                    ShotX = x1;
                                    ShotY = y;
                                    yourTurn = false;
                                    return;
                                }

                                if(DeadShip(player.field.map[y, x]))
                                {
                                    goto NewCoordinates;
                                }

                                if (!field.CheckCellForShoot(player.field.map[y, x]))
                                {
                                    x--;
                                    goto RepeatShot;
                                }

                            }
                        }

                        else
                        {
                            if (DeadShip(player.field.map[y, x]))
                            {
                                goto NewCoordinates;
                            }
                        }
                    }

                    else
                    {
                        field.ChangeColor(player.field.map[y, x], CellStatus.EmptyStriked, Color.Black);
                        yourTurn = false;
                    }
                }
                else
                {
                    goto NewCoordinates;
                }
            }
        }

        private void RememberCoordinates(int x, int y)
        {
            if (RememberedShot)
            {
                x = ShotX;
                y = ShotY;
            }
        }

        //public void Strike(Player player)
        //{
        //    Random rng = new Random();
        //    if (yourTurn == true)
        //    {
        //        int x, y, x1;
        //    LoopEnd:
        //        x = rng.Next(0, 9);
        //        y = rng.Next(0, 9);
        //        x1 = x + 1;
        //        if (player.field.map[y, x].Status != CellStatus.EmptyStriked && player.field.map[y, x].Status != CellStatus.ShipDamaged)
        //        {
        //            RepeatShot1:
        //            if (player.field.map[y, x].Status == CellStatus.HasShip)
        //            {
        //                player.field.ChangeColor(player.field.map[y, x], CellStatus.ShipDamaged, Color.Red);
        //            RepeatShot2:
        //                if (DeadShip(player.field.map[y, x]))
        //                {
        //                    player.CheckShips();
        //                    goto LoopEnd;
        //                }
        //                if (player.field.CheckCell(x1,y) && player.field.map[y, x1].Status == CellStatus.HasShip)
        //                {
        //                    player.field.ChangeColor(player.field.map[y, x1], CellStatus.ShipDamaged, Color.Red);
        //                    x = x1;
        //                    x1++;
        //                    goto RepeatShot2;
        //                }
        //                else
        //                {
        //                    if (!player.field.CheckCell(x1, y))
        //                    {
        //                        x--;
        //                        goto RepeatShot1;
        //                    }
        //                    if(player.field.map[y, x1].Status != CellStatus.HasShip)
        //                    {
        //                        player.field.ChangeColor(player.field.map[y, x1], CellStatus.EmptyStriked, Color.Black);
        //                        yourTurn = false;
        //                        player.yourTurn = true;
        //                        return;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                player.field.ChangeColor(player.field.map[y, x], CellStatus.EmptyStriked, Color.Black);
        //                yourTurn = false;
        //                player.yourTurn = true;
        //                return;
        //            }
        //        }
        //        goto LoopEnd;
        //    }
        //}

    }
}
