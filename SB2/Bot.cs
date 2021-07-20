using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB2
{
    public class Bot : Player
    {
        public int Count = 0;
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
                    if (!ship.installed && StackBot(ship, cell, false, CellStatus.HasShipHidden))
                    {
                        Count++;
                    }
                }
            }
        }
        public bool StackBot(Ship ship, Cell cell, bool player, CellStatus shipStatus)
        {
            if (!field.CheckShipCell(cell, ship))
                return false;

            field.BlockCells(cell, ship, shipStatus, CellStatus.Blocked, player);

            NumberOfSet++;

            ship.installed = true;
            ship.NumberOfSet = NumberOfSet;

            ShipStack.Push(ship);

            return true;
        }
    }
}
