using System;
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
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Http.Initialize();

            var mainMenu = new Main();

            Application.Run(mainMenu);
        }
    }
}