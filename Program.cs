﻿using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace dndRandoGen
{
    public static class Program
    {
        static Character NewToon = new();
        
        public static void Main(string[] args)
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }
        private static bool MainMenu()
        {
            //Console.Clear();
            Console.WriteLine("Welcome Adventurer!");
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1) Create New Character(s)");
            Console.WriteLine("2) View Saved Character(s)");
            Console.WriteLine("3) Exit");
            Console.Write("Select an option:");
            try
            {
                string input = Console.ReadLine();
                int switchCall = int.Parse(input);

                switch (switchCall)
                {
                    case 1:
                        Console.Clear();
                        NewToon.NewChar();
                        return true;
                    case 2:
                        Console.Clear();
                        //SaveSheet (); Json file
                        return true;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("It's dangerous to go alone. Thanks for finding your new party member(s) with us!\n");
                        return false;
                    default:
                        Console.Clear();
                        Console.WriteLine("Please choose carefully! The option you chose isn't valid. Press any key to return to the main menu!\n");
                        Console.ReadKey();
                        return true;
                }
            }
            catch (FormatException)
            {
                throw;
            }

        }
    }
}
