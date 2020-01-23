#include <ESP8266WiFi.h>
#include <DHT.h>
#include <DHT_U.h>

#include "sensor.h"
#include "arduino_dht_client.h"
#include "arduino_mqtt_client.h"
#include "uuid_gen.h"

#define DHTPIN D5
#define DHTTYPE DHT11

#define WLAN_SSID ""
#define WLAN_PASS ""
#define CONNECTION_ATTEMPTS 20
#define SLEEP_TIME_SECONDS 900

#define MQTT_SERVER ""
#define MQTT_USERNAME ""
#define MQTT_PASS ""

WiFiClient client;


ArduinoDHTClient dhtClient(DHTPIN, DHTTYPE);
ArduinoMQTTClient mqttClient(&client, MQTT_SERVER, 1883, MQTT_USERNAME, MQTT_PASS);

Sensor s(generateUUID(std::string(WiFi.macAddress().c_str())), "temp+hum", &dhtClient, &mqttClient);

bool connect() {
  Serial.print("checking wifi...");
  int try_number = 0;
  
  WiFi.mode(WIFI_STA);
  WiFi.begin(WLAN_SSID, WLAN_PASS);
  while (WiFi.status() != WL_CONNECTED) {
    Serial.print(".");
    delay(1000);
    try_number++;
    if (try_number >= CONNECTION_ATTEMPTS) {
      return false;
    }
  }

  Serial.println("");
  Serial.println("WiFi connected");
  Serial.println("IP address: ");
  Serial.println(WiFi.localIP());

  Serial.print("\nconnecting...");
  return true;
}

void deepSleep(int seconds) {
  ESP.deepSleep(seconds * 1000000);
}

void setup() {
  Serial.begin(115200);
  Serial.setTimeout(2000);

  Serial.println("Device wake up");

  if (!connect()) {
    Serial.println("Could not connect to WIFI, sleeping...");
    deepSleep(SLEEP_TIME_SECONDS);
  }
  s.setup();
  if (!mqttClient.connect()) {
    Serial.println("Could not connect to WIFI, sleeping...");
    deepSleep(SLEEP_TIME_SECONDS);
  }

  s.loop();

  if (s.getState() == WaitingResponse) {
    // Don't sleep until we are registered
    Serial.println("Not registered yet, cannot sleep.");
    return;
  }

  Serial.println("Done sending, sleeping...");
  deepSleep(SLEEP_TIME_SECONDS);
}

void loop() {
  s.loop();
  delay(1000);

  if (s.getState() == Registered) {
    // We can sleep now, the device was registered
    Serial.println("Done sending, sleeping...");
    deepSleep(SLEEP_TIME_SECONDS);
  }
}
