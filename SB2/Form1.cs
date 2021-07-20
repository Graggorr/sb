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
        public Form1()
        {
            InitializeComponent();
            CreateMap();
        }

        private void CreateGame(object sender, EventArgs e)
        {
            Button clickedbutton = sender as Button;
            player = new Player();
            bot = new Bot();
            SetBotShips();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
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
            clickedbutton.Click -= new EventHandler(CreateGame);
            clickedbutton.Click += new EventHandler(StartGame);

        }
        private void CreateMap()
        {
            Button startButton = new Button
            {
                Text = "Start",
                Size = new Size(60, 20),
                Location = new Point(650, 650)
            };
            Controls.Add(startButton);
            startButton.Click += new EventHandler(CreateGame);
        }
        private void StartGame(Object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (Player.NumberOfSet == 20)
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
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
            else
            {
                MessageBox.Show("There are no 10 ships");
            }
        }

        private void War()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    bot.field.map[i, j].Click += new EventHandler(PlayerStrike);
                }
            }
            BotStrike();
        }

        private void BotStrike()
        {
            Random rng = new Random();
            if (bot.yourTurn == true)
            {
                int x, y, x1;
            LoopEnd:
                x = rng.Next(0, 9);
                y = rng.Next(0, 9);
                x1 = x + 1;
                if (player.field.map[y, x].Status != CellStatus.EmptyStriked && player.field.map[y, x].Status != CellStatus.ShipDamaged)
                {
                    if (player.field.map[y, x].Status == CellStatus.HasShip)
                    {
                        player.field.ChangeColor(player.field.map[y, x], CellStatus.ShipDamaged, Color.Red);
                    RepeatShot:
                        if (!bot.DeadShip(player.field.map[y, x]) && player.field.CheckCell(x1, y) && player.field.map[y, x].Status == CellStatus.HasShip)
                        {
                            player.field.ChangeColor(player.field.map[y, x1], CellStatus.ShipDamaged, Color.Red);
                            x++;
                            x1++;
                            goto RepeatShot;
                        }
                        else
                        {
                            player.field.ChangeColor(player.field.map[y, x1], CellStatus.EmptyStriked, Color.Black);
                            bot.yourTurn = false;
                            player.yourTurn = true; 
                        }
                    }
                    else
                    {
                        player.field.ChangeColor(player.field.map[y, x], CellStatus.EmptyStriked, Color.Black);
                        bot.yourTurn = false;
                        player.yourTurn = true;
                    }
                    player.CheckShips();
                    return;
                }
                goto LoopEnd;
            }
        }

        private void PlayerStrike(object sender, EventArgs e)
        {
            Cell clickedcell = sender as Cell;
            if (player.yourTurn == true)
            {
                if (clickedcell.Status != CellStatus.EmptyStriked && clickedcell.Status != CellStatus.ShipDamaged)
                {
                    if (clickedcell.Status == CellStatus.Empty)
                    {
                        bot.field.ChangeColor(clickedcell, CellStatus.EmptyStriked, Color.Black);

                        player.yourTurn = false;
                        bot.yourTurn = true;

                        BotStrike();
                    }
                    if (clickedcell.Status == CellStatus.HasShipHidden)
                    {
                        bot.field.ChangeColor(clickedcell, CellStatus.ShipDamaged, Color.Red);

                        if (!bot.DeadShip(clickedcell))
                        {
                            MessageBox.Show("Ship is dead");
                            bot.field.BlockDeadShipCell(clickedcell);
                        }
                    }

                    bot.CheckShips();
                }
                return;
            }
        }

        private void SetPlayerShips(object sender, EventArgs e)
        {
            Cell clickedCell = sender as Cell;
            if (SingleRankButton.Checked == true)
            {
                player.SetShips(Player.SINGLEKEY, clickedCell, true);
            }
            if (DoubleRankButton.Checked == true)
            {
                player.SetShips(Player.DOUBLEKEY, clickedCell, true);
            }
            if (TripleRankButton.Checked == true)
            {
                player.SetShips(Player.TRIPLEKEY, clickedCell, true);
            }
            if (QuadroRankButton.Checked == true)
            {
                player.SetShips(Player.QUADROKEY, clickedCell, true);
            }
        }

        private void SetBotShips()
        {
            int x, y;
            Random rng = new Random();
            while (bot.Count != 10)
            {
                x = rng.Next(0, 9);
                y = rng.Next(0, 9);
                bot.SetShips(bot.field.map[y, x]);
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
    }
}
