using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    public struct rollResult
    {
        public string name;
        public string side;
    }

    public class Dice
    {
        private string[] dice;
        private string name;

        public Dice(string[] a_dice,string a_name)
        {
            dice = a_dice;
            name = a_name; 
        }

        public string[] getDice()
        {
            return dice;
        }

        public string getName()
        {
            return name;   
        }

        public rollResult roll()
        {
            rollResult roll = new rollResult();

            Random random = new Random();
            int result = random.Next(dice.Length);

            roll.name = name;
            roll.side = dice[result];

            return roll; 
        }
    }
}
