using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiceGame
{
    public partial class Main : Form
    {
        Core core = null; 
        public Main(Core a_core)
        {
            core = a_core;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var dice = core.UpdateDiceList();

            foreach (var i in dice)
            {
                DiceList.Items.Add(i.getName());
            }
        }

        private void Settings_Click(object sender, EventArgs e)
        {

        }

        private void SessionOpen_Click(object sender, EventArgs e)
        {

        }

        private void Kick_Click(object sender, EventArgs e)
        {

        }

        private void End_Click(object sender, EventArgs e)
        {

        }

        private void Roll_Click(object sender, EventArgs e)
        {
            string output = core.RollDice();

            if(Result.Text != String.Empty)
                History.Items.Add(Result.Text);
            
            Result.Text = output;
        }

        private void DiceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            core.selectDice(DiceList.SelectedItem.ToString()); 
        }
    }
}
