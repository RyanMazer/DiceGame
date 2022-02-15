using System;
using System.Collections.Generic;
using System.Linq;

namespace DiceGame.Source
{
    /// <summary>
    /// Used to return both Dice Name and face the dice rolled on
    /// </summary>
    public struct RollResult
    {
        public string Name;
        public string Side;
    }

    /// <summary>
    /// Class used to store dice information
    /// </summary>
    public class Dice
    {
        /// <summary>
        /// Initialize Dice using name and faces
        /// </summary>
        /// <param name="aFaces">Faces the dice has</param>
        /// <param name="aName">Dice name</param>
        public Dice(string[] aFaces, string aName)
        {
            Faces = aFaces;
            Name = aName;
        }

        public Dice()
        {
        }

        /// <summary>
        /// Faces the dice can roll
        /// </summary>
        public string[] Faces { get; set; }

        /// <summary>
        /// Dice name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Roll the dice, returning both name and face rolled
        /// </summary>
        /// <returns>Returns result in a struct</returns>
        public RollResult Roll()
        {
            var roll = new RollResult();

            var random = new Random();
            var result = random.Next(Faces.Length);

            roll.Name = Name;
            roll.Side = Faces[result];

            return roll;
        }

        /// <summary>
        /// Overrides == with a check against dice names
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var dice = obj as Dice;

            if (dice == null) return false;

            return Name == dice.Name;
        }

        /// <summary>
        /// Overrides hash code to go off name instead of going off object itself.
        /// </summary>
        /// <returns>Hashcode of the dice Name</returns>
        public override int GetHashCode() => Name.GetHashCode();
    }

    /// <summary>
    /// Allows comparing Dice not just on name but faces as well
    /// Mostly to show off another way of comparing objects
    /// Could move into the == override on dice itself
    /// </summary>
    public class DiceComparer : IEqualityComparer<Dice>
    {
        public int GetHashCode(Dice co)
        {
            return co.Name.GetHashCode();
        }

        public bool Equals(Dice x1, Dice x2)
        {
            if (ReferenceEquals(x1, x2)) return true;
            if (x1 is null || x2 is null) return false;
            if (x1.Name != x2.Name) return false;

            foreach (var s in x1.Faces)
            {
                if (!x2.Faces.Contains(s))
                    return false;
            }

            return true;
        }
    }
}