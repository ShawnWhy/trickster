using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace WpfApp1
{
    // Descendant class Trickster


    // Descendant class Authoritarian
    public class Authoritarian : Character, IProvoke, IRespond
    {
        public Authoritarian(string name, decimal money, int positionX, int positionY, int hitPoints, ObservableCollection<Character> Characters, TextBox narrator, PlayerCharacter playercharacter, int characterIndex, Canvas canvas, ListBox listBox)
            : base(name, money, positionX, positionY, hitPoints, Characters, narrator, playercharacter, characterIndex, canvas, listBox) { }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Authoritarian: {Name}, Money: {Money}, Position: ({PositionX}, {PositionY}), Hit Points: {HitPoints}");
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
            if(characters2.Count== 0)
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

        public void respondTrickster(Trickster trickster)
        {

            if (HitPoints <= 0)
            {
                CharacterDeath(this, PlayerCharacter);
                Narrator.Text += $"\n{Name} the authoritarian clutches his would and dies a ignoble death";
            }
            else
            {
                trickster.HitPoints -= 5;
                Narrator.Text += $"{Name} responds to {trickster.Name} the trickster";
                Narrator.Text += $"\n{Name} the athoritarian becomes irritated and strikes the trickster, dealing 5 damage";
                if (trickster.HitPoints <= 0)
                {
                    CharacterDeath(trickster, PlayerCharacter);
                }
                else
                {
                    Narrator.Text += $"{trickster.Name} the trickster has {trickster.HitPoints} health left";
                }
            }
            updateCanvasandCharacterList();
        }

        public void respondAuthoritarian(Authoritarian authoritarian)
        {
            if (HitPoints <= 0)
            {
                CharacterDeath(this, PlayerCharacter);
                Narrator.Text += $"\n{Name} the authoritarian clutches his would and dies a ignoble death";
            }
            else
            {
                Narrator.Text += $"\n{Name} responds to {authoritarian.Name} the authoritarian";

                Narrator.Text += $"\n{Name} the authoritarian fights back against {authoritarian.Name}, taking 25 of their health";
                authoritarian.HitPoints -= 25;
                if (authoritarian.HitPoints <= 0)
                {
                    CharacterDeath(authoritarian, PlayerCharacter);

                }
                else
                {
                    Narrator.Text += $"{authoritarian.Name} the authoritarian has {authoritarian.HitPoints} health left";
                }
            }
            updateCanvasandCharacterList();
        }

        public void respondCaptain(Captain captain)
        {
            if (HitPoints <= 0)
            {
                CharacterDeath(this, PlayerCharacter);
                Narrator.Text += $"\n{Name} the authoritarian clutches his would and dies a ignoble death";
            }
            else
            {
                Narrator.Text += $"\n{Name} responds to {captain.Name} the captain";
                Narrator.Text += $"\n{Name} the authoritarian becomes irritated and strikes the captain, dealing 20 damage he also confiscate 50 of the captain's money";
                Narrator.Text += $"\n{captain.Name} the captain retaliates against the strike, dealing 10 points in return ";
                captain.HitPoints -= 20;
                HitPoints -= 10;
                if (captain.Money >= 50)
                {
                    Money += 50;
                }
                else
                {
                    Money += captain.Money;

                }

                captain.Money -= 50;
                Money += 50;
                if (captain.HitPoints <= 0)
                {
                    CharacterDeath(captain, PlayerCharacter);
                    Narrator.Text += $"\n{captain.Name} clutches his wound and falls over, dead";

                }
                else if (captain.Money < 0)
                {
                    CharacterDeath(captain, PlayerCharacter);
                    Narrator.Text += $"\n{captain.Name} the captain has no money to pay the fine, and is jailed by the {Name} the authoritarian";
                }
                else
                {
                    Narrator.Text += $"\n{captain.Name} the captain has {captain.HitPoints} health left";
                }

                if (HitPoints <= 0)
                {
                    CharacterDeath(this, PlayerCharacter);
                    Narrator.Text += $"\n{Name} the authoritarian clutches his would and dies a ignoble death";
                }
                else
                {
                    Narrator.Text += $"\n{Name} the authoritarian has {HitPoints} health left";
                }
            }
            
            updateCanvasandCharacterList();
        }

        public void respondCoquette(Coquette coquette)
        {
            if (HitPoints <= 0)
            {
                CharacterDeath(this, PlayerCharacter);
                Narrator.Text += $"\n{Name} the authoritarian clutches his would and dies a ignoble death";
            }
            else
            {
                Narrator.Text += $"\n{Name} responds to {coquette.Name} the coquette";
                Narrator.Text += $"\n{Name} the authoritarian becomes indignant and fines the coquette 10 of her money";
                Narrator.Text += $"\n{coquette.Name} the coquette is irate at the injustice and shanks {Name} the authoritarian for 40 health";
                if(coquette.Money>=10)
                {
                    Money += 10;

                }
                else
                {
                    Money += coquette.Money;
                }
                coquette.Money -= 10;
                if(coquette.Money<=0)
                {
                    CharacterDeath(coquette, PlayerCharacter);
                    Narrator.Text += $"\n{coquette.Name} the coquette has no money to pay the fine, and is jailed by the {Name} the authoritarian";
                }
                else
                {
                    Narrator.Text += $"\n{coquette.Name} the coquette has {coquette.Money} money left";
                }

                if (HitPoints <= 0)
                {
                    Narrator.Text += $"\n{Name} the athritarian falls to his knees and realizes he has been mortally wounded, he curses his fate and dies";
                    CharacterDeath(this, PlayerCharacter);
                }
                

            }

            updateCanvasandCharacterList();
        }

        public void respondInnocent(Innocent innocent)
        {
            if (HitPoints <= 0)
            {
                CharacterDeath(this, PlayerCharacter);
                Narrator.Text += $"\n {Name} the authoritarian cannot believe his eyes as he is mortally wounded by such a insignificiant creature. He dies";
            }
            else
            {
                Narrator.Text += $"\n {Name} the authoritarian cannot believe that he even has to respond to such an insignificiant creatur, he sweeps his arm and knocks his down for 50 points";
                if (innocent.HitPoints <= 0)
                {
                    CharacterDeath(innocent, PlayerCharacter);
                    Narrator.Text += $"\n {innocent.Name}, this fragile creature is easily broken by the authoritarian, he lies on the ground, bleeding to death while reciting Garcilaso";
                }
                else
                {
                    Narrator.Text += $"\n {innocent.Name} the innocent has {innocent.HitPoints} health left";
                }

            }
                            Narrator.Text += $"\n {Name} the authoritarian cannot believe his eyes as he is mortally wounded by such a insignificiant creature. He dies";
                updateCanvasandCharacterList();
        }

        public void respondPrecious(Precious precious)
        {
            if (HitPoints <= 0)
            {
                CharacterDeath(this, PlayerCharacter);
                Narrator.Text += $"\n{Name} the authoritarian clutches his would and dies a ignoble death";
            }
            else
            {
                Narrator.Text += $"\n{Name} responds to {precious.Name} the precious";
                Narrator.Text += $"\n{Name} the authoritarian is pleasantly suporised by the appearance of the precious, he gives her 50 of the money he is carrying";
                if (Money >= 50)
                {
                    Money -= 50;
                    precious.Money += 50;
                    Narrator.Text += $"\n{precious.Name} the precious is delighted by the gift and gives {Name} the authoritarian a hug, healing him for 10 points";

                }
                else
                {

                    CharacterDeath(this, PlayerCharacter);
                    Narrator.Text += $"\n{precious.Name} is not amused by the pautry gift and takes it as an insult. {Name} the authoritarian takes out his dagger and kills him self infront of her out of deep shame";
                }
            }
            updateCanvasandCharacterList();
        }
        public void provokeTrickster(Trickster trickster)
        {
            Narrator.Text = $"{Name} provoked {trickster.Name} the trickster";
            Narrator.Text +=$"\n{Name} cannot believe a scoudrel like {trickster.Name} is in his presence, he strikes him for 30 points";
            trickster.HitPoints -= 30;
            trickster.respondAuthoritarian(this);

        }

        public void provokeAuthoritarian(Authoritarian authoritarian)
        {
            Narrator.Text = $"{Name} provoked {authoritarian.Name} the authoritarian";
            Narrator.Text +=$"\n{Name} the authoritarian is threatened by the appearance of a rival in town. He approaches {authoritarian.Name} and strikes him for 20 points";
            authoritarian.HitPoints -= 20;
            authoritarian.respondAuthoritarian(this);
            
        }

        public void provokeCaptain(Captain captain)
        {
            Narrator.Text = $"{Name} provoked {captain.Name} the captain";
            Narrator.Text += $"\n{Name} the authoritarian is irked as the presence of this upstart miliraty man in the political scene, he makes up a false excuse to fine 40 of the captain's money and beats him with his walking stick";
            captain.Money -= 40;
            
            captain.HitPoints -= 10;
            if(captain.Money<0)
            {   
                CharacterDeath(captain, PlayerCharacter);
                Narrator.Text += $"\n{captain.Name} the captain has no money to pay the fine, and is jailed by the {Name} the authoritarian, his cries can be heared at night proclaiming his innocence";
                updateCanvasandCharacterList();

            }
            else
            {
                Money += 40;
                captain.respondAuthoritarian(this);

            }

        }

        public void provokeCoquette(Coquette coquette)
        {
            Narrator.Text = $"{Name} provoked {coquette.Name} the coquette";
            Narrator.Text += $"\n{Name} the authoritarian is morally scandalized by the presence or a immoral woman. He fines {coquette.Name} the coquette 40 of her money";
            if(coquette.Money>=100)
            {
                coquette.Money -= 40;
                Narrator.Text += $"\n{coquette.Name} has enough money to pay the fine and thinks nothing of it";
                Money += 40;
                updateCanvasandCharacterList();
            }
            else
            {
                Narrator.Text += $"\n{coquette.Name} thinks that the fine is way too much, and refused to pay.  She retaliates";
                coquette.respondAuthoritarian(this);

            }

        }

        public void provokeInnocent(Innocent innocent)
        {
            Narrator.Text = $"{Name} provoked {innocent.Name} the innocent";
            Narrator.Text += $"\n{Name} the authoritarian is disgusted by the presence of such a weakling in his presence. He strikes {innocent.Name} the innocent for 50 points";
            innocent.HitPoints -= 50;
            innocent.respondAuthoritarian(this);
        }

        public void provokePrecious(Precious precious)
        {
            Narrator.Text = $"{Name} provoked {precious.Name} the precious";
            Narrator.Text += $" \n{Name} the authoritarian is delighted by the presence of the precious, he gives her 50 of his money";
            if (Money >= 50)
            {
                Money -= 50;
                precious.Money += 50;
                Narrator.Text += $"\n{precious.Name} the precious is delighted by the gift and gives {Name} the authoritarian a hug, healing him for 10 points";
                updateCanvasandCharacterList();
            }
            else
            {
                Narrator.Text += $"\n{precious.Name} is not amused by the pautry gift and takes it as an insult.";
                precious.respondAuthoritarian(this);
            }
        }





    }
}
