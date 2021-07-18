using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB2
{
    public class Bot: Player
    {
        public Bot()
        {
            field = new Field(false);
            Initialize();
            yourTurn = false;
        }
        public void SetShips(Cell cell)
        {
            foreach(var key in Keys)
            {
                foreach(var ship in Warships[key])
                {
                    Stack(ship, cell, false, CellStatus.HasShipHidden);
                    goto LoopEnd;
                }
            }
            LoopEnd:
            return;
        }
    }
}
