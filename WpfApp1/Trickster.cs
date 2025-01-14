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
            if (HitPoints <= 0)
            {
                Narrator.Text += $"\n{Name} is caught off guard and stabbed in the back by {trickster.Name}";
                
                CharacterDeath(this, PlayerCharacter);
            }
            else
            {
                Narrator.Text += $"\n{Name} responds to {trickster.Name} the trickster";
                Narrator.Text += $"\n{Name} in turn, stabs {trickster.Name} the trickster in the back, causing 100 damage";
                trickster.HitPoints -= 100;
                moveCharacter(this, Canvas);
                moveCharacter(trickster, Canvas);
                if (trickster.HitPoints <= 0)
                {
                    Narrator.Text += $"\n{trickster.Name} the trickster is caught off guard and is also stabbed in the back";
                    Narrator.Text += $"\n{trickster.Name} He dies, and the world looses a piece of whimsy";
                    moveCharacter(this, Canvas);
                    CharacterDeath(trickster, PlayerCharacter);
                }



            }
            Task.Delay(TimeSpan.FromSeconds(10));
        }

        public void respondAuthoritarian(Authoritarian authoritarian)
        {

            if (HitPoints <= 0)
            {
                Narrator.Text += $"{Name} is caught off guard and is struck dead. no more tricks will he do";
            }
            else
            {
                Narrator.Text += $"\n{Name} responds to {authoritarian.Name} the authoritarian";

                Narrator.Text += $"\n{Name} the trickster recovers and pilfers {authoritarian.Name} for 80 of his money";
                if (authoritarian.Money >= 80)
                {
                    Money += 80;
                    authoritarian.Money -= 80;
                    Narrator.Text += $"\n{Name} the trickster laughs and dissapears in to the shadows with the money";
                    moveCharacter(authoritarian, Canvas);
                }
                else
                {
                    Narrator.Text += $"{authoritarian.Name} has so little money that, in anger, {Name} the trickster strikes in his weakest point, killing him";
                    CharacterDeath(authoritarian, PlayerCharacter);
                }
                moveCharacter(this, Canvas);

            }
            
            awaitMove();

        }

        public void respondCaptain(Captain captain)
        {
            if (HitPoints <= 0)
            {
                Narrator.Text += $"\n{Name} is caught off guard by the knights captain and is struck dead, error of a life time";
                CharacterDeath(this, PlayerCharacter);
            }
            else
            {
                Narrator.Text += $"\n{Name} responds to {captain.Name} the captain";
                Narrator.Text += $"\n{Name} the trickster sneaks up to {captain.Name}'s blind spot to unsheath his knife";
                Narrator.Text += $"\n{captain.Name} the captain is caught off guard and takes 50 points of damage";
                captain.HitPoints -= 50;
                if (captain.HitPoints <= 0)
                {
                    Narrator.Text += $"\n{captain.Name} the captain of the guard is killed by a schoundrel, a trickster, a thief let history speak ill of {Name} henceforth";
                    CharacterDeath(captain, PlayerCharacter);
                    moveCharacter(this, Canvas);
                }
                else
                {
                    Narrator.Text += $"\n{captain.Name} survives his injuries and has {HitPoints} points of health left";
                    moveCharacter(this, Canvas);
                    moveCharacter(captain, Canvas);

                }


            }
            awaitMove();
        }

        public void respondCoquette(Coquette coquette)
        {
            if (HitPoints <= 0)
            {
                Narrator.Text += $"\n{Name} the trickster is caught off guard and dies like the fool he is";
                CharacterDeath(this, PlayerCharacter);
            }
            else
            {
                Narrator.Text += $"\n{Name} responds to {coquette.Name} the coquette";
                Narrator.Text += $"\n{Name} the trickster recongnizes {coquette.Name}, as his old flame. With a wry smirk, he tries to seduce her";
                if (Money >= 60 && HitPoints >= 60)
                {
                    Narrator.Text += $"\n{Name} the trickster has enough money and charm to woo back {coquette.Name} the coquette";
                    Narrator.Text += $"\n{Name} and {coquette.Name} heal each other for 20 points each";
                    HitPoints += 20;
                    coquette.HitPoints += 20;
                    moveCharacter(this, Canvas);
                    moveCharacter(coquette, Canvas);
                }
                else
                {
                    Narrator.Text += $"\n{Name} the trickster is still kind of a looser, and {coquette.Name} the coquette slaps him for 20 points and inflicts 30 points of shame on him self";
                    HitPoints -= 30;
                    if (HitPoints <= 0)
                    {
                        CharacterDeath(this, PlayerCharacter);
                        Narrator.Text += $"\n{Name} the trickster dies, embroiled in the drama of love and death, now by humility, now by jelousy opressed";
                    }
                    moveCharacter(this, Canvas);
                }
            }
            awaitMove();
                    
        }

        public void respondInnocent(Innocent innocent)
        {
            if (HitPoints <= 0)
            {
                Narrator.Text += $"\n{Name} the trickster dies from cringe of interacting with such an idiot";
                CharacterDeath(this, PlayerCharacter);
            }
            else
            {
                Narrator.Text += $"\n{Name} responds to {innocent.Name} the innocent";
                Narrator.Text += $"\n{Name} the trickster kicks {innocent.Name} the innocent in the behind, dealing 30 damage";
                innocent.HitPoints -= 30;
                if (innocent.HitPoints <= 0)
                {
                    CharacterDeath(innocent, PlayerCharacter);
                    Narrator.Text += $"\n{innocent.Name} the innocent dies, a victim of the cruel world";
                }
                else
                {
                    Narrator.Text += $"\n{innocent.Name} the innocent has {innocent.HitPoints} points of health left";
                }
                moveCharacter(this, Canvas);
                //wait .7 seconds
                
            } awaitMove();

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
            Narrator.Text += $"\n{Name} the trickster sneaks up to {trickster.Name}, his rival, and  unsheath his knife";
            Narrator.Text += $"\n{trickster.Name} the trickster is caught off guard and is stabbed in the back by {Name} the trickster";
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
