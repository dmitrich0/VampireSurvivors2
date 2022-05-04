using System;
using System.Windows.Forms;
using VampireSurvivors2.View;

namespace VampireSurvivors2
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GameMenu());
        }
    }
}