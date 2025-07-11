﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensors
{
    internal static class Menu
    {
        public static void ShowMenu()
        {
            Console.ResetColor();
            Console.WriteLine("┌──────────────────────────────────────────────┐");
            Console.WriteLine("│              Investigation Menu              │");
            Console.WriteLine("├──────────────────────────────────────────────┤");
            Console.WriteLine("│ 1. Investigate Agent                         │");
            Console.WriteLine("│ 2. Manage Sensors                            │");
            Console.WriteLine("│ 3. Exit                                      │");
            Console.WriteLine("└──────────────────────────────────────────────┘");
        }

        public static void ShowVaulteMenu()
        {
            Console.ResetColor();
            Console.WriteLine("┌──────────────────────────────────────────────┐");
            Console.WriteLine("│              Sensor Vault Menu               │");
            Console.WriteLine("├──────────────────────────────────────────────┤");
            Console.WriteLine("│ 1. Create New Sensor                         │");
            Console.WriteLine("│ 2. Delete Sensor                             │");
            Console.WriteLine("│ 3. Back to Main Menu                         │");
            Console.WriteLine("└──────────────────────────────────────────────┘");
        }
    }
}
