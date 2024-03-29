﻿using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace dndRandoGen
{
    public class Base
    {
        public List<RandomCharacter> characters { get; set; }
    }
    class SavedCharacters
    {
        private static string jsonLoc = "data\\SavedCharacters.json";
        private static string logLoc = "data\\log.txt";


        public void Display()
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = SaveMenu();
            }
        }
        public bool SaveMenu()
        {
            Console.WriteLine("\n From here you can either:");
            Console.WriteLine("1) Save current character.");
            Console.WriteLine("2) Delete saved characters.");
            Console.WriteLine("3) Return to main menu.");
            Console.Write(" Now, which do you choose... : ");

            try
            {
                string input = Console.ReadLine();
                int switchCall = int.Parse(input);
                switch (switchCall)
                {
                    case 1:
                        Save();
                        Display();
                        return true;
                    case 2:
                        SeeList();
                        TakeOut();
                        Display();
                        return true;
                    case 3:
                        Console.Clear();
                        return false;
                    default:
                        Console.WriteLine(" Please choose carefully! The option you chose isn't valid. Press any key to return to the main menu!\n");
                        Console.ReadKey();
                        return true;
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine(" FORMAT ERROR: " + ex);
                File.AppendAllText(logLoc, Environment.NewLine + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + ": FPRMAT ERROR: " + ex);
                return true;
            }
        }

        public void Save()
        {
            Console.Write("\n What is the name of your character? ");
            string name = Console.ReadLine();
            if (name != null)
            {
                AddToJson(name);
                Console.WriteLine("\n Thank you! Now returning you to the main menu.");
                Program.MainMenu();
            }
        }

        public void TakeOut()
        {
            Console.WriteLine("\n Please use the Id# of the character you would like to remove.");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("\n Are you sure you want to remove #{0}? \n y/n: ", id);
            string input = Console.ReadLine();
            if (input == "y")
            {
                RemoveFromJson(id);
            }
            Console.WriteLine("\n Thank you! Now returning you to the main menu.");
            Program.MainMenu();
        }

        //updates json file

        public static void SeeList()
        {
            var json = JsonConvert.DeserializeObject<Base>(File.ReadAllText(jsonLoc));
            try
            {
                if (json != null)
                {
                    if (json.characters != null && json.characters.Count > 0)
                    {
                        foreach (var c in json.characters)
                        {
                            Console.WriteLine("\n");
                            foreach (var prop in c.GetType().GetProperties())
                            {
                                Console.WriteLine("    {0}: {1}", prop.Name, prop.GetValue(c, null));
                            }
                        }
                    }
                    else 
                    {
                        Console.WriteLine(" There are currently no characters in the save file.");
                    }
                }
                else
                {
                    Console.WriteLine(" There are currently no files for a saved character.");
                }
            }
            catch (FileNotFoundException ex)    
            {
                Console.WriteLine(" FILE NOT FOUND: " + ex);
                File.AppendAllText(logLoc, Environment.NewLine + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + ": FILE NOT FOUND: " + ex);
            }
            catch (JsonException ex)    
            {
                Console.WriteLine(" INVALID JSON: " + ex);
                File.AppendAllText(logLoc, Environment.NewLine + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + ": INVALID JSON: " + ex);
            }
            catch (Exception ex)    
            {
                Console.WriteLine(" ERROR: " + ex);
                File.AppendAllText(logLoc, Environment.NewLine + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + ": ERROR: " + ex);
            }
        }
        public static void AddToJson(string name)
        {
            var json = JsonConvert.DeserializeObject<Base>(File.ReadAllText(jsonLoc));
            try 
            {
                int highestID = 0;
                foreach (RandomCharacter c in json.characters)
                {
                    if (c.Id > highestID)
                    {
                        highestID = c.Id;
                    }
                    else 
                    {
                        break;
                    }
                }

                if (json.characters == null)
                {
                    json.characters = new List<RandomCharacter>();
                }
                
                json.characters.Add(new RandomCharacter()
                {
                    Id = highestID + 1,
                    Name = name,
                    Race = Generator.randChar.Race,
                    Role = Generator.randChar.Role,
                    Gender = Generator.randChar.Gender
                });

                string json1 = JsonConvert.SerializeObject(json, Formatting.Indented);
                File.WriteAllText(jsonLoc, json1);
                File.AppendAllText(logLoc, Environment.NewLine + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + ": Item added to Characters");
            }

            catch (FileNotFoundException ex)
            {
                Console.WriteLine(" FILE NOT FOUND: " + ex);
                File.AppendAllText(logLoc, Environment.NewLine + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + ": FILE NOT FOUND: " + ex);
            }
            catch (JsonException ex)
            {
                Console.WriteLine(" INVALID JSON: " + ex);
                File.AppendAllText(logLoc, Environment.NewLine + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + ": INVALID JSON: " + ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(" ERROR: " + ex);
                File.AppendAllText(logLoc, Environment.NewLine + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + ": ERROR: " + ex);
            }
        }

        public static void RemoveFromJson(int num)
        {
            int id = num;
            try
            {
                var json = JsonConvert.DeserializeObject<Base>(File.ReadAllText(jsonLoc));
                string json1 = "";

                foreach (var c in json.characters)
                {
                    if (c.Id == id)
                    {
                        json.characters.Remove(c);
                        Console.WriteLine(" Character Removed");
                        break;
                    }
                }

                json1 = JsonConvert.SerializeObject(json, Formatting.Indented);
                File.WriteAllText(jsonLoc, json1);
                File.AppendAllText(logLoc, Environment.NewLine + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + ": Character removed.");
                return;
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(" FILE NOT FOUND: " + ex);
                File.AppendAllText(logLoc, Environment.NewLine + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + ": FILE NOT FOUND: " + ex);
            }
            catch (JsonException ex)
            {
                Console.WriteLine(" INVALID JSON: " + ex);
                File.AppendAllText(logLoc, Environment.NewLine + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + ": INVALID JSON: " + ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(" ERROR: " + ex);
                File.AppendAllText(logLoc, Environment.NewLine + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + ": ERROR: " + ex);
            }
        }
    }
}

