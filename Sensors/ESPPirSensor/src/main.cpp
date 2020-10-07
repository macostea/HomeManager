#include <ESP8266WiFi.h>

#include "sensor.h"
#include "arduino_mqtt_client.h"
#include "arduino_homey_client.h"
#include "arduino_pir_client.h"
#include "uuid_gen.h"

#define PIR_SLEEP_PIN D5

#define WLAN_SSID ""
#define WLAN_PASS ""
#define CONNECTION_ATTEMPTS 20
#define SLEEP_TIME_SECONDS 900

#define MQTT_SERVER ""
#define MQTT_USERNAME ""
#define MQTT_PASS ""

WiFiClient client;

ArduinoMQTTClient mqttClient(&client, MQTT_SERVER, 1883, MQTT_USERNAME, MQTT_PASS);
ArduinoHomeyClient homeyClient;
ArduinoPIRClient pirClient(PIR_SLEEP_PIN);

unsigned long homeyRegisterTimeoutPrevious = 0;
const unsigned long homeyRegisterTimeoutInterval = 120000;

unsigned long mqttWaitTimeoutPrevious = 0;
const unsigned long mqttWaitTimeoutInterval = 30000;

std::string getUUID() {
  String macAddr = WiFi.macAddress();
  macAddr.toLowerCase();

  return generateUUID(std::string(macAddr.c_str()));
}

Sensor s(getUUID(), "temp+hum", &mqttClient, &homeyClient, &pirClient);

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

  Serial.println("Device wake up");

  if (!connect()) {
    Serial.println("Could not connect to WIFI, sleeping...");
    deepSleep(SLEEP_TIME_SECONDS);
  }

  s.setup();
  if (!mqttClient.connect()) {
    Serial.println("Could not connect to MQTT broker, moving on...");
    // TODO: Check here if we can continue because homey publish makes sense but mqtt does not. It will always fail
  }

  s.loop();

  homeyRegisterTimeoutPrevious = millis();

  if (s.getState() == WaitingResponse) {
    // Don't sleep until we are registered
    Serial.println("Not registered yet, cannot sleep.");
    return;
  }

  if (s.getState() == Sleepy) {
    Serial.println("Done sending, sleeping...");
    deepSleep(SLEEP_TIME_SECONDS);
  }
}

void loop() {
  s.loop();

  if (s.getState() == HomeyUnregistered) {
    unsigned long currentMillis = millis();
    if (currentMillis - homeyRegisterTimeoutPrevious > homeyRegisterTimeoutInterval) {
      s.homeyRegisterTimeout();
    }
  }

  if (s.getState() == WaitingResponse) {
    if (mqttWaitTimeoutPrevious == 0) {
      mqttWaitTimeoutPrevious = millis();
    } else {
      unsigned long currentMillis = millis();
      if (currentMillis - mqttWaitTimeoutPrevious > mqttWaitTimeoutInterval) {
        s.becomeSleepy();
      }
    }
  }

  if (s.getState() == Sleepy) {
    // We can sleep now, the device was registered and data was sent
    Serial.println("Done sending, sleeping...");
    deepSleep(SLEEP_TIME_SECONDS);
  }
}
