using NUnit.Framework;

namespace RockPaperScissorsClient.Test
{
    public class Tests
    {
        private MainPageViewModel _viewModel;
        private MockSerialPortService _mockSerialPortService;

        [SetUp]
        public void Setup()
        {
            _mockSerialPortService = new MockSerialPortService();
            _viewModel = new MainPageViewModel(_mockSerialPortService);
        }

        [Test]
        public void TestChoicesInitialization()
        {
            Assert.That(_viewModel.Choices.Count, Is.EqualTo(3));
            Assert.Contains("Rock", _viewModel.Choices);
            Assert.Contains("Paper", _viewModel.Choices);
            Assert.Contains("Scissors", _viewModel.Choices);
        }

        [Test]
        public void TestSetPlayMode()
        {
            _viewModel.PlayMode = "ManVsMan";
            Assert.That(_viewModel.PlayMode, Is.EqualTo("ManVsMan"));
        }

        [Test]
        public void TestSetFirstPlayerChoice()
        {
            _viewModel.FirstPlayerChoice = "Rock";
            Assert.That(_viewModel.FirstPlayerChoice, Is.EqualTo("Rock"));
        }

        [Test]
        public void TestSetSecondPlayerChoice()
        {
            _viewModel.SecondPlayerChoice = "Paper";
            Assert.That(_viewModel.SecondPlayerChoice, Is.EqualTo("Paper"));
        }

        [Test]
        public void TestNewGame()
        {
            _viewModel.NewGame();
            Assert.That(_viewModel.FirstPlayerChoice, Is.EqualTo(string.Empty));
            Assert.That(_viewModel.SecondPlayerChoice, Is.EqualTo(string.Empty));
            Assert.That(_viewModel.PlayMode, Is.EqualTo("ManVsAI"));
        }

        [Test]
        public void TestPlayManVsAI()
        {
            var response = "ManVsAI,Rock,Scissors,Win";
            _viewModel.FirstPlayerChoice = "Rock";
            _viewModel.PlayMode = "ManVsAI";
            _mockSerialPortService.AddResponse(response);
            _viewModel.Play();
            _mockSerialPortService.InvokeDataReceived(new CustomSerialDataReceivedEventArgs(response));

            Assert.That(_mockSerialPortService.ReadLine(), Is.EqualTo(response));
        }
    }
}
