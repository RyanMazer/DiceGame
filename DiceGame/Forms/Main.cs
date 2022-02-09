using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DiceGame.Source; 

namespace DiceGame.Forms
{
    public partial class Main : Form
    {
        private Core core = null;
        private DiceEdit diceEdit;

        public Main(Core a_core)
        {
            core = a_core;
            diceEdit = new DiceEdit();
            diceEdit.bindSave(saveDice);
            diceEdit.UploadAction(UploadAsync); 

            InitializeComponent();
            Console.WriteLine(Environment.MachineName);

        }

        //This function is Async because the messagebox locks the main thread
        //Which causes a crash when the diceloading actually finishes
        //By making this function async it prevents locking the main thread and lets diceloading finish cleanly
        private async void Settings_Click(object sender, EventArgs e)
        {
            if (core.ELoadingState != ELoadingState.S_Loaded)
            {
                if (core.ELoadingState != ELoadingState.S_Failed)
                    MessageBox.Show("Please wait while the dicelist is being loaded", "Please Wait", MessageBoxButtons.OK);
            }
            else
            {
                diceEdit.Show();
                diceEdit.loadDiceList(core.DiceList); 
            }
        }

        private async void SessionOpen_Click(object sender, EventArgs e)
        {
            await HTTP.RegisterSession("TestingSession");
        }

        private void Kick_Click(object sender, EventArgs e)
        {

        }

        private async void End_Click(object sender, EventArgs e)
        {
            await HTTP.CloseSessionAsync("TestingSession");
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

        public void saveDice(List<Dice> a_diceList)
        {
            core.saveDice(a_diceList);
        }

        public async void UploadAsync()
        {
            core.StartUploadAsync(); 
        }
    }
}
