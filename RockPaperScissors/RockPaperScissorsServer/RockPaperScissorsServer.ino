#include <Arduino.h>

String choices[] = {"Rock", "Paper", "Scissors"};

void setup() {
  Serial.begin(9600);
}

String getRandomChoice() {
  int index = random(3);
  return choices[index];
}

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

void loop() {
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
