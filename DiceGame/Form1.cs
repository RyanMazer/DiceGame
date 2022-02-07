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

            if(output == null)
                return;

            History.Items.Add(output);
            Result.Text = output;
        }

        private void DiceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            core.selectDice(DiceList.SelectedItem.ToString()); 
        }

        private async void Main_Shown(object sender, EventArgs e)
        {
            await core.Intialize();

            var dice = core.DiceList;

            foreach (var i in dice)
            {
                DiceList.Items.Add(i.getName());
            }

            DiceList.SelectedIndex = 1;
        }
    }
}
