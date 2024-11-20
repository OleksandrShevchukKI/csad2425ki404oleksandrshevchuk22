/**
 * @file RockPaperScissors.ino
 * @brief A Rock-Paper-Scissors game implementation for Arduino.
 * 
 * This code implements a Rock-Paper-Scissors game where the player can 
 * play against the computer, or the computer can simulate games with different strategies.
 */

#include <Arduino.h>

/// Choices for the Rock-Paper-Scissors game.
String choices[] = {"Rock", "Paper", "Scissors"};


/**
 * @brief Get a random choice from Rock, Paper, and Scissors.
 * 
 * @return A randomly selected choice as a String.
 */
String getRandomChoice() {
  int index = random(3);
  return choices[index];
}

/**
 * @brief Get the winning choice against the provided choice with a 75% probability.
 * 
 * @param choice The choice against which to find the winning choice.
 * @return A String representing the winning choice.
 */
String getWinningChoice(String choice) {
  int chance = random(100);
  if (chance < 75) {
    if (choice == "Rock") return "Paper";
    if (choice == "Paper") return "Scissors";
    return "Rock";
  } else {
    return getRandomChoice();
  }
}

/**
 * @brief Determine the result of a Rock-Paper-Scissors game.
 * 
 * @param firstChoice The choice of the first player.
 * @param secondChoice The choice of the second player.
 * @return A String representing the result of the game.
 */
String getResult(String firstChoice, String secondChoice) {
  if (firstChoice == secondChoice) return "Draw";
  if ((firstChoice == "Rock" && secondChoice == "Scissors") ||
      (firstChoice == "Scissors" && secondChoice == "Paper") ||
      (firstChoice == "Paper" && secondChoice == "Rock")) {
    return "First Player Wins";
  } else {
    return "Second Player Wins";
  }
}

void gameLoop() {
  if (Serial.available() > 0) {
    String input = Serial.readStringUntil('\n');
    input.trim();

    String firstChoice, secondChoice, playMode, inputPlayMode;
    int firstSeparatorIndex = input.indexOf(',');
    int secondSeparatorIndex = input.indexOf(',', firstSeparatorIndex + 1);

    if (firstSeparatorIndex > 0 && secondSeparatorIndex > firstSeparatorIndex) {
      firstChoice = input.substring(0, firstSeparatorIndex);
      secondChoice = input.substring(firstSeparatorIndex + 1, secondSeparatorIndex);
      playMode = "ManVsMan";
      inputPlayMode = input.substring(0, firstSeparatorIndex);
    } else {
      firstChoice = input;
      inputPlayMode = input;
      secondChoice = getRandomChoice();
      playMode = "ManVsAI";
    }

    if (inputPlayMode == "AIvsAIWinStrategy") {
      firstChoice = getRandomChoice();
      secondChoice = getWinningChoice(firstChoice);
      playMode = "AIvsAIWinStrategy";
    } else if (inputPlayMode == "AIvsAIRandom") {
      firstChoice = getRandomChoice();
      secondChoice = getRandomChoice();
      playMode = "AIvsAIRandom";
    }

    String result = getResult(firstChoice, secondChoice);
    Serial.println(playMode + "," + firstChoice + "," + secondChoice + "," + result);
  }
}

void setup() {
Serial.begin(9600);
}

void loop(){
gameLoop();
}