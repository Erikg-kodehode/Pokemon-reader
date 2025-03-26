using System;
using System.Windows.Forms;

namespace PokemonReader.WinForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new StartupForm());
        }
    }
}
