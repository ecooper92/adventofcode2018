using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdventOfCode2018.Winforms
{
    public partial class Form1 : Form
    {
        private (double X, double Y, double VX, double VY)[] _lines;
        private double _minX;
        private double _minY;
        private double _maxX;
        private double _maxY;
        private double speed = 50f;
        private double size = 4;

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            _lines = File.ReadAllLines(@"input.txt")
                .Select(l => Regex.Match(l, "position=<([ |-]*[0-9]+), ([ |-]*[0-9]+)> velocity=<([ |-]*[0-9]+), ([ |-]*[0-9]+)>", RegexOptions.Compiled))
                .Select(m => (X: double.Parse(m.Groups[1].Value), Y: double.Parse(m.Groups[2].Value), VX: double.Parse(m.Groups[3].Value), VY: double.Parse(m.Groups[4].Value)))
                .ToArray();
            //Advance(10500);
            Advance(10942);

            base.OnLoad(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            _minX = _lines.Min(l => l.X);
            _minY = _lines.Min(l => l.Y);
            _maxX = _lines.Max(l => l.X);
            _maxY = _lines.Max(l => l.Y);

            double factorX = (double)(_maxX - _minX) / (ClientRectangle.Width - 10);
            double factorY = (double)(_maxY - _minY) / (ClientRectangle.Height - 10);

            for (int j = 0; j < _lines.Length; j++)
            {
                e.Graphics.FillRectangle(Brushes.Red, (float)(((_lines[j].X - _minX)) / factorX), (float)(((_lines[j].Y - _minY)) / factorY), (float)size, (float)size);
            }

            base.OnPaint(e);
        }

        private void Advance(int count)
        {
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < _lines.Length; j++)
                {
                    _lines[j].X += _lines[j].VX;
                    _lines[j].Y += _lines[j].VY;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Advance(1);
            Invalidate();
        }
    }
}
