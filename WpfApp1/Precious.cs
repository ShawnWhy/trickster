using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp1
{

    // Descendant class Precious
    public class Precious : Character
    {
        public Precious(string name, decimal money, int positionX, int positionY, int hitPoints, ObservableCollection<Character> characters, TextBox narrator, PlayerCharacter playercharacter, int characterIndex,Canvas canvas, ListBox listBox)
            : base(name, money, positionX, positionY, hitPoints, characters, narrator, playercharacter, characterIndex, canvas, listBox) { }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Precious: {Name}, Money: {Money}, Position: ({PositionX}, {PositionY}), Hit Points: {HitPoints}");
        }

        public void Provoke(Character character)
        {
            string narratorString = $"{Name} pokes {character.Name}";
            Narrator.Text += narratorString;
        }

        public void Respond()
        {
            Narrator.Text += "Responding to provoke.";
            if (this.Characters.Count == 0)
            {
                Narrator.Text += "No characters to provoke.";
                return;
            }

            //find the closest character in the character list
            //create a new list of characters where the current character is abscent by name
            ObservableCollection<Character> characters2 = new ObservableCollection<Character>(this.Characters.Where(c => c.Name != this.Name));
            if (characters2.Count == 0)
            {
                Narrator.Text += "No characters to provoke.";
                return;
            }
            Narrator.Text += "Responding to provoke.";
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
           
            Narrator.Text += $"{Name} responds to {trickster.Name} the trickser";
            if (HitPoints > 0)
            {
                Narrator.Text += $"{Name} the Precious is freightened and kicks {trickster.Name} the trickster and runs away.";
                trickster.HitPoints -= 10;
                if (trickster.HitPoints <= 0)
                {
                    Narrator.Text += $"{trickster.Name} the trickster is dead.";
                    CharacterDeath(trickster, PlayerCharacter);
                }
                else
                {
                    Narrator.Text += $"{trickster.Name} the trickster has {trickster.HitPoints} hit points left.";
                }

            }
            else
            {
                Narrator.Text += $"{Name} the Precious is dead.";
                CharacterDeath(this, PlayerCharacter);
            }
            updateCanvasandCharacterList();
        }

        public void respondAuthoritarian(Authoritarian authoritarian)
        {
            
            Narrator.Text += $"\n {Name} responds to {authoritarian.Name} the authoritarian";
            if (HitPoints > 0)
            {
                Narrator.Text += $"\n{Name} the Precious flirts with {authoritarian.Name} the authoritarian. but then insults him for being a beastly savage";
                Narrator.Text += $"\n{authoritarian.Name} the authoritarian is deeply hurt by the insult and loses 40 hitpoints";
                
                if(authoritarian.HitPoints <= 0)
                {
                    Narrator.Text += $"\n{authoritarian.Name} the authoritarian kills him self out of shame right in front of {Name} the precious.";
                    CharacterDeath(authoritarian, PlayerCharacter);
                }
                else if(authoritarian.HitPoints <= 25)
                {
                    Narrator.Text += $"\n{authoritarian.Name} the authoritarian is too weak to mend his broken heart, he is unable to recover";
                    CharacterDeath(authoritarian, PlayerCharacter);
                }
                else
                {
                    Narrator.Text += $"\n{authoritarian.Name} the authoritarian has {authoritarian.HitPoints} hit points left.";
                }

            }
            else
            {
                Narrator.Text += $"{Name} the Precious is dead.";
                CharacterDeath(this, PlayerCharacter);
            }
            updateCanvasandCharacterList();
        }

    

        public void respondCaptain(Captain captain)
        {
         

            Narrator.Text += $"\n{Name} responds to {captain.Name} the captain";
            //add new line
            if (HitPoints > 0)
            {
                Narrator.Text += $"\n{Name} the Precious is furious with {captain.Name}. she slaps him with a sharp open palm";
                captain.HitPoints -= 10;
                Narrator.Text += $"\n{captain.Name} the captain lost 10 hitpoints";
                if (captain.HitPoints < 0)
                {
                    Narrator.Text += $"\n{captain.Name} the captain is dead.";
                    CharacterDeath(captain, PlayerCharacter);
                }
                else if (captain.HitPoints <= 25)
                {
                    Narrator.Text += $"\n{captain.Name} the captain too weak to mend his broken heart, he is unable to recover";
                    CharacterDeath(captain, PlayerCharacter);
                }
                else if (captain.Money > 100)
                {
                    Narrator.Text += $"\n{captain.Name} the captain is a wealthy and handsome fellow, {Name} the precious agrees to the marriage and heals him for 10 points";
                    captain.Money -= 100;
                    captain.HitPoints += 10;
                    Money+= 100;
                }
            }
            else
            {
                Narrator.Text += $"{Name} the Precious is dead.";
                CharacterDeath(this, PlayerCharacter);
                CharacterDeath(captain, PlayerCharacter);
                Narrator.Text +=$"{captain.Name} the captain cannot believe the gravity of his deeds. He kills him self in front of the crowd in a narcissistic dramatic display";
            }
            updateCanvasandCharacterList();

        }

        public void respondCoquette(Coquette coquette)
        {

            Narrator.Text += $"\n {Name} responds to {coquette.Name} the coquette";
            if (HitPoints > 0)
            {
                Narrator.Text += $"\n{Name} the Precious slaps {coquette.Name} the coquette in retaliation";
                coquette.HitPoints -= 10;
                if (coquette.HitPoints <= 0)
                {
                    Narrator.Text += $"\n{coquette.Name} the coquette is dead.";
                    CharacterDeath(coquette, PlayerCharacter);
                }
                else
                {
                    if (Money > 10)
                    {
                        Money -= 10;

                        Narrator.Text += $"\n{coquette.Name} the coquette pilfers 10 moneys from {Name}";
                    }
                    else
                    {
                        Narrator.Text += $"\n{Name} the Precious is too poor to pilfer from";
                        Narrator.Text += $"\n she is deeply ashamed of her poverty and is injured by her pride";
                        HitPoints -= 10;
                        if (HitPoints <= 0)
                        {
                            Narrator.Text += $"\n{Name} the Precious is dead.";
                            CharacterDeath(this, PlayerCharacter);
                        }
                    }
                    Narrator.Text += $"\n{coquette.Name} the coquette pilfetred 10 moneys from {Name} the precious";
                }
            }
            else
            {
                Narrator.Text += $"{Name} the Precious is dead.";
                CharacterDeath(this, PlayerCharacter);
            }
            Narrator.Text += $"\n{Name} the Precious slaps {coquette.Name} the coquette in retaliation";
            Narrator.Text += $"\n";
            if (HitPoints > 0)
            {
                coquette.HitPoints -= 10;
                if (coquette.HitPoints <= 0)
                {
                    Narrator.Text += $"\n{coquette.Name} the coquette is dead.";
                }
            }
            else
            {
                Narrator.Text += $"{Name} the Precious is dead.";
                CharacterDeath(this, PlayerCharacter);
            }
            updateCanvasandCharacterList();
        }

        public void respondInnocent(Innocent innocent)
        {
            if (HitPoints > 0)
            {


                Narrator.Text += $"\n{Name} responds to {innocent.Name} the innocent";
                Narrator.Text += $"\n{Name} the Precious maliciously mocks {innocent.Name} to no effect and is injured in her pride";
                Narrator.Text += $"\n{Name} the Precious loses 10 hitpoints";
                HitPoints -= 10;
                if (HitPoints <= 0)
                {
                    Narrator.Text += $"\n{Name} the Precious  cannot take the humiliation and dies.";
                    CharacterDeath(this, PlayerCharacter);
                }
            }
            else
            {
                Narrator.Text += $"{Name} the Precious is dead.";
                CharacterDeath(this, PlayerCharacter);
            }
            updateCanvasandCharacterList();
        }

        public void respondPrecious(Precious precious)
        {
            if (HitPoints > 0)
            {
                Narrator.Text += $"{Name} the Precious is too proud to respond to {precious.Name} the precious";
                Narrator.Text += $"{Name} the Precious gives {precious.Name} the side eye and injures her dignity for 5 points";
                precious.HitPoints-= 5;
                if(precious.HitPoints <= 0)
                {
                    Narrator.Text += $"{precious.Name} the precious could not live with the indiginity, she dies";
                    CharacterDeath(precious, PlayerCharacter);
                }
            }
            else
            {
                Narrator.Text += $"{Name} the Precious is dead.";
                CharacterDeath(this, PlayerCharacter);
            }
            updateCanvasandCharacterList();

        }

        public void provokeTrickster(Trickster trickster)
        {
            Narrator.Text = $"{Name} provoked {trickster.Name} the trickster";
            Narrator.Text += $"\n{Name} the precious stares down {trickster.Name} the trickster, causing little psychological damage";

            trickster.HitPoints -= 5;
            trickster.respondPrecious(this);

        }

        public void provokeAuthoritarian(Authoritarian authoritarian)
        {
            Narrator.Text = $"{Name} provoked {authoritarian.Name} the authoritarian";
            Narrator.Text += $"\n{Name} the precious chastises {authoritarian.Name} the authoritarian for some invented transgression, causing small psychological damage.";
            Narrator.Text += $"\nshe bullys {authoritarian.Name} into donating 30 moneys to the public.  You steal the charity money";
            authoritarian.Money -= 30;
            this.PlayerCharacter.Money += 30;
            authoritarian.respondPrecious(this);

        }

        public void provokeCaptain(Captain captain)
        {
            Narrator.Text = $"{Name} provoked {captain.Name} the captain";
            Narrator.Text += $"\n{Name} the precious slaps {captain.Name} the captain, causing minor physical damage";
            captain.HitPoints -= 5;
            captain.respondPrecious(this);

        }

        public void provokeCoquette(Coquette coquette)
        {
            Narrator.Text = $"{Name} provoked {coquette.Name} the coquette";
            Narrator.Text += $"\n{Name} the precious slaps {coquette.Name} the coquette, causing minor physical damage";
            coquette.HitPoints -= 5;
            coquette.respondPrecious(this);

        }

        public void provokeInnocent(Innocent innocent)
        {
            Narrator.Text = $"{Name} provoked {innocent.Name} the innocent";
            Narrator.Text += $"\n{Name} insults {innocent.Name} the innocent in a subtleway. {innocent.Name} the innocent is injured morally at a later time";
            Narrator.Text += $"\n{innocent.Name} the innocent takes 5 points of damage";
            innocent.HitPoints -= 5;  
            innocent.respondPrecious(this);
        }

        public void provokePrecious(Precious precious)
        {
            Narrator.Text = $"{Name} provoked {precious.Name} the precious";
            Narrator.Text += $"{Name} the precious complements {precious.Name} the precious but the compliment contains a underhanded insult. {precious.Name} takes slight moral damage";
            Narrator.Text += $"\n{precious.Name} the precious takes 5 points of damage";
            precious.HitPoints -= 5;
            precious.respondPrecious(this);
        }
    }

}
