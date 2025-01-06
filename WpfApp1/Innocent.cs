using Microsoft.Windows.Themes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp1
{

    // Descendant class Innocent
    public class Innocent : Character
    {
        public Innocent(string name, decimal money, int positionX, int positionY, int hitPoints, ObservableCollection<Character> characters, TextBox narrator, PlayerCharacter playercharacter, int characterIndex, Canvas canvas, ListBox listBox)
            : base(name, money, positionX, positionY, hitPoints, characters, narrator, playercharacter, characterIndex, canvas, listBox) { }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Innocent: {Name}, Money: {Money}, Position: ({PositionX}, {PositionY}), Hit Points: {HitPoints}");
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

            if(HitPoints >= 0)
            {
                Narrator.Text += $"\n{Name} is too fragile to take the physical damage dealt by the brutal strike, he dies while suffering";
                CharacterDeath(this, PlayerCharacter);
            }
            else
            {
                Narrator.Text += $"\n{Name} responds to {authoritarian.Name} the authoritarian";
                Narrator.Text += $"\n{Name} the innocent has no natural defence against such brutal personages.  He guilts the Authoritarian with a speech about protection of the meek, shaming him in to giving him 5 money";
                if (authoritarian.Money >= 5)
                {
                    authoritarian.Money -= 5;
                    Money += 5;
                    Narrator.Text += $"\n{Name} the innocent has takes the money as a gift from heaven";
                }
                else
                {
                
                    Narrator.Text += $"\n{authoritarian.Name} cannot believe the status of his poor finances, ashamed that he is now amongst the poorest of the poor, he commits suicide right in front of the crowd that is gathered";
                    CharacterDeath (authoritarian, PlayerCharacter);
                }
                

            }
            


        }

        public void respondCaptain(Captain captain)
        {
            if (HitPoints >= 0)
            {
                Narrator.Text += $"\n{Name} is too fragile to take the physical damage dealt by the brutal strike, he dies while suffering";
                CharacterDeath(this, PlayerCharacter);
            }
            else
            {
                Narrator.Text += $"\n{Name} responds to the strike by falsify the extend of his injury and puts on a show of exessive suffering the crow gathers to reprimand the captain and he has to produce monetary restitutions";
                Narrator.Text += $"\n{captain.Name} takes 20 damage and is fined 50 money";
                if (captain.Money >= 50)
                {
                    captain.Money -= 50;
                    captain.HitPoints -= 20;
                    Money += 50;
                    if (captain.HitPoints <= 0)
                    {
                        Narrator.Text += $"\n{captain.Name} the captain dies form a lack of honor";
                        CharacterDeath(captain, PlayerCharacter);
                    }

                }
                else
                {
                    Narrator.Text += $"\n{Name} the innocent takes the money and is happy to have survived the ordeal";
                }

            }
            updateCanvasandCharacterList();
        }

        public void respondCoquette(Coquette coquette)
        {
            if (HitPoints >= 0)
            {
                Narrator.Text += $"\n{Name} is too fragile to take the physical damage dealt by the coquette, he dies while suffering";
                CharacterDeath(this, PlayerCharacter);
            }
            else
            {
                Narrator.Text += $"\n{Name} responds to {coquette.Name} the coquette";
                Narrator.Text += $"\n{Name} the innocent does not how to respond other than to exagerate his meekness causing the coquette 30 points of damage dues to its cringiness";
                coquette.HitPoints -= 30;
                if (coquette.HitPoints <= 0)
                {
                    Narrator.Text += $"\n{coquette.Name} the coquette dies of cringe";
                    CharacterDeath(coquette, PlayerCharacter);
                }
                else
                {
                    Narrator.Text += $"\n{Name} they both walk away with their lives";
                }
            }
            updateCanvasandCharacterList();
        }

        public void respondInnocent(Innocent innocent)
        {
            if (HitPoints >= 0)
            {
                Narrator.Text += $"\n{Name} is too fragile to take the physical damage dealt by the innocent, he dies while suffering";
                CharacterDeath(this, PlayerCharacter);
            }
            else
            {

                Narrator.Text += $"\n{Name} and {innocent.Name} played happily together";
                Money += 10;
            }
            updateCanvasandCharacterList();
        }

        public void respondPrecious(Precious precious)
        {
            if (HitPoints >= 0) {
                Narrator.Text += $"\n{Name} is too fragile to take the physical damage dealt by the precious, he dies while suffering";
                CharacterDeath(this, PlayerCharacter);
            }
            else
            {
                Narrator.Text += $"\n{Name} responds to {precious.Name} the precious";
                Narrator.Text += $"\n{Name} the innocent is so moved by the beauty of the precious that he gives writes her a terrible love poem, which is such an assult on the precious' delicate taste that she takes damage";

                precious.HitPoints -= 10;
                if (precious.HitPoints <= 0)
                {
                    Narrator.Text += $"\n{precious.Name} the precious dies of cringe";
                    CharacterDeath(precious, PlayerCharacter);
                }
                else
                {
                    Narrator.Text += $"\n{Name} they both walk away with their lives";
                }
            }
            
        }


        public void provokeTrickster(Trickster trickster)
        {
            Narrator.Text = $"{Name} provoked {trickster.Name} the trickster";
            Narrator.Text +=$"\n{Name} the innocent sees {trickster.Name} the trickster and assults him with his terrible poetry causing 20 mental damage";
            trickster.HitPoints -= 20;
            trickster.respondInnocent(this);
            

        }

        public void provokeAuthoritarian(Authoritarian authoritarian)
        {
            Narrator.Text = $"{Name} provoked {authoritarian.Name} the authoritarian";
            Narrator.Text += $"\n{Name} the innocent sees {authoritarian.Name} the authoritarian and assults him with his dreams of utopian city, causing 20 mental damage";
            authoritarian.HitPoints -= 30;
            if (authoritarian.HitPoints <= 0)
            {
                Narrator.Text += $"\n{authoritarian.Name} the authoritarian is so moved by the vision of the perfect city that he dies of a broken heart";
                CharacterDeath(authoritarian, PlayerCharacter);
            }
            else if (Money >= 100)
                {
                    if (authoritarian.Money >= 10)
                    {
                        authoritarian.Money -= 10;
                        Money += 10;
                        Narrator.Text += $"\n{Name} the innocent is rich enough to warrant respect from the authoritarian, and he donates 10 money to him";

                    }
                    else
                    {
                        Narrator.Text += $"\n{authoritarian.Name} cannot believe the status of his poor finances, ashamed that he is now amongst the poorest of the poor, he kills him self";
                        CharacterDeath(authoritarian, PlayerCharacter);
                    }
                }
            
            else {
                authoritarian.respondInnocent(this);
            }
        }

        public void provokeCaptain(Captain captain)
        {
            Narrator.Text = $"{Name} provoked {captain.Name} the captain";
            Narrator.Text += $"{Name} the innocent sees {captain.Name} the captain and went to annoy him with his poetry causing 20 mental damage.  He tries to throw him 20 money to make him stop";
            captain.HitPoints -= 20;
            if (captain.HitPoints <= 0)
            {
                Narrator.Text += $"\n{captain.Name} the captain dies of a brain aneurysm";
                CharacterDeath(captain, PlayerCharacter);
            }
            else
            if (captain.Money >= 20)
            {
                captain.Money -= 20;
                Money += 20;
                if (Money >= 100)
                {
                    Narrator.Text += $"\n{Name} the innocent is rich enough to stop playing they both survive";
                }
                else
                {
                    Narrator.Text += $"\n{Name} the innocent does not stop";
                    captain.respondInnocent(this);
                }

            }
            else
            {
                Narrator.Text += $"\n{captain.Name} the captain is made aware of his dire finiancial circumstances and dies of shame";
                CharacterDeath(captain, PlayerCharacter);
            }
           

        }

        public void provokeCoquette(Coquette coquette)
        {
            Narrator.Text = $"{Name} provoked {coquette.Name} the coquette";
            Narrator.Text += $"\n{Name} the innocent sees {coquette.Name} the coquette and goes to serenade her with a terrible love song, causing 10 mental damage";
            coquette.HitPoints -= 10;
            coquette.respondInnocent(this);

        }

        public void provokeInnocent(Innocent innocent)
        {
            Narrator.Text = $"{Name} provoked {innocent.Name} the innocent";
            Narrator.Text += $"{Name} the innocent sees {innocent.Name} the innocent and is so moved by a fellow soul kin that they performed together gaining 10 money each";
            innocent.Money += 10;
            innocent.respondInnocent(this);
        }

        public void provokePrecious(Precious precious)
        {
            Narrator.Text = $"{Name} provoked {precious.Name} the precious";
            Narrator.Text +=$"{Name} the innocent sees {precious.Name} the precious and is so moved by the beauty of the precious that he gives writes her a terrible love poem";
            precious.respondInnocent(this);
        }
    }
}
