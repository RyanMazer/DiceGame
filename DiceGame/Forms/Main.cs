using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DiceGame.Source;
using DiceGame.Source.Base;
using DiceGame.Source.Logic;

namespace DiceGame.Forms
{
    public partial class Main : Form
    {
        private DiceLogicBase logic = null;
        private DiceEdit diceEdit;

        delegate void UpdateRollCallback(string roll);
        delegate void UpdateUsersCallback();

        public Main()
        {
            logic = new DiceLogicLocal();

            diceEdit = new DiceEdit();
            diceEdit.bindSave(saveDice);
            diceEdit.UploadAction(Upload);

            InitializeComponent();
            Console.WriteLine(Environment.MachineName);

        }

        private async void Main_Shown(object sender, EventArgs e)
        {
            await logic.InitializeAsync();

            UpdateDiceList();
        }

        private async void Roll_Click(object sender, EventArgs e)
        {
            if (logic != null)
                UpdateRoll(await logic.DiceRoll());
        }

        private void UpdateDiceList()
        {
            var dice = logic.DiceList;

            DiceList.Items.Clear();

            if (dice == null || dice.Count == 0)
                return;

            foreach (var i in dice)
            {
                DiceList.Items.Add(i.getName());
            }

            DiceList.SelectedIndex = 1;
        }

        public void UpdateRoll(string roll)
        {
            if (roll == string.Empty)
                return;

            if (this.History.InvokeRequired)
            {
                UpdateRollCallback d = new UpdateRollCallback(UpdateRoll);
                Invoke(d, new object[] { roll });
            }
            else
            {
                History.Items.Add(roll);
                Result.Text = roll;
            }
        }

        private void DiceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            logic.SelectDice(DiceList.SelectedItem.ToString());
        }

        #region DiceEdit

        private void Settings_Click(object sender, EventArgs e)
        {
            if (logic.LoadingState != ELoadingState.S_Loaded)
            {
                if (logic.LoadingState != ELoadingState.S_Failed)
                    MessageBox.Show("Please wait while the dicelist is being loaded", "Please Wait", MessageBoxButtons.OK);
            }
            else
            {
                diceEdit.Show();
                diceEdit.loadDiceList(logic.DiceList);
            }
        }

        public void saveDice(List<Dice> a_diceList)
        {
            var localLogic = logic as DiceLogicLocal;

            if (localLogic != null)
                localLogic.SaveDice(a_diceList);
            else
                Console.Error.WriteLine("SaveDice should not be callable while DiceLogic is Remote");

            UpdateDiceList();
        }

        public void Upload()
        {
            var localLogic = logic as DiceLogicLocal;

            if (localLogic != null)
                _ = localLogic.StartUploadAsync();
            else
                Console.Error.WriteLine("UploadDice should not be callable while DiceLogic is Remote");
        }

        #endregion

        #region Sessions

        private async void SessionOpen_Click(object sender, EventArgs e)
        {
            //await HTTP.RegisterSession("TestingSession");
            await SignalRServer.InitializeServer();

            JoinSession();
        }

        private async void Join_Click(object sender, EventArgs e)
        {
            JoinSession();
            //await SignalRConnector.Connect($"http://82.73.229.223:8080");
            //await SignalRConnector.Connect($"http://{NetUtil.GetLocalIPAddress()}:8080");
        }

        private async void JoinSession()
        {
            logic.CloseLogic();

            var remote = new DiceLogicRemote();
            remote.UpdateDice = UpdateRoll;
            remote.UpdateUsers = UpdateUsers;

            logic = remote;

            remote.Name = NameInput.Text;
            await logic.InitializeAsync();

            UpdateDiceList();
        }

        void UpdateUsers()
        {
            var remote = logic as DiceLogicRemote;

            if (remote != null)
            {
                if (Players.InvokeRequired)
                {
                    UpdateUsersCallback d = new UpdateUsersCallback(UpdateUsers);
                    Invoke(d, new object[] { });
                }
                else
                {
                    Players.Items.Clear();

                    foreach (var user in remote.Users)
                        Players.Items.Add(user.Value);
                }
            }
        }

        private void Kick_Click(object sender, EventArgs e)
        {

        }

        private async void End_Click(object sender, EventArgs e)
        {
            await HTTP.CloseSessionAsync("TestingSession");
        }

        #endregion
    }
}
