using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConsoleApplication65;

namespace Gomoku
{
    public partial class Form1 : Form
    {
        private GomokuEngine GomokuObj;
        private bool Ready = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GomokuObj = new GomokuEngine();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i] is Button && (Controls[i] as Button).Tag != null)
                {
                    (Controls[i] as Button).Text = "";
                }
            }
            GomokuObj.NewGame();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
            {
                GomokuObj.Player1 = textBox1.Text;
                GomokuObj.Player2 = textBox2.Text;

                GomokuObj.Start();
                Ready = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Ready)
            {
                MessageBox.Show("Podaj imiona graczy!");
                return;
            }
            Point p = new Point();
            p = (Point)(sender as Button).Tag;

            (sender as Button).Text = GomokuObj.Active.Type == FieldType.ftCircle ? "O" : "X";

            GomokuObj.Set(p.X, p.Y);

            if (GomokuObj.Winner)
            {
                MessageBox.Show(String.Format("Brawo dla {0}", GomokuObj.Active.Name), "Wygrana!");
                for (int i = 0; i < Controls.Count; i++)
                {
                    if (Controls[i] is Button && (Controls[i] as Button).Tag != null)
                    {
                        (Controls[i] as Button).Text = "";
                    }
                }
            }
        }

    }

}
