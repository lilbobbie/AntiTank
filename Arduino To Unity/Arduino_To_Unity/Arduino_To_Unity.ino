int ledPin1 = 12;
int ledPin2 = 11;
int ledPin3 = 10;

int butPin1F = 7; 
int butPin2L = 6; 
int butPin3B = 5;
int butPin4R = 4;

int buzzPin1 = 3;
char serialInputChar;

void setup() {
  Serial.begin(9600);

  pinMode(butPin1F, INPUT);
  pinMode(butPin2L, INPUT);
  pinMode(butPin3B, INPUT);
  pinMode(butPin4R, INPUT);
  
  pinMode(buzzPin1, OUTPUT);

  pinMode(ledPin1, OUTPUT);
  pinMode(ledPin2, OUTPUT);
  pinMode(ledPin3, OUTPUT);




  digitalWrite(butPin1F, HIGH);
  digitalWrite(butPin2L, HIGH);
  digitalWrite(butPin3B, HIGH);
  digitalWrite(butPin4R, HIGH);
}

void loop() {
  serialInputChar = Serial.read();
  
  if (digitalRead(butPin1F) == LOW) {
    Serial.println("FORWARD");
    Serial.write(1);
    Serial.flush();
    delay(20);
  }

  if (digitalRead(butPin2L) == LOW) {
    Serial.println("LEFT");
    Serial.write(2);
    Serial.flush();
    delay(20);
  }

   if (digitalRead(butPin3B) == LOW) {
    Serial.println("BACK");
    Serial.write(3);
    Serial.flush();
    delay(20);
  }

   if (digitalRead(butPin4R) == LOW) {
    Serial.println("RIGHT");
    Serial.write(4);
    Serial.flush();
    delay(20);
  }
   if (serialInputChar == 'H'){
    tone(buzzPin1,50);
    delay(25);
    noTone(buzzPin1);
 }

   if (serialInputChar == 'F'){
    digitalWrite(ledPin1, HIGH);
    digitalWrite(ledPin2, HIGH);
    digitalWrite(ledPin3, HIGH);
 }

   if (serialInputChar == 'M'){
    digitalWrite(ledPin1, LOW);
    digitalWrite(ledPin2, HIGH);
    digitalWrite(ledPin3, HIGH);
 }
   if (serialInputChar == 'L'){
    digitalWrite(ledPin1, LOW);
    digitalWrite(ledPin2, LOW);
    digitalWrite(ledPin3, HIGH);
 }
  if (serialInputChar == 'E'){
    digitalWrite(ledPin1, LOW);
    digitalWrite(ledPin2, LOW);
    digitalWrite(ledPin3, LOW);
  }
  

}
