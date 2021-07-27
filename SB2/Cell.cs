using System;
using System.Drawing;
using System.Windows.Forms;

namespace SB2
{
    public class Cell: Button
    {
        public CellStatus Status { get; set; }
        public Coordinates Coordinates { get; set; }
        public int NumberOfSet { get; set; }
        public Cell(int x, int y, Point location, Size size)
        {
            Coordinates = new Coordinates(x, y);
            Location = location;
            Size = size;
            Status = CellStatus.Empty;
            BackColor = Button.DefaultBackColor;
        }
    }
    public enum CellStatus
    {
        Empty,
        EmptyStriked,
        HasShip,
        HasShipHidden,
        ShipDamaged,
        Blocked
    }
}
