int ANALOG_IN_PIN = 33;

float adc_voltage = 0.0;
float in_voltage = 0.0;

float R1 = 30000.0;
float R2 = 7500.0; 
 
float ref_voltage = 5.0;
int adc_value = 0;



#include <WiFi.h>

const char* ssid     = "G1S3";
const char* password = "12345687";

WiFiServer server(80);

void setup()
{
    Serial.begin(115200);
    pinMode(ANALOG_IN_PIN, INPUT);

    delay(10);

    Serial.println();
    Serial.println();
    Serial.print("Connecting to ");
    Serial.println(ssid);

    WiFi.begin(ssid, password);

    while (WiFi.status() != WL_CONNECTED) {
        delay(500);
        Serial.print(".");
    }

    Serial.println("");
    Serial.println("WiFi connected.");
    Serial.println("IP address: ");
    Serial.println(WiFi.localIP());
    
    server.begin();

}

int value = 0;

void loop(){
   adc_value = analogRead(ANALOG_IN_PIN);
   adc_voltage  = (adc_value * ref_voltage) / 1024.0; 
   in_voltage = adc_voltage / (R2/(R1+R2)) ; 
  Serial.println(in_voltage, 2);
 
 WiFiClient client = server.available();

  if (client) {
    Serial.println("New Client.");
    String currentLine = "";
    while (client.connected()) {
      if (client.available()) {
        char c = client.read();
        Serial.write(c);
        if (c == '\n') {



          if (currentLine.length() == 0) {


            client.println("HTTP/1.1 200 OK");
            client.println("Content-type:text/html");
            client.println();


            client.print("<div><p>");
            client.print(in_voltage);
            client.print("</p></div>");

            client.println();

            break;
          } else {
            currentLine = "";
          }
        } else if (c != '\r') {
          currentLine += c;
        }
      }
    }
    delay(500);
    client.stop();
    Serial.println("Client Disconnected.");
  }
}
