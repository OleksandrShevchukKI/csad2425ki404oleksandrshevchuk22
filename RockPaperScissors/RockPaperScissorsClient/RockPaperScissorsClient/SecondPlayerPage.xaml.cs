namespace RockPaperScissorsClient;

public partial class SecondPlayerPage : ContentPage
{
    private MainPage _mainPage;

    public SecondPlayerPage(MainPage mainPage)
    {
        InitializeComponent();
        _mainPage = mainPage;
    }

    private void ChoiceButton_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        string choice = button.CommandParameter.ToString();
        _mainPage.SetSecondPlayerChoice(choice);
        Navigation.PopModalAsync();
    }
}