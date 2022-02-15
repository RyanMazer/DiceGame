using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DiceGame.Source;

namespace DiceGame.Forms
{
    /// <summary>
    /// Form to edit current dicelist to save locally or upload to the database 
    /// </summary>
    public partial class DiceEdit : Form
    {
        private bool saved;

        private Action<List<Dice>> saveDice;
        public Action<List<Dice>> SaveDice { set { saveDice = value; } }

        private Action upload;
        public Action Upload { set { upload = value; } }

        public DiceEdit()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads currently available dice into the listbox
        /// </summary>
        /// <param name="aDice"></param>
        public void LoadDiceList(List<Dice> aDice)
        {
            DiceList.Nodes.Clear();

            foreach (var dice in aDice)
            {
                var node = DiceList.Nodes.Add(dice.Name);
                foreach (var s in dice.Faces)
                    node.Nodes.Add(s);
            }
        }

        /// <summary>
        /// Saves the current edits to the dicelist if there are any
        /// </summary>
        private void Save()
        {
            if (saveDice != null)
            {
                var diceList = new List<Dice>();

                //Converts the nodes and their children nodes into Dice objects
                foreach (TreeNode node in DiceList.Nodes)
                {
                    var name = node.Text;

                    var face = new List<string>();
                    foreach (TreeNode s in node.Nodes) face.Add(s.Text);
                    var dice = new Dice(face.ToArray(), name);
                    diceList.Add(dice);
                }

                saveDice(diceList);
                saved = true;
            }
            else
            {
                MessageBox.Show(@"Something went horribly wrong", @"Error", MessageBoxButtons.OK);
                Application.Exit();
            }
        }

        private void DiceList_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            saved = false;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (saved == false)
                Save();
        }

        private void Upload_Click(object sender, EventArgs e)
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
            Hide();
        }

        private void AddDice(object sender, EventArgs e)
        {
            if (DiceList.SelectedNode == null)
                DiceList.Nodes.Add("NewDice");
            else
                DiceList.SelectedNode.Nodes.Add("New Face");
        }

        private void DiceMouseClick(object sender, MouseEventArgs e)
        {
            //Using current mouse location within listbox checks if mouse is outside specified bounds
            //If true then selected node is set to null
            var totalHeight = DiceList.ItemHeight * DiceList.Nodes.Count;

            if (!(e.X < 100 && e.X >= 0) || !(e.Y < totalHeight && e.Y >= 0))
                DiceList.SelectedNode = null;
        }
    }
}