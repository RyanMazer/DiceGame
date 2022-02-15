using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using DiceGame.Source;
using DiceGame.Source.Base;
using DiceGame.Source.Logic;

namespace DiceGame.Forms
{
    enum EFormState
    {
        S_Session,
        S_Local
    }

    public partial class Main : Form
    {
        private readonly DiceEdit diceEdit;
        private DiceLogicBase logic;
        private bool selfHost; //True if hosting server
        private ServerList serverList;

        public Main()
        {
            //Initiate logic as local, can be replaced later if remote instance is needed
            logic = new DiceLogicLocal();

            //creates Dice Edit window and binds functions to Actions
            diceEdit = new DiceEdit
            {
                SaveDice = saveDice,
                Upload = Upload
            };

            InitializeComponent();
            
            //Console.WriteLine(Environment.MachineName);
        }


        private async void Main_Shown(object sender, EventArgs e)
        {
            var init = logic.InitializeAsync();

            UpdateState(EFormState.S_Local);

            await init; //Wait for dicelist to be initialized before updating DiceList ui element
            UpdateDiceList();
        }

        private async void Roll_Click(object sender, EventArgs e)
        {
            if (logic != null)
                UpdateRoll(await logic.DiceRoll()); //awaited cause DiceLogicRemote makes a server call
        }

        /// <summary>
        /// Called to update list of available dice
        /// </summary>
        private void UpdateDiceList()
        {
            var dice = logic.DiceList;

            DiceList.Items.Clear();

            if (dice == null || dice.Count == 0)
                return;

            foreach (var i in dice) DiceList.Items.Add(i.Name);

            DiceList.SelectedIndex = 1;
        }


        /// <summary>
        /// Called to update History and Result and display new Roll result
        /// </summary>
        /// <param name="roll">Output to display</param>
        public void UpdateRoll(string roll)
        {
            if (roll == string.Empty)
                return;

            //If current thread is not UI thread, create delegate and invoke on ui thread; 
            if (History.InvokeRequired)
            {
                UpdateRollCallback d = UpdateRoll;
                Invoke(d, roll);
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

        private async void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!selfHost) return;

            try
            {
                    
                //Called like this to prevent application from closing before 
                //Server is unregistered out of the server list
                Task.Run(async () => await Http.CloseSessionAsync()).Wait(); 
                //TODO Will not be called if application crashes or is forced closed. But server does need to be deleted from the database

                //If not set to null will call OnDisconnect, which will attempt to access ui elements that will now be disposed of
                logic = null;

                await SignalRServer.ShutdownServer();
            }
            catch (Exception a)
            {
                Console.WriteLine(a.Message);
                e.Cancel = true;
            }
        }

        //Callback delegates for functions called in the Server or Client thread
        //which attempt to edit elements in the UI thread
        private delegate void UpdateRollCallback(string roll);

        private delegate void UpdateUsersCallback();

        private delegate void OnDisconnectCallback();

        #region DiceEdit

        private void Settings_Click(object sender, EventArgs e)
        {
            if (logic.LoadingState != ELoadingState.S_Loaded)
            {
                if (logic.LoadingState != ELoadingState.S_Failed)
                    MessageBox.Show("Please wait while the dicelist is being loaded", "Please Wait",
                        MessageBoxButtons.OK);
            }
            else
            {
                diceEdit.Show();
                diceEdit.LoadDiceList(logic.DiceList);
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
            if (ServerName.Text == string.Empty)
            {
                MessageBox.Show(@"Set a server name please!", @"Error", MessageBoxButtons.OK);
                return;
            }

            selfHost = true;

            //Initializes the server, registers it in the database and joins it after creation
            await SignalRServer.InitializeServerAsync();

            await JoinSessionAsync(await Http.GetIp());

            await Http.RegisterSession(ServerName.Text, await Http.GetIp());
        }

        /// <summary>
        /// Called to update UI and disable/enable buttons depending on if a remote session is active or not
        /// </summary>
        /// <param name="aFormState">State to update to</param>
        private void UpdateState(EFormState aFormState)
        {
            var menuState = false;

            menuState = !(aFormState is EFormState.S_Local); 

            SessionOpen.Enabled = !menuState;
            Join.Enabled = !menuState;
            Kick.Enabled = menuState;
            End.Enabled = menuState;
            diceEdit.Enabled = !menuState;
        }

        private void Join_Click(object sender, EventArgs e)
        {
            if (NameInput.Text == string.Empty)
            {
                MessageBox.Show(@"Set a user name please!", @"Error", MessageBoxButtons.OK);
                return;
            }

            serverList = new ServerList(this);
            serverList.Show();
        }



        /// <summary>
        /// Called to initiate a server connection on the provided IP
        /// </summary>
        /// <param name="ip">Ip to connect to</param>
        /// <returns>Task</returns>
        public async Task JoinSessionAsync(string ip)
        {
            UpdateState(EFormState.S_Session);

            logic.CloseLogic();

            //Replace the current DiceLogicLocal instance with a Remote instance
            //Needed to enable server calls and responses
            var remote = new DiceLogicRemote
            {
                UpdateDice = UpdateRoll,
                UpdateUsers = UpdateUsers,
                Disconnect = OnDisconnect
            };

            logic = remote;

            remote.UserName = NameInput.Text;
            await remote.Connect(ip);

            UpdateDiceList();
        }


        /// <summary>
        /// Called to update player list
        /// </summary>
        private void UpdateUsers()
        {
            if (logic is DiceLogicRemote remote)
            {
                //If current thread is not UI thread, create delegate and invoke on ui thread; 
                if (Players.InvokeRequired)
                {
                    var d = new UpdateUsersCallback(UpdateUsers);
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

        private async void Kick_Click(object sender, EventArgs e)
        {
            if (Players.SelectedIndex == -1) MessageBox.Show(@"Select a user please", @"Error!", MessageBoxButtons.OK);

            if (logic is DiceLogicRemote d)
            {
                var result = false;
                foreach (var u in d.Users)
                    if (u.Value == Players.SelectedItem.ToString())
                    {
                        result = await d.Kick(u.Key);
                        break;
                    }

                if (!result)
                    MessageBox.Show(@"Kick failed. Either player is not here, or you're not admin.", @"Error!",
                        MessageBoxButtons.OK);
                else
                    MessageBox.Show(@"Kick success", @"Success", MessageBoxButtons.OK);
            }
        }

        private async void End_Click(object sender, EventArgs e)
        {
            if (selfHost)
                _ = SignalRServer.ShutdownServer();

            selfHost = false;
            UpdateState(EFormState.S_Local);

            //Replace Remote logic with Local logic to handle dice rolling locally again
            logic.CloseLogic();
            logic = new DiceLogicLocal();
            await logic.InitializeAsync();
        }

        /// <summary>
        /// Called to disconnect a player after Server shutdown or user being kicked
        /// </summary>
        private void OnDisconnect()
        {
            //If current thread is not UI thread, create delegate and invoke on ui thread; 
            if (Players.InvokeRequired)
            {
                var d = new OnDisconnectCallback(OnDisconnect);
                Invoke(d, new object[] { });
            }
            else
            {
                UpdateState(EFormState.S_Local);
                logic.CloseLogic();
                logic = new DiceLogicLocal();
                Players.Items.Clear();

                logic.InitializeAsync();

                MessageBox.Show(@"Server closed or user kicked", @"Notice!", MessageBoxButtons.OK);
            }
        }

        #endregion
    }
}