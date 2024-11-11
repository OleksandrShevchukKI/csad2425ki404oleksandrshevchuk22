namespace RockPaperScissorsClient;

/// <summary>
/// The page for the second player to make a choice.
/// </summary>
public partial class SecondPlayerPage : ContentPage
{
    private MainPage _mainPage;

    /// <summary>
    /// Initializes a new instance of the <see cref="SecondPlayerPage"/> class.
    /// </summary>
    /// <param name="mainPage">The main page of the application.</param>
    public SecondPlayerPage(MainPage mainPage)
    {
        InitializeComponent();
        _mainPage = mainPage;
    }

    /// <summary>
    /// Handles the choice button click event.
    /// </summary>
    private void ChoiceButton_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        string choice = button.CommandParameter.ToString();
        _mainPage.SetSecondPlayerChoice(choice);
        Navigation.PopModalAsync();
    }
}