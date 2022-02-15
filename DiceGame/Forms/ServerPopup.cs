using System;
using System.Windows.Forms;

namespace DiceGame.Forms
{
    public partial class ServerPopup : Form
    {
        public ServerPopup()
        {
            InitializeComponent();
        }

        private void ServerPopup_Load(object sender, EventArgs e)
        {
            ControlBox = false;
        }

        private void ServerPopup_Leave(object sender, EventArgs e)
        {
            Activate();
        }
    }
}