using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Ports;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Xml.Linq;

namespace RockPaperScissorsClient
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private SerialPort _serialPort;
        private string[] _choices;
        private string _playMode;
        private string _firstPlayerChoice;
        private string _secondPlayerChoice;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainPageViewModel()
        {
            LoadConfiguration();
            InitializeSerialPort();
            PlayCommand = new Command(Play);
            SaveGameCommand = new Command(SaveGame);
            LoadGameCommand = new Command(LoadGame);
            NewGameCommand = new Command(NewGame);
        }

        public ObservableCollection<string> Choices { get; set; } = new ObservableCollection<string>();

        public ObservableCollection<string> PlayModes { get; set; } = new ObservableCollection<string>
        {
            "ManVsAI", "ManVsMan", "AIvsAIRandom", "AIvsAIWinStrategy"
        };

        public string PlayMode
        {
            get => _playMode;
            set
            {
                _playMode = value;
                OnPropertyChanged();
            }
        }

        public string FirstPlayerChoice
        {
            get => _firstPlayerChoice;
            set
            {
                _firstPlayerChoice = value;
                OnPropertyChanged();
            }
        }

        public string SecondPlayerChoice
        {
            get => _secondPlayerChoice;
            set
            {
                _secondPlayerChoice = value;
                OnPropertyChanged();
            }
        }

        public string Result { get; set; }

        public ICommand PlayCommand { get; }
        public ICommand SaveGameCommand { get; }
        public ICommand LoadGameCommand { get; }
        public ICommand NewGameCommand { get; }

        private void LoadConfiguration()
        {
            _choices = new[] { "Rock", "Paper", "Scissors" };
            PlayMode = "ManVsAI";

            foreach (var choice in _choices)
            {
                Choices.Add(choice);
            }
            _serialPort = new SerialPort("COM5", 9600);
            _serialPort.DataReceived += SerialPort_DataReceived;
        }

        private void InitializeSerialPort()
        {
            if (!_serialPort.IsOpen)
            {
                _serialPort.Open();
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = _serialPort.ReadLine();
            MainThread.BeginInvokeOnMainThread(() => ProcessResponse(data));
        }

        public void Play()
        {
            if (PlayMode == "ManVsAI" && !string.IsNullOrEmpty(FirstPlayerChoice))
            {
                _serialPort.WriteLine($"{FirstPlayerChoice}");
            }
            else if (PlayMode == "ManVsMan" && !string.IsNullOrEmpty(FirstPlayerChoice) && !string.IsNullOrEmpty(SecondPlayerChoice))
            {
                _serialPort.WriteLine($"{FirstPlayerChoice},{SecondPlayerChoice},ManVsMan");
            }
            else if (PlayMode == "AIvsAIRandom")
            {
                _serialPort.WriteLine("AIvsAIRandom");
            }
            else if (PlayMode == "AIvsAIWinStrategy")
            {
                _serialPort.WriteLine("AIvsAIWinStrategy");
            }
        }

        private void ProcessResponse(string data)
        {
            string[] parts = data.Split(',');
            if (parts.Length == 4)
            {
                string mode = parts[0];
                string firstChoice = parts[1];
                string secondChoice = parts[2];
                string result = parts[3];
                Result = $"Play mode: {mode}\nFirst Player chose: {firstChoice}\nSecond Player chose: {secondChoice}\nResult: {result}";
            }
            else
            {
                Result = data;
            }
            OnPropertyChanged(nameof(Result));
        }

        public void SaveGame()
        {
            var saveData = new XDocument(
                new XElement("GameData",
                    new XElement("FirstPlayerChoice", FirstPlayerChoice),
                    new XElement("SecondPlayerChoice", SecondPlayerChoice),
                    new XElement("PlayMode", PlayMode)
                )
            );
            string savedGameFilePath = Path.Combine(FileSystem.AppDataDirectory, "saved_game.xml");
            saveData.Save(savedGameFilePath);
        }

        public void LoadGame()
        {
            string savedGameFilePath = Path.Combine(FileSystem.AppDataDirectory, "saved_game.xml");
            XDocument savedGame = XDocument.Load(savedGameFilePath);
            var gameData = savedGame.Element("GameData");

            FirstPlayerChoice = gameData.Element("FirstPlayerChoice").Value;
            SecondPlayerChoice = gameData.Element("SecondPlayerChoice").Value;
            PlayMode = gameData.Element("PlayMode").Value;

            Result = $"Loaded Game:\nPlay Mode: {PlayMode}\nFirst Player: {FirstPlayerChoice}\nSecond Player: {SecondPlayerChoice}";
            OnPropertyChanged(nameof(Result));
        }

        public void NewGame()
        {
            FirstPlayerChoice = string.Empty;
            SecondPlayerChoice = string.Empty;
            PlayMode = "ManVsAI";
            Result = string.Empty;
            OnPropertyChanged(nameof(FirstPlayerChoice));
            OnPropertyChanged(nameof(SecondPlayerChoice));
            OnPropertyChanged(nameof(PlayMode));
            OnPropertyChanged(nameof(Result));
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
