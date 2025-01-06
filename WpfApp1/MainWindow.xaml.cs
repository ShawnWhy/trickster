using System;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1;

namespace WpfApp1
{

            public enum CharacterType
        {
            Trickster,
            Authoritarian,
            Captain,
            Coquette,
            Innocent,
            Precious
        }
        //List of 20 common names in enum
        public enum CommonNames
        {
            Thomas,
            John,
            Jack,
            Jill,
            Jane,
            Jim,
            Joe,
            Jerry,
            Jake,
            Jeff,
            James,
            Jessica,
            Jennifer,
            Jason,
            Justin,
            Jordan,
            Josh,
            Jamie,
            Jay,
            Jaden
        }

    public abstract class Character
    {
        public string Name { get; set; }
        public decimal Money { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int HitPoints { get; set; }
        public int CharacterIndex { get; set; }
        public PlayerCharacter PlayerCharacter { get; set; }
        public ObservableCollection<Character> Characters { get; set; }
        public TextBox Narrator { get; set; }

        public Canvas Canvas { get; set; }

        public ListBox ItemsListBox { get; set; }

        protected Character(string name, decimal money, int positionX, int positionY, int hitPoints, ObservableCollection<Character> characters, TextBox narrator, PlayerCharacter playerCharacter, int characterIndex, Canvas canvas, ListBox listBox)
        {
            Name = name;
            Money = money;
            PositionX = positionX;
            PositionY = positionY;
            HitPoints = hitPoints;
            Characters = characters;
            Narrator = narrator;
            Canvas = canvas;
            ItemsListBox = listBox;
            CharacterIndex = characterIndex;
            if (playerCharacter != null)
            {
                PlayerCharacter = playerCharacter;
            }
            else playerCharacter = (PlayerCharacter)this;
        }

        public void CharacterDeath(Character character, PlayerCharacter playercharacter)
        {
            //give all of the money of the character from the list to the player character
            playercharacter.Money += character.Money;
            //remove the character from the list
            Characters.Remove(character);
            updateCanvasandCharacterList();
            

            
        }

        public void updateCanvasandCharacterList()
        {
            ItemsListBox.Items.Clear();
            Canvas.Children.Clear();
            foreach (var character in Characters)
            {
                // Assuming character class has properties Name, Money, and Hitpoints
                var details = $"{character.Name} the {character.GetType().Name} - Money: {character.Money}, Hitpoints: {character.HitPoints}";
                ItemsListBox.Items.Add(details);
                Image image = new Image();
                //image source
                image.Height = 50; //height of image auto
                image.Width = 50;
                //width of image auto
                image.Source = new BitmapImage(new Uri("pack://application:,,,/assets/" + character.GetType().Name + ".png"));
                image.Margin = new Thickness(character.PositionX, character.PositionY, 0, 0);

               image.Margin = new Thickness(character.PositionX, character.PositionY, 0, 0);
                //capture clicks in the eclips

                image.MouseUp += (sender, e) =>
                {
                    PlayerCharacter.Provoke(character);
                };
                Canvas.Children.Add(image);

                TextBlock textBlock = new TextBlock();
                textBlock.Text = character.Name + " the " + character.GetType().Name;
                textBlock.Margin = new Thickness(character.PositionX, character.PositionY, 0, 0);
                //add a id to the textblock
                //register name

                textBlock.Name = "textBlock" + CharacterIndex;
                Canvas.Children.Add(textBlock);

            }

        }

    
 

        public string toString()
        {
            return this.Name + this.Money;
        }

        public void Narrate(string text)
        {
            //display text in the narrator text box in the front end

            this.Narrator.Text = text;
        }

        public abstract void DisplayInfo();
    }

    public class PlayerCharacter : Trickster
    {
        public PlayerCharacter(string name, decimal money, int positionX, int positionY, int hitPoints, ObservableCollection<Character> characters, TextBox narrator, int characterIndex, Canvas canvas, ListBox characterList, PlayerCharacter playercharacter = null)
            : base(name, money, positionX, positionY, hitPoints, characters, narrator, playercharacter, characterIndex, canvas, characterList) { }

        public override void DisplayInfo()
        {
            Console.WriteLine($"playerCharacter: {Name}, Money: {Money}, Position: ({PositionX}, {PositionY}), Hit Points: {HitPoints}");
        }


