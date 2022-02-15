using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiceGame.Source.Base
{
    /// <summary>
    /// Base class for Dice Logic
    /// </summary>
    public abstract class DiceLogicBase
    {
        protected Dice currentDice;
        protected List<Dice> diceList;

        //Im not 100% on this but i think Resharper did this when i cleaned up my code. 
        public List<Dice> DiceList => diceList;
        public Dice CurrentDice => currentDice;
        public ELoadingState LoadingState { get; private set; }

        /// <summary>
        /// Updates current state and logs state change. Mostly used for debugging rn
        /// </summary>
        /// <param name="a_state"></param>
        protected virtual void UpdateState(ELoadingState a_state)
        {
            LoadingState = a_state;

            Console.WriteLine("Current State: " + LoadingState);
        }

        public abstract Task InitializeAsync();
        public abstract Task<string> DiceRoll();

        /// <summary>
        /// Called to change currently selected dice. Works for both local and remote so moved to base class 
        /// </summary>
        /// <param name="aName">Name of the new dice</param>
        public virtual void SelectDice(string aName)
        {
            if (aName != null)
            {
                //Loops over all dice to compare names and selects dice when input dice found
                foreach (var d in diceList)
                    if (d.Name == aName)
                        currentDice = d;
            }
            else
            {
                MessageBox.Show(
                    @"Something went wrong. You shouldn't be able to select an empty dice! Contact Developer plz!",
                    @"Error", MessageBoxButtons.OK);
            }
        }

        public abstract void CloseLogic();
    }
}