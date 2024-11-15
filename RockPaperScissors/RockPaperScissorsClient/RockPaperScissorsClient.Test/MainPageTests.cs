using RockPaperScissorsClient;

namespace RockPaperScissorsClient.Test
{
    public class Tests
    {
        private MainPageViewModel _viewModel;

        [SetUp]
        public void Setup()
        {
            _viewModel = new MainPageViewModel();
        }

        [Test]
        public void TestChoicesInitialization()
        {
            Assert.AreEqual(3, _viewModel.Choices.Count);
            Assert.Contains("Rock", _viewModel.Choices);
            Assert.Contains("Paper", _viewModel.Choices);
            Assert.Contains("Scissors", _viewModel.Choices);
        }

        [Test]
        public void TestSetPlayMode()
        {
            _viewModel.PlayMode = "ManVsMan";
            Assert.AreEqual("ManVsMan", _viewModel.PlayMode);
        }

        [Test]
        public void TestSetFirstPlayerChoice()
        {
            _viewModel.FirstPlayerChoice = "Rock";
            Assert.AreEqual("Rock", _viewModel.FirstPlayerChoice);
        }

        [Test]
        public void TestSetSecondPlayerChoice()
        {
            _viewModel.SecondPlayerChoice = "Paper";
            Assert.AreEqual("Paper", _viewModel.SecondPlayerChoice);
        }

        [Test]
        public void TestNewGame()
        {
            _viewModel.NewGame();
            Assert.AreEqual(string.Empty, _viewModel.FirstPlayerChoice);
            Assert.AreEqual(string.Empty, _viewModel.SecondPlayerChoice);
            Assert.AreEqual("ManVsAI", _viewModel.PlayMode);
        }
    }
}