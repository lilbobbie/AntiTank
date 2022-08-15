/*
  18/09/2018
  Wim Van Weyenberg
  SerialIO
  Als een 'A' ontvangen wordt gaat de LED op PIN13 aan en als een 'U' ontvangen wordt gaat de LED op PIN13 UIT
  Drukknop op PIN8 indien ingedrukt wordt een 'T' gestuurd

*/

char serialInputChar;
void setup()
{
  Serial.begin(9600);
  pinMode(8,INPUT_PULLUP);
  pinMode(9,INPUT_PULLUP);
  digitalWrite(13,LOW); //LED off
  pinMode(13,OUTPUT);
  
}

void loop()
{
  if (Serial.available())
  {
    serialInputChar = Serial.read();
    if (serialInputChar == 'A')
    {
      digitalWrite(13, HIGH);

    }
    if (serialInputChar == 'U')
    {
      digitalWrite(13, LOW);
    }
  }
  if (digitalRead(8) == LOW)
  {
    Serial.write('R');
    delay(50); //debounce
    while (digitalRead(8) == LOW);
    delay(50); //debounce
  }
   if (digitalRead(9) == LOW)
  {
    Serial.write('L');
    delay(50); //debounce
    while (digitalRead(9) == LOW);
    delay(50); //debounce
  }
}
