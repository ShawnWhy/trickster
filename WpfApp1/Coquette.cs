﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;
using WpfApp1;

namespace WpfApp1 { 
    // Descendant class Coquette
    public class Coquette : Character
    {
        public Coquette(string name, decimal money, int positionX, int positionY, int hitPoints, ObservableCollection<Character> characters, TextBox narrator, PlayerCharacter playercharacter, int characterIndex, Canvas canvas, ListBox listBox)
        : base(name, money, positionX, positionY, hitPoints, characters, narrator, playercharacter, characterIndex, canvas, listBox) { }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Coquette: {Name}, Money: {Money}, Position: ({PositionX}, {PositionY}), Hit Points: {HitPoints}");
        }
        public void Provoke(Character character)
        {
            string narratorString = $"{Name} pokes {character.Name}";
            Narrator.Text = narratorString;
        }

        public void Respond()
        {
            if (this.Characters.Count == 0)
            {
                Console.WriteLine("No characters to provoke.");
                return;
            }

            //find the closest character in the character list
            //create a new list of characters where the current character is abscent by name
            ObservableCollection<Character> characters2 = new ObservableCollection<Character>(this.Characters.Where(c => c.Name != this.Name));
            if (characters2.Count == 0)
            {
                Narrator.Text = "No characters to provoke.";
                return;
            }
            Character character = characters2.OrderBy(c => Math.Abs(c.PositionX - PositionX) + Math.Abs(c.PositionY - PositionY)).First();
            //switch based on the character's class
            switch (character.GetType().Name)
            {
                case "Trickster":
                    provokeTrickster((Trickster)character);
                    break;
                case "Authoritarian":
                    provokeAuthoritarian((Authoritarian)character);
                    break;
                case "Captain":
                    provokeCaptain((Captain)character);
                    break;
                case "Coquette":
                    provokeCoquette((Coquette)character);
                    break;
                case "Innocent":
                    provokeInnocent((Innocent)character);
                    break;
                case "Precious":
                    provokePrecious((Precious)character);
                    break;
                default:
                    Console.WriteLine("Character not found.");
                    break;
            }

            // Rest of the method code...
        }
        public void respondTrickster(Trickster trickster)
        {
            double healhtLost = trickster.HitPoints * 0.5;
            //round to integer
            trickster.HitPoints -= (int)healhtLost;
            Narrator.Text += $"{trickster.Name} the trickster responds";
        }

        public void respondAuthoritarian(Authoritarian authoritarian)
        {

            Narrator.Text += $"{Name} responds to {authoritarian.Name} the authoritarian";

        }

        public void respondCaptain(Captain captain)
        {
            if (HitPoints <= 0)
            {
                Narrator.Text += $"\n{Name} the coquette dies of cringe";
            }
            else
            {
                if (captain.Money >= 100)
                {
                    Narrator.Text += $"\n{captain.Name} is a passable romantic partner {Name} take 50 pending money from him and heals him for 10 points";
                    captain.Money -= 50;
                    Money += 50;
        }
                else
                {
                    Narrator.Text += $"\n{captain.Name} does not have enough money and is too much of a looser t0 become a passable love interest.  She slaps him for 20 points and and he inflicts 30 points of shame on him self";
                    captain.HitPoints -= 30;
                    captain.HitPoints -= 20;
                    if(captain.HitPoints <= 0)
                    {
                        CharacterDeath(captain, PlayerCharacter);
                        Narrator.Text += $"\n{captain.Name} the captain dies, embroiled in the drama of love and death, now by humility, now by jelousy opressed";
                    }
                }
            }
            updateCanvasandCharacterList();
        }

        public void respondCoquette(Coquette coquette)
        {
            double healhtLost = coquette.HitPoints * 0.5;
            //round to integer
            coquette.HitPoints -= (int)healhtLost;
            Narrator.Text += $"{Name} responds to {coquette.Name} the coquette";
        }

        public void respondInnocent(Innocent innocent)
        {
            double healhtLost = innocent.HitPoints * 0.5;
            //round to integer
            innocent.HitPoints -= (int)healhtLost;
            Narrator.Text += $"{Name} responds to {innocent.Name} the innocent";
        }

        public void respondPrecious(Precious precious)
        {
            double healhtLost = precious.HitPoints * 0.5;
            //round to integer
            precious.HitPoints -= (int)healhtLost;
            Narrator.Text += $"{Name} responds to {precious.Name} the precious";
        }


        public void provokeTrickster(Trickster trickster)
        {
            Narrator.Text += $"{Name} provoked {trickster.Name} the trickster";
            trickster.respondCoquette(this);

        }

        public void provokeAuthoritarian(Authoritarian authoritarian)
        {
            Narrator.Text += $"{Name} provoked {authoritarian.Name} the authoritarian";
            authoritarian.respondCoquette(this);

        }

        public void provokeCaptain(Captain captain)
        {
            Narrator.Text += $"{Name} provoked {captain.Name} the captain";
            captain.respondCoquette(this);

        }

        public void provokeCoquette(Coquette coquette)
        {
            Narrator.Text += $"{Name} provoked {coquette.Name} the coquette";
            coquette.respondCoquette(this);

        }

        public void provokeInnocent(Innocent innocent)
        {
            Narrator.Text += $"{Name} provoked {innocent.Name} the innocent";
            innocent.respondCoquette(this);
        }

        public void provokePrecious(Precious precious)
        {
            Narrator.Text += $"{Name} provoked {precious.Name} the precious";
            precious.respondCoquette(this);
        }
    }
}