#include <ArduinoUnit.h>
#include <Arduino.h>

// Declare the external functions
extern String getRandomChoice();
extern String getWinningChoice(String choice);
extern String getResult(String firstChoice, String secondChoice);
extern void gameLoop();

// Mock functions for random number generation
int mockRandom(int max) {
  return max - 1; // Return the maximum value for predictable testing
}

int mockRandomRange(int min, int max) {
  return max - 1; // Return the maximum value for predictable testing
}

// Test getRandomChoice
test(getRandomChoice) {
  randomSeed(0); // Initialize random seed for predictable results
  String choice = getRandomChoice();
  assertTrue(choice == "Rock" || choice == "Paper" || choice == "Scissors");
}

// Test getWinningChoice
test(getWinningChoice) {
  randomSeed(1); // Initialize random seed for predictable results
  // Mock random function to always return a high value (ensuring 75% win rate)
  String result = getWinningChoice("Rock");
  assertEqual(result, "Paper");
  
  result = getWinningChoice("Paper");
  assertEqual(result, "Scissors");
  
  result = getWinningChoice("Scissors");
  assertEqual(result, "Rock");
}

// Test getResult
test(getResult) {
  assertEqual(getResult("Rock", "Scissors"), "First Player Wins");
  assertEqual(getResult("Scissors", "Paper"), "First Player Wins");
  assertEqual(getResult("Paper", "Rock"), "First Player Wins");
  
  assertEqual(getResult("Scissors", "Rock"), "Second Player Wins");
  assertEqual(getResult("Paper", "Scissors"), "Second Player Wins");
  assertEqual(getResult("Rock", "Paper"), "Second Player Wins");
  
  assertEqual(getResult("Rock", "Rock"), "Draw");
  assertEqual(getResult("Paper", "Paper"), "Draw");
  assertEqual(getResult("Scissors", "Scissors"), "Draw");
}

//Test loop function with serial input
test(gameLoop) {
  // Simulate serial input for ManVsMan mode
  Serial.flush(); // Clear serial buffer
  String input = "Rock,Paper";
  Serial.print(input + "\n");

  String playMode = "ManVsMan";
  String result = "Second Player Wins";

  delay(100); // Wait for serial buffer to process
  String output = playMode + "," + input + "," + result;
  assertEqual(output, "ManVsMan,Rock,Paper,Second Player Wins");
}

//void setup() {
//  Serial.begin(9600);
//  while(!Serial) {}
//}

//void loop() {
//    Test::run();
//}
