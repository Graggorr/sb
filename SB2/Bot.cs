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
        private int shotX { get; set; }
        private int shotY { get; set; }
        private bool rememberedShot { get; set; }
        private bool shotIsGot { get; set; }
        private List<Coordinates> StackedCoordinates { get; set; }
        private Coordinates c { get; set; }

        public Bot()
        {
            field = new Field(false);
            Count = 0;
            yourTurn = false;
            StackedCoordinates = new List<Coordinates>();
            Initialize();
        }

        public void SetShips(Cell cell)
        {
            foreach (var warships in Warships)
            {
                foreach (var ship in warships.Value)
                {
                    if (!field.CheckShipCell(cell, ship.LargeOfShip))
                    {
                        return;
                    }

                    if (!ship.installed)
                    {
                        Stack(ship, cell, false);

                        if (ship.installed)
                            Count++;

                        return;
                    }
                }
            }
        }

        public void StackCoordinate(int x, int y)
        {
            c = new Coordinates(x, y);
            StackedCoordinates.Add(c);
        }

        public void Strike(Player player)
        {
            if (yourTurn)
            {
                int x, y;

                if (rememberedShot)
                {
                    x = shotX;
                    y = shotY;
                }

                else
                {
                    Random rng = new Random();
                repeat:
                    x = rng.Next(0, 10);
                    y = rng.Next(0, 10);
                    foreach(var coordinates in StackedCoordinates)
                    {
                        if (x == coordinates.X && y == coordinates.Y)
                            goto repeat;
                    }
                    StackCoordinate(x, y);
                }

                Shoot(x, y, player);

                if (shotIsGot)
                    Strike(player);
            }
        }

        private void Shoot(int x, int y, Player player)
        {

            if (!player.field.CheckCoordinates(x, y) || player.field.map[y, x].Status == CellStatus.Blocked || player.field.map[y, x].Status == CellStatus.EmptyStriked)
            {
                shotIsGot = true;
                rememberedShot = false;

                return;
            }

            if(player.field.map[y, x].Status == CellStatus.ShipDamaged && rememberedShot)
            {
                Shoot(x - 1, y, player);
            }

            if(player.field.map[y, x].Status == CellStatus.ShipDamaged && !rememberedShot)
            {
                shotIsGot = true;
                rememberedShot = false;

                return;
            }

            if (player.field.map[y, x].Status == CellStatus.HasShip)
            {
                field.ChangeColor(player.field.map[y, x], CellStatus.ShipDamaged, Color.Red);

                shotIsGot = true;
                KeepShooting(x, y, player);
            }

            if (player.field.map[y, x].Status == CellStatus.Empty)
            {
                field.ChangeColor(player.field.map[y, x], CellStatus.EmptyStriked, Color.Black);

                yourTurn = false;
                player.yourTurn = true;
                shotIsGot = false;
                rememberedShot = false;
            }
        }

        private void KeepShooting(int x, int y, Player player)
        {
            if (!player.DeadShip(player.field.map[y, x]))
            {
                if (rememberedShot && player.field.CheckCoordinates(x - 1, y))
                {
                    Shoot(x - 1, y, player);
                }

                if (!rememberedShot && player.field.CheckCoordinates(x + 1, y))
                {
                    Shoot(x + 1, y, player);

                    if (player.field.map[y, x + 1].Status == CellStatus.EmptyStriked)
                        RememberCoordinates(x - 1, y);
                }
            }

            else
            {
                player.CheckShips();
                return;
            }
        }
        private void RememberCoordinates(int x, int y)
        {
            shotX = x;
            shotY = y;
            rememberedShot = true;
        }
    }
}
