using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DiceGame.Forms;

namespace DiceGame.Source
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            HTTP.Initialize(); 

            Core core = new Core();
            Main MainMenu = new Main(core);

            Application.Run(MainMenu);
        }
    }
}
