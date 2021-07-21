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
        public int Count = 0;
        int ShotX, ShotY;
        public Bot()
        {
            field = new Field(false);
            Initialize();
            yourTurn = false;
        }
        public void SetShips(Cell cell)
        {
            foreach (var warships in Warships)
            {
                foreach (var ship in warships.Value)
                {
                    if (!ship.installed && field.CheckShipCell(cell, ship))
                    {
                        Stack(ship, cell, false, CellStatus.HasShipHidden);
                        Count++;
                    }
                }
            }
        }
        public void Strike(Player player)
        {
            Random rng = new Random();
            if (yourTurn == true)
            {
                int x, y, x1;
            LoopEnd:
                x = rng.Next(0, 9);
                y = rng.Next(0, 9);
                x1 = x + 1;
                if (player.field.map[y, x].Status != CellStatus.EmptyStriked && player.field.map[y, x].Status != CellStatus.ShipDamaged)
                {
                    RepeatShot1:
                    if (player.field.map[y, x].Status == CellStatus.HasShip)
                    {
                        player.field.ChangeColor(player.field.map[y, x], CellStatus.ShipDamaged, Color.Red);
                    RepeatShot2:
                        if (DeadShip(player.field.map[y, x]))
                        {
                            player.CheckShips();
                            goto LoopEnd;
                        }
                        if (player.field.CheckCell(x1,y) && player.field.map[y, x1].Status == CellStatus.HasShip)
                        {
                            player.field.ChangeColor(player.field.map[y, x1], CellStatus.ShipDamaged, Color.Red);
                            x = x1;
                            x1++;
                            goto RepeatShot2;
                        }
                        else
                        {
                            if (!player.field.CheckCell(x1, y))
                            {
                                x--;
                                goto RepeatShot1;
                            }
                            if(player.field.map[y, x1].Status != CellStatus.HasShip)
                            {
                                player.field.ChangeColor(player.field.map[y, x1], CellStatus.EmptyStriked, Color.Black);
                                yourTurn = false;
                                player.yourTurn = true;
                                return;
                            }
                        }
                    }
                    else
                    {
                        player.field.ChangeColor(player.field.map[y, x], CellStatus.EmptyStriked, Color.Black);
                        yourTurn = false;
                        player.yourTurn = true;
                        return;
                    }
                }
                goto LoopEnd;
            }
        }

    }
}
