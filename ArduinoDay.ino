int data;
int value;
void setup()
{
  Serial.begin(9600);
  pinMode(13,OUTPUT);
}

void loop()
{

  //Reading data from Unity
  if(Serial.available()){
    data = Serial.read();
    if(data=='1'){
      digitalWrite(13,HIGH);
    }
    if(data=='0'){
      digitalWrite(13,LOW);
    }
  }

  //Send data to Unity
 if(digitalRead(7) == 1)
  {
    Serial.println(300);
    Serial.flush();
  }

 if(digitalRead(4) == 1)
  {
    Serial.println(400);
    Serial.flush();
  }
    value = map(analogRead(0),0,1023,0,360);
    Serial.println(value);
    Serial.flush();
    delay(200);
}
