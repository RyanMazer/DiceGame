using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiceGame
{
    public class Core
    {
        private List<Dice> dice = new List<Dice>();
        private Dice currentDice = null;

        public Core()
        {
            string[] sides = { "1", "2", "3", "4", "5", "6" };
            Dice newdice = new Dice(sides, "d6");
            dice.Add(newdice);
        }

        public List<Dice> UpdateDiceList()
        {
            return dice;
        }

        public void selectDice(string diceName)
        {
            if (diceName != null)
                foreach (Dice d in dice)
                    if (d.getName() == diceName)
                        currentDice = d;
                    else
                        MessageBox.Show("Something went wrong. Contact Developer!", "Error", MessageBoxButtons.OK);
            else
                MessageBox.Show("Something went wrong. You shouldnt be able to select an empty dice! Contact Developer plz!", "Error", MessageBoxButtons.OK);
        }

        public void Intialize()
        {

        }

        public string RollDice()
        {
            string output = "";

            if (currentDice != null)
            {
                rollResult result = currentDice.roll();
                output = "You rolled " + result.side + " on a" + result.name;
            }
            else
                MessageBox.Show("Select a dice to roll", "Error", MessageBoxButtons.OK);

            return output;
        }
    }

}
