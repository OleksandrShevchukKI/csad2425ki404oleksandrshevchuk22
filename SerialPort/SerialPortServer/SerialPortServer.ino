void setup() {
  Serial.begin(9600);               // Ініціалізуємо серійну комунікацію
  pinMode(12, OUTPUT);              // Встановлюємо 12 пін як вихід для отримання
  pinMode(13, OUTPUT);              // Встановлюємо 13 пін як вихід для надсилання
}

void loop() {
  if (Serial.available() > 0) {
   
    digitalWrite(12, HIGH);
    String message = Serial.readStringUntil('\n');
    
    
    digitalWrite(12, LOW);
   

    digitalWrite(13, HIGH);
    Serial.println(message);
    
    
    digitalWrite(13, LOW);

  }
}