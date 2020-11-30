#include <ESP8266WiFi.h>
#include <DHT.h>
#include <DHT_U.h>

#include "sensor.h"
#include "arduino_dht_client.h"
#include "arduino_mqtt_client.h"
#include "uuid_gen.h"

#define DHTPIN D5
#define DHTTYPE DHT11

#ifndef WLAN_SSID
  #error "WLAN_SSID must be defined"
#endif

#ifndef WLAN_PASS
  #error "WLAN_PASS must be defined"
#endif

#define CONNECTION_ATTEMPTS 20
#define SLEEP_TIME_SECONDS 900

#ifndef MQTT_SERVER
  #error "MQTT_SERVER must be defined"
#endif

#ifndef MQTT_USERNAME
  #error "MQTT_USERNAME must be defined"
#endif

#ifndef MQTT_PASS
  #error "MQTT_PASS must be defined"
#endif

#define MKSTR( x ) STR(x)
#define STR( x ) #x

WiFiClient client;

ArduinoDHTClient dhtClient(DHTPIN, DHTTYPE);
ArduinoMQTTClient mqttClient(&client, MKSTR(MQTT_SERVER), 1883, MKSTR(MQTT_USERNAME), MKSTR(MQTT_PASS));

unsigned long mqttWaitTimeoutPrevious = 0;
const unsigned long mqttWaitTimeoutInterval = 30000;

std::string getUUID() {
  String macAddr = WiFi.macAddress();
  macAddr.toLowerCase();

  return generateUUID(std::string(macAddr.c_str()));
}

Sensor s(getUUID(), "temp+hum", &dhtClient, &mqttClient);

bool connect() {
  Serial.print("checking wifi...");
  int try_number = 0;
  
  WiFi.mode(WIFI_STA);
  WiFi.begin(MKSTR(WLAN_SSID), MKSTR(WLAN_PASS));
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
