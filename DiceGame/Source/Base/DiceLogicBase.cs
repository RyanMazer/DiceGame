using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiceGame.Source.Base
{


    public abstract class DiceLogicBase
    {
        protected List<Dice> diceList;
        public List<Dice> DiceList { get { return diceList; } }

        protected Dice currentDice; 
        public Dice CurrentDice { get { return currentDice; } }

        private ELoadingState loadingState;
        public ELoadingState LoadingState { get { return loadingState; } }

        protected virtual void UpdateState(ELoadingState a_state)
        {
            loadingState = a_state;

            Console.WriteLine("Current State: " + loadingState.ToString());
        }

        public abstract Task InitializeAsync();
        public abstract Task<string> DiceRoll();
        public virtual void SelectDice(string a_name)
        {
            if (a_name != null)
            {
                foreach (Dice d in diceList)
                    if (d.getName() == a_name)
                    {
                        currentDice = d;
                    }
            }
            else
                MessageBox.Show("Something went wrong. You shouldnt be able to select an empty dice! Contact Developer plz!", "Error", MessageBoxButtons.OK);
        }

        public abstract void CloseLogic();
    }
}
