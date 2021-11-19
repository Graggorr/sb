using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SB2
{
    public partial class Form1 : Form
    {
        Player player;
        Bot bot;
        public const int FieldSize = 10;
        public Form1()
        {
            InitializeComponent();
            CreateGame();
        }

        private void CreateGame()
        {
            player = new Player();
            bot = new Bot();
            SetBotShips();
            for (int i = 0; i < Form1.FieldSize; i++)
            {
                for (int j = 0; j < Form1.FieldSize; j++)
                {
                    Controls.Add(player.field.map[i, j]);
                    Controls.Add(bot.field.map[i, j]);
                    player.field.map[i, j].Click += new EventHandler(SetPlayerShips);
                }
            }
            SingleRankButton.Visible = true;
            DoubleRankButton.Visible = true;
            TripleRankButton.Visible = true;
            QuadroRankButton.Visible = true;
            RemoveShipButton.Visible = true;
            Button startButton = new Button
            {
                Text = "Start",
                Size = new Size(60, 20),
                Location = new Point(450, 450)
            };
            Controls.Add(startButton);
            startButton.Click += new EventHandler(StartGame);

        }
        private void StartGame(Object sender, EventArgs e)
        {
            if (player.Count == 10)
            {
                Button clickedButton = sender as Button;
                {
                    for (int i = 0; i < Form1.FieldSize; i++)
                    {
                        for (int j = 0; j < Form1.FieldSize; j++)
                        {
                            player.field.map[i, j].Click -= new EventHandler(SetPlayerShips);
                            player.field.BattleColors(player.field.map[i, j]);
                            bot.field.BattleColors(bot.field.map[i, j]);
                        }
                    }
                    Controls.Remove(clickedButton);
                    Controls.Remove(SingleRankButton);
                    Controls.Remove(DoubleRankButton);
                    Controls.Remove(TripleRankButton);
                    Controls.Remove(QuadroRankButton);
                    Controls.Remove(RemoveShipButton);
                    War();
                }
            }
            else
            {
                MessageBox.Show("There are no 10 ships");
            }
        }
        private void War()
        {
            for (int i = 0; i < Form1.FieldSize; i++)
            {
                for (int j = 0; j < Form1.FieldSize; j++)
                {
                    bot.field.map[i, j].Click += new EventHandler(PlayerStrike);
                }
            }
        }

        private void SetBotShips()
        {
            int x, y;
            Random rng = new Random();
            const int shipsCount = 10;
            while (bot.Count < shipsCount)
            {
                x = rng.Next(0, 9);
                y = rng.Next(0, 9);
                bot.SetShips(bot.field.map[y, x]);
            }
        }

        private void SetPlayerShips(object sender, EventArgs e)
        {
            Cell clickedCell = sender as Cell;
            if (SingleRankButton.Checked)
            {
                player.SetShips(Player.SINGLEKEY, clickedCell);
            }
            if (DoubleRankButton.Checked)
            {
                player.SetShips(Player.DOUBLEKEY, clickedCell);
            }
            if (TripleRankButton.Checked)
            {
                player.SetShips(Player.TRIPLEKEY, clickedCell);
            }
            if (QuadroRankButton.Checked)
            {
                player.SetShips(Player.QUADROKEY, clickedCell);
            }
        }

        private void RemoveShipButton_Click(object sender, EventArgs e)
        {
            foreach (var ship in player.ShipStack)
            {
                player.RemoveShips(ship);
                break;
            }
        }

        private void PlayerStrike(object sender, EventArgs e)
        {
            Cell clickedcell = sender as Cell;
            if (player.yourTurn == true)
            {
                if (clickedcell.Status != CellStatus.EmptyStriked && clickedcell.Status != CellStatus.ShipDamaged && clickedcell.Status != CellStatus.Blocked)
                {
                    if (clickedcell.Status == CellStatus.Empty)
                    {
                        bot.field.ChangeColor(clickedcell, CellStatus.EmptyStriked, Color.Black);

                        player.yourTurn = false;
                        bot.yourTurn = true;
                        bot.Strike(player);
                    }
                    if (clickedcell.Status == CellStatus.HasShipHidden)
                    {
                        bot.field.ChangeColor(clickedcell, CellStatus.ShipDamaged, Color.Red);

                        if (bot.DeadShip(clickedcell))
                            bot.CheckShips();
                    }
                }
                return;
            }
        }
    }
}