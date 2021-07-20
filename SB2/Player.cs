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
        public bool yourTurn = true;
        public const string QUADROKEY = "QUADRO";
        public const string TRIPLEKEY = "TRIPLE";
        public const string DOUBLEKEY = "DOUBLE";
        public const string SINGLEKEY = "SINGLE";
        public static int NumberOfSet = 0;
        public bool FoundShip { get; set; }

        public List<Ship> SingleRankShips { get; private set; }
        public List<Ship> DoubleRankShips { get; private set; }
        public List<Ship> TripleRankShips { get; private set; }
        public List<Ship> QuadroRankShips { get; private set; }
        public List<string> Keys { get; private set; }
        public Dictionary<string, List<Ship>> Warships { get; private set; }
        public Stack<Ship> ShipStack { get; set; }

        public Player()
        {
            field = new Field(true);
            Initialize();
        }

        protected void Initialize()
        {
            SingleRankShips = new List<Ship> { new Ship(1), new Ship(1), new Ship(1), new Ship(1) };
            DoubleRankShips = new List<Ship> { new Ship(2), new Ship(2), new Ship(2) };
            TripleRankShips = new List<Ship> { new Ship(3), new Ship(3) };
            QuadroRankShips = new List<Ship> { new Ship(4) };

            Keys = new List<string> { SINGLEKEY, DOUBLEKEY, TRIPLEKEY, QUADROKEY };

            Warships = new Dictionary<string, List<Ship>>
            {
                { SINGLEKEY, SingleRankShips },
                { DOUBLEKEY, DoubleRankShips },
                { TRIPLEKEY, TripleRankShips },
                { QUADROKEY, QuadroRankShips }
            };

            ShipStack = new Stack<Ship>();
        }

        public void SetShips(string Key, Cell cell, bool player)
        {
            foreach (var ship in Warships[Key])
            {
                if (ship.installed == false)
                {
                    Stack(ship, cell, player, CellStatus.HasShip);
                    goto LoopEnd;
                }
            }
        LoopEnd:
            return;
        }

        public void RemoveShips(Ship ship)
        {
            NumberOfSet--;

            ship = ShipStack.Pop();
            ship.installed = false;

            field.UnblockCells(ship);
            ship.ClearCoordinates();
        }

        public void Stack(Ship ship, Cell cell, bool player, CellStatus shipStatus)
        {
            field.BlockCells(cell, ship, shipStatus, CellStatus.Blocked, player);

            ship.installed = true;
            NumberOfSet++;
            ship.NumberOfSet = NumberOfSet;
            
            ShipStack.Push(ship);
        }

        public void Strike(Cell clickedcell)
        {
            if (clickedcell.Status == CellStatus.EmptyStriked || clickedcell.Status == CellStatus.ShipDamaged 
                ||!yourTurn)
            {
                return;
            }

            if (clickedcell.Status == CellStatus.Empty)
            {
                clickedcell.Status = CellStatus.EmptyStriked;
                clickedcell.BackColor = Color.Black;
            }
            if (clickedcell.Status == CellStatus.HasShip)
            {
                clickedcell.Status = CellStatus.ShipDamaged;
                clickedcell.BackColor = Color.Red;
                CheckShips();
            }
            yourTurn = false;
        }

        public void CheckShips()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (field.map[i, j].Status == CellStatus.HasShip)
                    {
                        goto LoopEnd;
                    }
                    else
                    {
                        Lost();
                    }
                }
            }
        LoopEnd:
            return;
        }

        public bool DeadShip(Cell cell)
        {
            if (cell.NumberOfSet == 0)
            {
                return true;
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (field.map[i, j].NumberOfSet == cell.NumberOfSet && (field.map[i, j].Status == CellStatus.HasShip || field.map[i, j].Status == CellStatus.HasShipHidden))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void Lost()
        {
            DialogResult result = MessageBox.Show(
                "The game is over",
                "Message",
                MessageBoxButtons.OK);
            if (result == DialogResult.OK)
            {Application.Exit();}
        }
    }
}
