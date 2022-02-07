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
        private List<Dice> diceList;
        public List<Dice> DiceList { get { return diceList; } }

        private Dice currentDice = null;
        public Dice CurrentDice { get { return currentDice; } }
        private Loading loading;

        public Core()
        {
            loading = new Loading();
            diceList = new List<Dice>();
        }

        public void selectDice(string diceName)
        {
            if (diceName != null)
            {
                foreach (Dice d in diceList)
                    if (d.getName() == diceName)
                    {
                        currentDice = d;
                    }
            }
            else
                MessageBox.Show("Something went wrong. You shouldnt be able to select an empty dice! Contact Developer plz!", "Error", MessageBoxButtons.OK);
        }

        public async Task Intialize()
        {
            var ReqResult = await HTTP.getDiceListAsync();

            foreach (var dice in ReqResult)
            {
                string[] faces = dice.diceFace.Split(',');

                Dice newDice = new Dice(faces, dice.diceName);
                diceList.Add(newDice);
            }

            await HTTP.UpdateDiceListAsync();
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
