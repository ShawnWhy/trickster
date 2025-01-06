using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1;
using System.Xml.Linq;
using System.Windows.Markup;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
namespace WpfApp1
{
    public class Trickster : Character, IProvoke, IRespond
    {
        public Trickster(string name, decimal money, int positionX, int positionY, int hitPoints, ObservableCollection<Character> characters, TextBox narrator, PlayerCharacter playerCharacter, int characterIndex, Canvas canvas, ListBox listBox)
            : base(name, money, positionX, positionY, hitPoints, characters, narrator, playerCharacter, characterIndex, canvas, listBox) { }

        
        public override void DisplayInfo()
        {
           
        }


        public void respondTrickster(Trickster trickster)
        {
            double healhtLost = trickster.HitPoints * 0.5;
            //round to integer
            trickster.HitPoints -= (int)healhtLost;
            Narrator.Text += $"{Name} respons to {trickster.Name} the trickster";
        }

        public void respondAuthoritarian(Authoritarian authoritarian)
        {

            if (HitPoints <= 0)
            {
                Narrator.Text += $"{Name} is caught off guard and is struck dead. no more tricks will he do";
            }
            else
            {
                Narrator.Text += $"{Name} responds to {authoritarian.Name} the authoritarian";

                Narrator.Text += $"{Name} the trickster recovers and pilfers {authoritarian.Name} for 80 of his money";
                if (authoritarian.Money >= 80)
                {
                    Money += 80;
                    authoritarian.Money -= 80;
                    Narrator.Text += $"{Name} the trickster laughs and dissapears in to the shadows with the money";
                }
                else
                {
                    Narrator.Text += $"{authoritarian.Name} has so little money that, in anger, {Name} the trickster strikes in his weakest point, killing him";
                    CharacterDeath(authoritarian, PlayerCharacter);
                }

            }

        }

        public void respondCaptain(Captain captain)
        {
            double healhtLost = captain.HitPoints * 0.5;
            //round to integer
            captain.HitPoints -= (int)healhtLost;

            Narrator.Text += $"{Name} responds to {captain.Name} the captain";
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
            trickster.respondTrickster(this);

        }

        public void provokeAuthoritarian(Authoritarian authoritarian)
        {
            Narrator.Text += $"{Name} provoked {authoritarian.Name} the authoritarian";
            authoritarian.respondTrickster(this);

        }

        public void provokeCaptain(Captain captain)
        {
            Narrator.Text += $"{Name} provoked {captain.Name} the captain";
            captain.respondTrickster(this);

        }

        public void provokeCoquette(Coquette coquette)
        {
            Narrator.Text += $"{Name} provoked {coquette.Name} the coquette";
            coquette.respondTrickster(this);

        }

        public void provokeInnocent(Innocent innocent)
        {
            Narrator.Text += $"{Name} provoked {innocent.Name} the innocent";
            innocent.respondTrickster(this);
        }

        public void provokePrecious(Precious precious)
        {
            Narrator.Text += $"{Name} provoked {precious.Name} the precious";
            precious.respondTrickster(this);
        }





        public void Give(Character receiver, decimal amount)
        {
            if (Money >= amount)
            {
                Money -= amount;
                receiver.Money += amount;
                Console.WriteLine($"{Name} gave {amount} to {receiver.Name}");
            }
            else
            {
                Console.WriteLine($"{Name} does not have enough money to give.");
            }
        }

        public void Provoke(Character character)
        {
            string narratorString = $"{Name} pokes {character.Name}";
            Narrator.Text += narratorString;

            switch (character.GetType().Name)
            {
                case "Trickster":

                    Trickster trickster = (Trickster)character;
                    trickster.Respond();
                    
                    //cast character to trickster
                    break;
                case "Authoritarian":
                    Authoritarian authoritarian = (Authoritarian)character;
                    authoritarian.Respond();
                    break;
                case "Captain":
                    Captain captain = (Captain)character;
                    captain.Respond();
                    break;
                case "Coquette":
                    Coquette coquette = (Coquette)character;
                    coquette.Respond();
                    break;
                case "Innocent":
                    Innocent innocent = (Innocent)character;
                    innocent.Respond();
                    break;
                case "Precious":
                    Precious precious = (Precious)character;
                    precious.Respond();
                    break;
                default:
                    Console.WriteLine("Character not found.");
                    break;

            }
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

        public void Take(Character giver, decimal amount)
        {
            if (giver.Money >= amount)
            {
                giver.Money -= amount;
                Money += amount;
                Console.WriteLine($"{Name} took {amount} from {giver.Name}");
            }
            else
            {
                Console.WriteLine($"{giver.Name} does not have enough money to take.");
            }
        }
    }

}