        public void Respond()
        {
            if (this.Characters.Count == 0)
            {
                Console.WriteLine("No characters to provoke.");
                return;
            }

            //find the closest character in the character list
            Character character = this.Characters.OrderBy(c => Math.Abs(c.PositionX - PositionX) + Math.Abs(c.PositionY - PositionY)).First();
            //switch based on the character's class
        }
    }
    // array of created characters
    // array of created characters
    public class CharacterFactory
    {
        public static Character CreateCharacter(CharacterType type, string name, ObservableCollection<Character> characters, TextBox narrator, PlayerCharacter playercharacter, int characterIndex, Canvas canvas, ListBox ItemListBox)
        {
            //random numbers for positionx, positiony, hitpoints
            Random random = new Random();
            int positionX = random.Next(0, 500);
            int positionY = random.Next(0, 200);
            int hitPoints = random.Next(50, 200);
            decimal money = 100.0m;

            switch (type)
            {
                case CharacterType.Trickster:
                    return new Trickster(name, money, positionX, positionY, hitPoints, characters, narrator, playercharacter, characterIndex, canvas, ItemListBox );
                case CharacterType.Authoritarian:
                    return new Authoritarian(name, money, positionX, positionY, hitPoints, characters, narrator, playercharacter, characterIndex, canvas, ItemListBox);
                case CharacterType.Captain:
                    return new Captain(name, money, positionX, positionY, hitPoints, characters, narrator, playercharacter, characterIndex, canvas, ItemListBox);
                case CharacterType.Coquette:
                    return new Coquette(name, money, positionX, positionY, hitPoints, characters, narrator, playercharacter, characterIndex, canvas, ItemListBox);
                case CharacterType.Innocent:
                    return new Innocent(name, money, positionX, positionY, hitPoints, characters, narrator, playercharacter, characterIndex, canvas, ItemListBox);
                case CharacterType.Precious:
                    return new Precious(name, money, positionX, positionY, hitPoints, characters, narrator, playercharacter, characterIndex, canvas, ItemListBox);
                default:
                    return null;
            }
        }
    }


    // Interface IProvoke

    public interface IProvoke
    {
        void Provoke(Character character);

        void provokeTrickster(Trickster trickster);
        void provokeAuthoritarian(Authoritarian authoritarian);
        void provokeCaptain(Captain captain);
        void provokeCoquette(Coquette coquette);
        void provokeInnocent(Innocent innocent);
        void provokePrecious(Precious precious);

    }

    // Interface IRespond
    public interface IRespond
    {

        void respondTrickster(Trickster trickster);
        void respondAuthoritarian(Authoritarian authoritarian);
        void respondCaptain(Captain captain);
        void respondCoquette(Coquette coquette);
        void respondInnocent(Innocent innocent);
        void respondPrecious(Precious precious);

    }
    public partial class MainWindow : Window
    {
        public ObservableCollection<Character> Characters { get; set; }
        public PlayerCharacter playerCharacter { get; set; }
        public int characterIndex { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            narrator.Text = "Welcome to the game!";
            newName.Text = "";
            characterIndex = new int();
            Characters = new ObservableCollection<Character>();
            playerCharacter = new PlayerCharacter("The Trickster", 0m, 10, 20, 200, Characters, narrator, characterIndex, canvas, ItemsListBox);

            foreach (var character in Characters)
            {
                var details = $"{character.Name} - Money: {character.Money}, Hitpoints: {character.HitPoints}";
                ItemsListBox.Items.Add(details);
            }

            DataContext = this;

            selectType.ItemsSource = Enum.GetValues(typeof(CharacterType));
        }

