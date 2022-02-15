using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace DiceGame.Source
{
    public static class Statics
    {
        /// <summary>
        /// Extract a list of Dice objects from a list of DiceJson objects
        /// </summary>
        /// <param name="ReqResult">List of DiceJson objects</param>
        /// <returns>List of Dice object</returns>
        public static List<Dice> GetDiceList(List<DiceJson> ReqResult)
        {
            var diceList = new List<Dice>();
            foreach (var dice in ReqResult)
            {
                var faces = dice.DiceFace.Split(',');

                var newDice = new Dice(faces, dice.DiceName);
                diceList.Add(newDice);
            }

            return diceList;
        }

        /// <summary>
        /// Gets your devices local ip 
        /// </summary>
        /// <returns>Returns local ip</returns>
        /// <exception cref="Exception"></exception>
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            throw new Exception("Local IP Address Not Found!");
        }
    }
}