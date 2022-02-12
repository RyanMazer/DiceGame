using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Source
{
    public struct rollResult
    {
        public string name;
        public string side;
    }

    public class Dice
    {
        private string[] faces;
        public string[] Faces { get { return faces; } set { faces = value; } }
        private string name;
        public string Name { get { return name; } set { name = value; } }

        public Dice(string[] a_dice, string a_name)
        {
            faces = a_dice;
            name = a_name;
        }
        public Dice()
        {

        }

        public string[] getFaces()
        {
            return faces;
        }

        public string getName()
        {
            return name;
        }

        public rollResult roll()
        {
            rollResult roll = new rollResult();

            Random random = new Random();
            int result = random.Next(faces.Length);

            roll.name = name;
            roll.side = faces[result];

            return roll;
        }

        public override bool Equals(object obj)
        {
            var dice = obj as Dice;
            
            if (dice == null) return false;

            if (name != dice.getName())
                return false;

            //foreach (string s in faces)
            //{
            //    if (!dice.getFaces().Contains(s))
            //        return false;
            //}

            return true; 
        }

        public override int GetHashCode()
        {
            return name.GetHashCode();
        }
    }

    public class DiceComparer : IEqualityComparer<Dice>
    {
        public int GetHashCode(Dice co)
        {
            if (co == null)
            {
                return 0;
            }
            return co.getName().GetHashCode();
        }

        public bool Equals(Dice x1, Dice x2)
        {
            if (object.ReferenceEquals(x1, x2))
            {
                return true;
            }
            if (object.ReferenceEquals(x1, null) ||
                object.ReferenceEquals(x2, null))
            {
                return false;
            }

            if (x1.getName() != x2.getName())
                return false;

            foreach (string s in x1.getFaces())
            {
                if (!x2.getFaces().Contains(s))
                    return false;
            }

            return true;
        }
    }
}