        private void CreateCharacter_Click(object sender, RoutedEventArgs e)
        {
            foreach (var character in Characters)
            {
                if (character.Name == newName.Text)
                {
                    narrator.Text += "The name is already taken. Please choose a different name.";
                    return;
                }
            }

            if (newName.Text is null)
            {
                narrator.Text = "Please give a name to your creature";
            }
            else if (selectType.SelectedItem is null)
            {
                narrator.Text = "Please give a life path for the creature to go on";
            }
            else
            {
                CharacterType selectedType = (CharacterType)selectType.SelectedItem;
                characterIndex++;

                Characters.Add(CharacterFactory.CreateCharacter(selectedType, newName.Text, Characters, narrator, (PlayerCharacter)playerCharacter, characterIndex, canvas, ItemsListBox));
                ItemsListBox.Items.Clear();
                newName.Text = "";
                narrator.Text = "Character created!" + characterIndex;

                canvas.Children.Clear();
                foreach (var character in Characters)
                {
                    var details = $"{character.Name} the {character.GetType().Name} - Money: {character.Money}, Hitpoints: {character.HitPoints}";
                    ItemsListBox.Items.Add(details);
                    //new image 
                    Image image = new Image();
                    //image source
                    image.Height = 50; //height of image auto
                    image.Width = 50;
                    //width of image auto
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/assets/" + character.GetType().Name + ".png"));
                    image.Margin = new Thickness(character.PositionX, character.PositionY, 0, 0); 


                    /*                    Ellipse ellipse = new Ellipse();
                                        ellipse.Width = 50;
                                        ellipse.Height = 50;

                                        switch (character.GetType().Name)
                                        {
                                            case "Trickster":
                                                ellipse.Fill = Brushes.Green;
                                                break;
                                            case "Authoritarian":
                                                ellipse.Fill = Brushes.Red;
                                                break;
                                            case "Captain":
                                                ellipse.Fill = Brushes.Yellow;
                                                break;
                                            case "Coquette":
                                                ellipse.Fill = Brushes.Pink;
                                                break;
                                            case "Innocent":
                                                ellipse.Fill = Brushes.Purple;
                                                break;
                                            case "Precious":
                                                ellipse.Fill = Brushes.Blue;
                                                break;
                                        }

                                        ellipse.Stroke = Brushes.Black;
                                        ellipse.StrokeThickness = 1;

                                        ellipse.Name = "ellipse" + characterIndex;
                                        ellipse.Margin = new Thickness(character.PositionX, character.PositionY, 0, 0);*/

                    image.MouseUp += (sender, e) =>
                    {
                        playerCharacter.Provoke(character);
                    };
                    canvas.Children.Add(image);

                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = character.Name + " the " + character.GetType().Name;
                    textBlock.Margin = new Thickness(character.PositionX, character.PositionY, 0, 0);

                    textBlock.Name = "textBlock" + characterIndex;
                    canvas.Children.Add(textBlock);
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            narrator.Text = "Adding a character";
            Random random = new Random();
            int randomType = random.Next(0, 5);
            int randomName = random.Next(0, 20);
            characterIndex++;

            Characters.Add(CharacterFactory.CreateCharacter(
                (CharacterType)randomType,
                Enum.GetName(typeof(CommonNames), randomName),
                Characters, narrator, (PlayerCharacter)playerCharacter, characterIndex, canvas, ItemsListBox
            ));

            ItemsListBox.Items.Clear();
            foreach (var character in Characters)
            {
                var details = $"{character.Name} - Money: {character.Money}, Hitpoints: {character.HitPoints}";
                ItemsListBox.Items.Add(details);
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ItemsListBox.SelectedItem != null)
            {
                var selectedName = ItemsListBox.SelectedItem.ToString().Split(' ')[0];
                var characterToRemove = Characters.FirstOrDefault(x => x.Name == selectedName);

                if (characterToRemove != null)
                {
                    Characters.Remove(characterToRemove);
                    narrator.Text += "Removed " + characterToRemove.Name + " from the game.";
                    narrator.Text += "the number associated with the character is " + characterToRemove.Name;
                    ItemsListBox.Items.Clear();
                    canvas.Children.Clear();

                    foreach (var character in Characters)
                    {
                        var details = $"{character.Name} the {character.GetType().Name} - Money: {character.Money}, Hitpoints: {character.HitPoints}";
                        ItemsListBox.Items.Add(details);

                        Ellipse ellipse = new Ellipse();
                        ellipse.Width = 50;
                        ellipse.Height = 50;

                        switch (character.GetType().Name)
                        {
                            case "Trickster":
                                ellipse.Fill = Brushes.Green;
                                break;
                            case "Authoritarian":
                                ellipse.Fill = Brushes.Red;
                                break;
                            case "Captain":
                                ellipse.Fill = Brushes.Yellow;
                                break;
                            case "Coquette":
                                ellipse.Fill = Brushes.Pink;
                                break;
                            case "Innocent":
                                ellipse.Fill = Brushes.Purple;
                                break;
                            case "Precious":
                                ellipse.Fill = Brushes.Blue;
                                break;
                        }

                        ellipse.Stroke = Brushes.Black;
                        ellipse.StrokeThickness = 1;

                        ellipse.Name = "ellipse" + characterIndex;
                        ellipse.Margin = new Thickness(character.PositionX, character.PositionY, 0, 0);

                        ellipse.MouseUp += (sender, e) =>
                        {
                            playerCharacter.Provoke(character);
                        };
                        canvas.Children.Add(ellipse);

                        TextBlock textBlock = new TextBlock();
                        textBlock.Text = character.Name + " the " + character.GetType().Name;
                        textBlock.Margin = new Thickness(character.PositionX, character.PositionY, 0, 0);

                        textBlock.Name = "textBlock" + characterIndex;
                        canvas.Children.Add(textBlock);
                    }
                }
            }
        }
    }

        //when button is clicked and name is entered, add character to the list


    }


