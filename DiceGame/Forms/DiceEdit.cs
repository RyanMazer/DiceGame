using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using DiceGame.Source;

namespace DiceGame.Forms
{
    public partial class DiceEdit : Form
    {
        private Action<List<Dice>> saveDice;
        public void bindSave(Action<List<Dice>> func) { saveDice = func; }

        private Action upload;
        public void UploadAction(Action func) { upload = func; }

        bool saved = false;

        public bool diceLoaded { get { return DiceList.Nodes.Count > 0; } }

        public DiceEdit()
        {
            InitializeComponent();
        }

        public void loadDiceList(List<Dice> a_dice)
        {
            DiceList.Nodes.Clear();

            foreach (Dice dice in a_dice)
            {
                TreeNode node = DiceList.Nodes.Add(dice.getName());
                foreach (string s in dice.getFaces())
                    node.Nodes.Add(s);
            }
        }

        private void Save()
        {
            if (saveDice != null)
            {
                List<Dice> diceList = new List<Dice>();

                foreach (TreeNode node in DiceList.Nodes)
                {
                    string name = node.Text;

                    List<string> face = new List<string>();
                    foreach (TreeNode s in node.Nodes)
                    {
                        face.Add(s.Text);
                    }
                    Dice dice = new Dice(face.ToArray(), name);
                    diceList.Add(dice);
                }

                saveDice(diceList);
                saved = true;
            }
            else
            {
                MessageBox.Show("Something went horribly wrong", "Error", MessageBoxButtons.OK);
                Application.Exit();
            }
        }

        private void DiceList_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            saved = false;
        }

        private void SaveDice(object sender, EventArgs e)
        {
            Save();
        }

        private async void Upload_Click(object sender, EventArgs e)
        {
            if (!saved)
                Save();

            upload.Invoke();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (DiceList.SelectedNode != null)
            {
                saved = false;
                DiceList.SelectedNode.Remove();
            }
        }

        private void DiceEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void MouseClick(object sender, MouseEventArgs e)
        {
            int totalHeight = DiceList.ItemHeight * DiceList.Nodes.Count;

            if (!(e.X < 100 && e.X >= 0) || !(e.Y < totalHeight && e.Y >= 0))
                DiceList.SelectedNode = null;
        }

        private void Add(object sender, EventArgs e)
        {
            if(DiceList.SelectedNode == null)
            {
                DiceList.Nodes.Add("NewDice"); 
            }
            else
            {
                DiceList.SelectedNode.Nodes.Add("New Face"); 
            }    
        }
    }
}
