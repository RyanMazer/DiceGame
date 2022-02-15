using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DiceGame.Source;

namespace DiceGame.Forms
{
    /// <summary>
    /// Form to display currently active servers registered in the Database
    /// </summary>
    public partial class ServerList : Form
    {
        private readonly Main main;
        private ServerPopup popup;
        private readonly Dictionary<string, string> servers;

        public ServerList(Main aMain)
        {
            main = aMain;
            servers = new Dictionary<string, string>();

            InitializeComponent();
        }

        private async void Join_Click(object sender, EventArgs e)
        {
            if (Sessions.SelectedIndex == -1)
                return;

            var selected = Sessions.SelectedItem.ToString();

            popup = new ServerPopup();
            popup.Show();

            //Join server using IP of the current selected object in Session listbox
            await main.JoinSessionAsync(servers[selected]);

            popup.Hide();
            Close();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void OnShow(object sender, EventArgs e)
        {
            //Retrieves and displays currently registered sessions
            var ses = await Http.GetSessionListAsync();

            foreach (var s in ses)
            {
                servers.Add(s.SessionName, s.SessionIp);
                Sessions.Items.Add(s.SessionName);
            }
        }
    }
}