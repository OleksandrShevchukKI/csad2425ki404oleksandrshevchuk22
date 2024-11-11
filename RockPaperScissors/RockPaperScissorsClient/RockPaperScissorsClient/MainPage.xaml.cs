using System.IO.Ports;
using System.Xml.Linq;

namespace RockPaperScissorsClient
{
    /// <summary>
    /// The main page of the RockPaperScissorsClient application.
    /// </summary>
    public partial class MainPage : ContentPage
    {
        private SerialPort _serialPort;
        private string[] _choices;
        private string _playMode;
        private string _firstPlayerChoice;
        private string _secondPlayerChoice;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            LoadConfiguration();
            InitializeSerialPort();
            InitializePickers();
        }

        /// <summary>
        /// Gets the choices available for the game.
        /// </summary>
        public string[] Choices => _choices;

        /// <summary>
        /// Loads the configuration from the config.xml file.
        /// </summary>
        private void LoadConfiguration()
        {
            string configFilePath = Path.Combine(FileSystem.AppDataDirectory, "config.xml");
            XDocument config = XDocument.Load(configFilePath);
            var gameConfig = config.Element("configuration").Element("game");
            _choices = gameConfig.Element("choices").Elements("choice").Select(e => e.Value).ToArray();
            string port = gameConfig.Element("serialPort").Value;
            int baudRate = int.Parse(gameConfig.Element("baudRate").Value);
            _playMode = gameConfig.Element("playMode").Value;

            _serialPort = new SerialPort(port, baudRate);
            _serialPort.DataReceived += SerialPort_DataReceived;
        }

        /// <summary>
        /// Initializes the pickers with the game choices and play modes.
        /// </summary>
        private void InitializePickers()
        {
            foreach (var choice in _choices)
            {
                UserChoicePicker.Items.Add(choice);
            }

            if (_playMode == "ManVsAI")
            {
                PlayModePicker.SelectedIndex = 0;
            }
            else if (_playMode == "ManVsMan")
            {
                PlayModePicker.SelectedIndex = 1;
            }
            else if (_playMode == "AIvsAIRandom")
            {
                PlayModePicker.SelectedIndex = 2;
            }
            else if (_playMode == "AIvsAIWinStrategy")
            {
                PlayModePicker.SelectedIndex = 3;
            }
        }

        /// <summary>
        /// Initializes the serial port.
        /// </summary>
        private void InitializeSerialPort()
        {
            _serialPort.Open();
        }

        /// <summary>
        /// Handles the data received from the serial port.
        /// </summary>
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = _serialPort.ReadLine();
            MainThread.BeginInvokeOnMainThread(() => ProcessResponse(data));
        }

        /// <summary>
        /// Handles the Play button click event.
        /// </summary>
        private void PlayButton_Click(object sender, EventArgs e)
        {
            if (PlayModePicker.SelectedIndex == 0 && UserChoicePicker.SelectedIndex != -1)
            {
                string userChoice = UserChoicePicker.Items[UserChoicePicker.SelectedIndex];
                _serialPort.WriteLine($"{userChoice}");
            }
            else if (PlayModePicker.SelectedIndex == 1 && UserChoicePicker.SelectedIndex != -1)
            {
                _firstPlayerChoice = UserChoicePicker.Items[UserChoicePicker.SelectedIndex];
                var secondPlayerPage = new SecondPlayerPage(this);
                Navigation.PushModalAsync(secondPlayerPage);
            }
            else if (PlayModePicker.SelectedIndex == 2)
            {
                _serialPort.WriteLine("AIvsAIRandom");
            }
            else if (PlayModePicker.SelectedIndex == 3)
            {
                _serialPort.WriteLine("AIvsAIWinStrategy");
            }
            else
            {
                DisplayAlert("Error", "Please select a play mode and your move.", "OK");
            }
        }

        /// <summary>
        /// Sets the choice of the second player.
        /// </summary>
        public void SetSecondPlayerChoice(string choice)
        {
            _secondPlayerChoice = choice;
            _serialPort.WriteLine($"{_firstPlayerChoice},{_secondPlayerChoice},ManVsMan");
        }

        /// <summary>
        /// Processes the response received from the serial port.
        /// </summary>
        private void ProcessResponse(string data)
        {
            string[] parts = data.Split(',');
            if (parts.Length == 4)
            {
                string mode = parts[0];
                string firstChoice = parts[1];
                string secondChoice = parts[2];
                string result = parts[3];
                ResultLabel.Text = $"Play mode: {mode}\nFirst Player chose: {firstChoice}\nSecond Player chose: {secondChoice}\nResult: {result}";
            }
            else
            {
                ResultLabel.Text = data;
            }
        }

        /// <summary>
        /// Handles the New Game button click event.
        /// </summary>
        private void NewGameButton_Click(object sender, EventArgs e)
        {
            ResultLabel.Text = string.Empty;
            UserChoicePicker.SelectedIndex = -1;
        }

        /// <summary>
        /// Handles the Save button click event.
        /// </summary>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveGame();
        }

        /// <summary>
        /// Saves the game state to a file.
        /// </summary>
        private void SaveGame()
        {
            var saveData = new XDocument(
                new XElement("GameData",
                    new XElement("FirstPlayerChoice", _firstPlayerChoice),
                    new XElement("SecondPlayerChoice", _secondPlayerChoice),
                    new XElement("PlayMode", _playMode)
                )
            );
            string savedGameFilePath = Path.Combine(FileSystem.AppDataDirectory, "saved_game.xml");
            saveData.Save(savedGameFilePath);
        }

        /// <summary>
        /// Handles the Load button click event.
        /// </summary>
        private void LoadButton_Click(object sender, EventArgs e)
        {
            LoadGame();
        }

        /// <summary>
        /// Loads the game state from a file.
        /// </summary>
        private void LoadGame()
        {
            string savedGameFilePath = Path.Combine(FileSystem.AppDataDirectory, "saved_game.xml");
            XDocument savedGame = XDocument.Load(savedGameFilePath);
            var gameData = savedGame.Element("GameData");

            _firstPlayerChoice = gameData.Element("FirstPlayerChoice").Value;
            _secondPlayerChoice = gameData.Element("SecondPlayerChoice").Value;
            _playMode = gameData.Element("PlayMode").Value;

            var firstPlayerIndex = Array.IndexOf(_choices, _firstPlayerChoice);
            if (firstPlayerIndex >= 0)
            {
                UserChoicePicker.SelectedIndex = firstPlayerIndex;
            }

            ResultLabel.Text = $"Loaded Game:\nPlay Mode: {_playMode}\nFirst Player: {_firstPlayerChoice}\nSecond Player: {_secondPlayerChoice}";
        }
    }
}
