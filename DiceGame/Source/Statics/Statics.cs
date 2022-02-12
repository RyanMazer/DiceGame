using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace DiceGame.Source
{
    public static class Statics
    {
        public static List<Dice> GetDiceList(List<DiceJson> ReqResult)
        {
            var diceList = new List<Dice>(); 
            foreach (var dice in ReqResult)
            {
                string[] faces = dice.diceFace.Split(',');

                Dice newDice = new Dice(faces, dice.diceName);
                diceList.Add(newDice);
            }

            return diceList;
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }
    }
}
