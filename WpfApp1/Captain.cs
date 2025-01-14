using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Security.Policy;
using WpfApp1;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace WpfApp1
{
    // Descendant class Captain
    public class Captain : Character
    {
        public Captain(string name, decimal money, int positionX, int positionY, int hitPoints, ObservableCollection<Character> Characters, TextBox narrator, PlayerCharacter playercharacter, int characterIndex, Canvas canvas, ListBox listBox)
            : base(name, money, positionX, positionY, hitPoints, Characters, narrator, playercharacter, characterIndex, canvas, listBox) { }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Captain: {Name}, Money: {Money}, Position: ({PositionX}, {PositionY}), Hit Points: {HitPoints}");
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
            Narrator.Text += $"{Name} responds to {trickster.Name} the trickster";
        }

        public void respondAuthoritarian(Authoritarian authoritarian)
        {

            Narrator.Text += $"{Name} responds to {authoritarian.Name} the authoritarian";

        }

        public void respondCaptain(Captain captain)
        {
            if (HitPoints <= 0)
            {
                Narrator.Text += $"\n {Name} the captain is dead";
            }
            else
            {
                if (HitPoints > captain.HitPoints)
                {
                    Narrator.Text += $"{Name} beats {captain.Name} the captain after a vicious struggle";
                    Narrator.Text += $"\n{captain.Name} the captain dies of his wounds";
                    Narrator.Text += $"\n{Name} is wounded but alive, he take {captain.HitPoints} damage";
                    HitPoints -= captain.HitPoints;
                    CharacterDeath(captain, PlayerCharacter);
                    moveCharacter(this, Canvas);
                }
                else if (HitPoints < captain.HitPoints)
                {
                    Narrator.Text += $"{Name} the captain is defeated by {captain.Name} the captain";
                    Narrator.Text += $"\n{Name} the captain dies of his wounds";
                    Narrator.Text += $"\n{captain.Name} is wounded but alive, he take {HitPoints} damage";
                    captain.HitPoints -= HitPoints;
                    CharacterDeath(this, PlayerCharacter);
                    moveCharacter(captain, Canvas);
                }
                else
                {
                    Narrator.Text += $"{Name} the captain and {captain.Name} the captain are evenly matched, they both die in the struggle";
                    CharacterDeath(this, PlayerCharacter);
                    CharacterDeath(captain, PlayerCharacter);
                }
                awaitMove();
            }
        }

        public void respondCoquette(Coquette coquette)
        {
            if (HitPoints <= 0)
            {
                Narrator.Text += $"\n{Name} the captain is dead";
            }
            else
            {
                Narrator.Text += $"{Name} responds to {coquette.Name} the coquette";
            }
            //round to integer
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
            Narrator.Text = $"{Name} provoked {trickster.Name} the trickster";
            Narrator.Text += $"\n{Name} sees {trickster.Name} the trickester and is annoyed by his flamboyant demeanor. He kicks the trickster's legs out from under him causing 30 damage";
            trickster.HitPoints -= 30;
            trickster.respondCaptain(this);

        }

        public void provokeAuthoritarian(Authoritarian authoritarian)
        {
            Narrator.Text = $"{Name} provoked {authoritarian.Name} the authoritarian";
            Narrator.Text = $"{Name} the captain sees {authoritarian.Name} and is reminded of his authoritarian father he pretend to accidentally bump into him, causing 20 points of damage";
            authoritarian.HitPoints -= 20;
            authoritarian.respondCaptain(this);

        }

        public void provokeCaptain(Captain captain)
        {
            Narrator.Text = $"{Name} provoked {captain.Name} the captain";
            Narrator.Text += $"\n{Name} the captain sees {captain.Name} the captain and felt like challenging him to a duel";
            captain.respondCaptain(this);

        }

        public void provokeCoquette(Coquette coquette)
        {
            Narrator.Text = $"{Name} provoked {coquette.Name} the coquette";
            Narrator.Text += $"\n{Name} the captain sees {coquette.Name} and took advantage of to proposition her into a liason";
            coquette.respondCaptain(this);

        }

        public void provokeInnocent(Innocent innocent)
        {
            Narrator.Text = $"{Name} provoked {innocent.Name} the innocent";
            Narrator.Text += $"{Name} the captain sees {innocent.Name} the innocent and felt like bullying this fragile creature. he pushes him over and robs him of 20 of his money";
            innocent.Money -= 20;
            innocent.HitPoints -= 10;
            if(innocent.Money < 0)
            {
                Narrator.Text += $"{Name} the captain becomes irate that the innocent does not even have this paltry sum he stabbs {innocent.Name} the innocent to death";
                CharacterDeath(innocent, PlayerCharacter);
            }
            else {
                innocent.respondCaptain(this);
            }

            
        }

        public void provokePrecious(Precious precious)
        {
            Narrator.Text = $"{Name} provoked {precious.Name} the precious";
            Narrator.Text += $"{Name} the captain sees {precious.Name} the precious and is smitten.  He proposes marriage but he accidentally trips over, knocking her down as well, causing 10 damage";
            precious.HitPoints -=10;
            precious.respondCaptain(this);
        }
    }
}
