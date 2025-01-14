using System;
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
            awaitMove();
        }

        public void respondCoquette(Coquette coquette)
        {
            if (HitPoints <= 0)
            {
                Narrator.Text += $"\n{Name} the coquette dies tragically";
                CharacterDeath(this, PlayerCharacter);
                
            }
            else
            {
                Narrator.Text += $"\n{Name} the coquette kicks {coquette.Name} the coquette in the shin for 10 points of damage";
                coquette.HitPoints -= 10;
                if(coquette.HitPoints <= 0)
                {
                    CharacterDeath(coquette, PlayerCharacter);
                    Narrator.Text += $"\n{coquette.Name} the coquette dies from the blow of the rival";
                }   
            }
            awaitMove();
        }

        public void respondInnocent(Innocent innocent)
        {
            if (HitPoints <= 0)
            {
                Narrator.Text += $"\n{Name} the coquette dies from the cringe";
                CharacterDeath(this, PlayerCharacter);
            }
            else
            {
                Narrator.Text += $"\n{Name} the coquette grows irritated from the terrible performance and kicks {innocent.Name} the innocent for 30 points of damage";
                innocent.HitPoints -= 30;
                if (innocent.HitPoints <= 0)
                {
                    CharacterDeath(innocent, PlayerCharacter);
                    Narrator.Text += $"\n{innocent.Name} the innocent dies a rightful death";

                }
            }
            awaitMove();
        }

        public void respondPrecious(Precious precious)
        {
            if (HitPoints <= 0)
            {
                Narrator.Text += $"\n{Name} the coquette faints";
            }
            else
            {
                if (Money >= 100)
                {
                    Narrator.Text += $"\n{precious.Name} is a passable romantic partner {Name} take 50 pending money from him and heals him for 10 points";
                    precious.Money -= 50;
                    Money += 50;
                }
                else
                {
                    Narrator.Text += $"\n{precious.Name} does not have enough money and is too much of a looser t0 become a passable love interest.  She slaps him for 20 points and and he inflicts 30 points of shame on him self";
                    precious.HitPoints -= 30;
                    precious.HitPoints -= 20;
                    if (precious.HitPoints <= 0)
                    {
                        CharacterDeath(precious, PlayerCharacter);
                        Narrator.Text += $"\n{precious.Name} the precious dies, embroiled in the drama of love and death, now by humility, now by jelousy opressed";
                    }
                }
            }
        }


        public void provokeTrickster(Trickster trickster)
        {
            Narrator.Text = $"{Name} provoked {trickster.Name} the trickster";
            Narrator.Text += $"\n{Name} the coquette sees {trickster.Name} and is immediately irate from their past experience with each other she kicks him right in the crotch, causing 50 damage";
            trickster.HitPoints -= 50;
            trickster.respondCoquette(this);

        }

        public void provokeAuthoritarian(Authoritarian authoritarian)
        {
            Narrator.Text = $"{Name} provoked {authoritarian.Name} the authoritarian";
            Narrator.Text += $"\n{Name} flirt with {authoritarian.Name} the authoritarian and asks for 20 money to keep her self away from trouble";
            if (HitPoints >= 50)
            {
                if (authoritarian.Money >= 20)
                {
                    Narrator.Text += $"\n{authoritarian.Name} is attracted to {Name} and gives her 20";
                    authoritarian.Money -= 20;
                    Money += 20;
                    moveCharacter(authoritarian, Canvas);
                    moveCharacter(this, Canvas);
                }
                else
                {
                    Narrator.Text += $"\n{authoritarian.Name} reaches into his pocket and finds that he him self has become poor. he kills him self from poverty of shame";
                    CharacterDeath(authoritarian, PlayerCharacter);

                }
            }
            else
            {
                Narrator.Text += $"\n{Name} the coquette is too plain to attract {authoritarian.Name} the authoritarian, he brushes her off for 5 points of damage";
                HitPoints -= 5;
                if (HitPoints <= 0)
                {
                    CharacterDeath(this, PlayerCharacter);
                    Narrator.Text += $"\n{Name} the coquette is frail and dies from push from the brute";
                }
                else
                {
                    authoritarian.respondCoquette(this);    
                }
            }

            awaitMove();

        }

        public void provokeCaptain(Captain captain)
        {
            Narrator.Text = $"{Name} provoked {captain.Name} the captain";
            Narrator.Text += $"\n{Name} the coquette blew a kiss at the captain, he becomes dumbstruck";
            Narrator.Text += $"\n{Name} sneaks up to the captain and peaks into his wallet";
            if (captain.Money >= 20)
            {

                Narrator.Text += $"\n{Name} the coquette takes 20 pending money from the captain and leaves with out him noticing";
                captain.Money -= 20;
                Money += 20;
                moveCharacter(captain, Canvas);
                moveCharacter(this,Canvas);
                awaitMove();
            }
            else
            {
                Narrator.Text += $"\n{Name} the coquette finds so little money in the captain's wallet that she stamps on his foot in fury, causing 20 damage";
                captain.HitPoints -= 20;
                captain.respondCoquette(this);
            }

        }

        public void provokeCoquette(Coquette coquette)
        {
            Narrator.Text = $"{Name} provoked {coquette.Name} the coquette";
            Narrator.Text += $"\n{Name} the coquette sees {coquette.Name} the coquette and is irked by the appearance of a rival.  She purposely steps on {coquette.Name}'s foot for 10 damage";
            coquette.HitPoints -= 10;
            coquette.respondCoquette(this);

        }

        public void provokeInnocent(Innocent innocent)
        {
            Narrator.Text= $"{Name} provoked {innocent.Name} the innocent";
            Narrator.Text += $"\n{Name} the coquette sees {innocent.Name} and is irked by the apprearance of such a fraile creature. She pinches his cheek maliciously, causing 20 damage";
            innocent.HitPoints -= 20;
            innocent.respondCoquette(this);
        }

        public void provokePrecious(Precious precious)
        {
            Narrator.Text = $"{Name} provoked {precious.Name} the precious";
            Narrator.Text += $"\n{Name} the coquette sees {precious.Name} the precious and is irked by the appearance of such a delicate creature. She pinches his cheek maliciously, causing 20 damage";
            precious.HitPoints -= 20;
            precious.respondCoquette(this);
        }
    }
}
