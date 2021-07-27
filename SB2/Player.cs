using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace SB2
{
    public class Player
    {
        public Field field;
        public bool yourTurn { set; get; }
        public const string QUADROKEY = "QUADRO";
        public const string TRIPLEKEY = "TRIPLE";
        public const string DOUBLEKEY = "DOUBLE";
        public const string SINGLEKEY = "SINGLE";
        public int Count { protected set; get; }

        public List<Ship> SingleRankShips { get; private set; }
        public List<Ship> DoubleRankShips { get; private set; }
        public List<Ship> TripleRankShips { get; private set; }
        public List<Ship> QuadroRankShips { get; private set; }
        public List<string> Keys { get; private set; }
        public Dictionary<string, List<Ship>> Warships { get; private set; }
        public Stack<Ship> ShipStack { get; private set; }

        public Player()
        {
            field = new Field(true);
            Count = 0;
            yourTurn = true;
            Initialize();
        }

        protected void Initialize()
        {
            SingleRankShips = new List<Ship> { new Ship(1), new Ship(1), new Ship(1), new Ship(1) };
            DoubleRankShips = new List<Ship> { new Ship(2), new Ship(2), new Ship(2) };
            TripleRankShips = new List<Ship> { new Ship(3), new Ship(3) };
            QuadroRankShips = new List<Ship> { new Ship(4) };

            Warships = new Dictionary<string, List<Ship>>
            {
                { SINGLEKEY, SingleRankShips },
                { DOUBLEKEY, DoubleRankShips },
                { TRIPLEKEY, TripleRankShips },
                { QUADROKEY, QuadroRankShips }
            };

            ShipStack = new Stack<Ship>();
        }

        public void SetShips(string Key, Cell cell)
        {
            foreach (var ship in Warships[Key])
            {
                if (!ship.installed && field.CheckShipCell(cell, ship))
                {
                    Stack(ship, cell, true);
                    Count++;
                    return;
                }
            }
        }
        protected void Stack(Ship ship, Cell cell, bool player)
        {
            ship.installed = true;

            field.BlockCells(cell, ship, player);

            ShipStack.Push(ship);

        }

        public void RemoveShips(Ship ship)
        {
            ship = ShipStack.Pop();
            ship.installed = false;
            ship.UnblockCells(field);
            Count--;
        }

        public void BlockCellsAfterRemove()
        {
            foreach (var ship in ShipStack)
            {
                ship.BlockCells(field);
            }
        }
        public bool DeadShip(Cell cell)
        {
            foreach (var warship in Warships)
            {
                foreach (var ship in warship.Value)
                {
                    if (cell.NumberOfSet == ship.NumberOfSet && ship.CheckDeadShip(field))
                        return true;
                }
            }
            return false;
        }


        public void CheckShips()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (field.map[i, j].Status == CellStatus.HasShip || field.map[i, j].Status == CellStatus.HasShipHidden)
                    {
                        return;
                    }
                }
            }
            Lost();
        }

        private void Lost()
        {
            DialogResult result = MessageBox.Show(
                "The game is over",
                "Message",
                MessageBoxButtons.OK);
            if (result == DialogResult.OK)
            {
                Application.Exit();
            }
        }
    }
}