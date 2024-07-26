using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Project
{
    internal class Settings
    {
        private static readonly string[] AllSettings = { "Change Title", "Change Background Color", "Change Text Color", "Reset To Defaults", "Main Menu" };
        private static readonly string[] ColorsList = { "Black", "White", "Red", "Yellow", "Green", "Blue", "Cyan", "Gray", "Magenta" };
        public static void DisplaySettingsMenu()
        {
            Console.Clear();
            Console.WriteLine("\u001b[33m<---------------[Settings Menu]--------------->\u001b[0m");
            Console.Write("\n");
            Console.WriteLine("In this menu you can customize many things like colors and titles and other features..\n");
            Console.WriteLine("<----------[Actions]---------->");
            for (int i = 0; i < AllSettings.Length; i++)
            {
                Console.WriteLine($"\u001b[35;1m{i + 1}-\u001b[0m {AllSettings[i]}");
            }
            switch (Program.AskForChoice())
            {
                case 1: ChangeConsoleTitle(); break;
                case 2: ColorWidget(); break;
                case 3: RandomTextColor(); break;
                case 4: ResetToDefualts(); break;
                case 5: Program.DrawMainMenu(); break;
                default: WrongInput(); break;
            }
        }

        private static void WrongInput()
        {
            Random random = new Random();
            Console.Write("--> \u001b[31;1m" + Program.ErrorMessages[random.Next(0, Program.ErrorMessages.Length)] + "\u001b[0m");
            Task.Delay(1500).Wait();
            DisplaySettingsMenu();
        }

        private static void ChangeConsoleTitle()
        {
            Console.Write("\u001b[35;1m-->\u001b[0m Enter a new title: ");
            string title = Console.ReadLine();
            Console.Title = title;
            DisplaySettingsMenu();
        }

        private static void ColorWidget()
        {
            Console.Clear();
            Console.WriteLine("\u001b[39m<---------------[Color Menu]--------------->\u001b[0m");
            Console.WriteLine("\nSelect an color and enter the index to apply the new changes..\n");
            for (int i = 0; i < ColorsList.Length; i++)
            {
                Console.WriteLine($"[ \u001b[35;1m{i}\u001b[0m ] {ColorsList[i]}");
            }
            Console.WriteLine("\n<----------[Actions]---------->");
            Console.WriteLine("\u001b[35;1m1-\u001b[0m Give Color Index");
            Console.WriteLine("\u001b[35;1m2-\u001b[0m Main Menu");
            switch (Program.AskForChoice())
            {
                case 1: SpecificColor(); DisplaySettingsMenu(); break;
                case 2: Program.DrawMainMenu(); break;
                default: WrongInput(); break;
            }
        }

        private static void ResetToDefualts()
        {
            Console.Title = "ATM";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            DisplaySettingsMenu();
        }

        private static void RandomTextColor()
        {
            Random random = new Random();
            int inx = random.Next(0, 11);
            switch (inx)
            {
                case 0: Console.ForegroundColor = ConsoleColor.Green; Console.ResetColor(); break;
                case 1: Console.ForegroundColor = ConsoleColor.Red; break;
                case 2: Console.ForegroundColor = ConsoleColor.Yellow; break;
                case 3: Console.ForegroundColor = ConsoleColor.DarkGreen; break;
                case 4: Console.ForegroundColor = ConsoleColor.Blue; break;
                case 5: Console.ForegroundColor = ConsoleColor.Cyan; break;
                case 6: Console.ForegroundColor = ConsoleColor.Gray; break;
                case 7: Console.ForegroundColor = ConsoleColor.Magenta; break;
                case 8: Console.ForegroundColor = ConsoleColor.DarkCyan; break;
                case 9: Console.ForegroundColor = ConsoleColor.DarkGray; break;
                case 10: Console.ForegroundColor = ConsoleColor.DarkYellow; break;
                case 11: Console.ForegroundColor = ConsoleColor.DarkRed; break;
            }
            DisplaySettingsMenu();
        }

        private static void SpecificColor()
        {
            Console.Write("\u001b[35;1m-->\u001b[0m Enter Desired Color: ");
            string color = Console.ReadLine();
            bool success = int.TryParse(color, out int value);
            if (success)
            {
                switch (value)
                {
                    case 0: Console.BackgroundColor = ConsoleColor.Black; break;
                    case 1: Console.BackgroundColor = ConsoleColor.White; break;
                    case 2: Console.BackgroundColor = ConsoleColor.Red; break;
                    case 3: Console.BackgroundColor = ConsoleColor.Yellow; break;
                    case 4: Console.BackgroundColor = ConsoleColor.Green; break;
                    case 5: Console.BackgroundColor = ConsoleColor.Blue; break;
                    case 6: Console.BackgroundColor = ConsoleColor.Cyan; break;
                    case 7: Console.BackgroundColor = ConsoleColor.Gray; break;
                    case 8: Console.BackgroundColor = ConsoleColor.Magenta; break;
                    //case 9: Console.BackgroundColor = ConsoleColor.DarkGray; break;
                    //case 10: Console.BackgroundColor = ConsoleColor.DarkYellow; break;
                    //case 11: Console.BackgroundColor = ConsoleColor.DarkRed; break;
                    default: WrongInput(); break;
                }
            }
            else
            {
                WrongInput();
            }
        }
    }
}
