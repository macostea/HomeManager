#include "arduino_hal.h"

#include <DHT.h>
#include <DHT_U.h>
#include <Adafruit_MQTT.h>
#include <Adafruit_MQTT_Client.h>
#include <uuid_gen.h>

#include "arduino_dht_client.h"
#include "arduino_mqtt_client.h"

#define DHTPIN D5
#define DHTTYPE DHT11

#define WLAN_SSID "risky click"
#define WLAN_PASS "dinozaurulfertil"
#define CONNECTION_ATTEMPTS 20

#define MQTT_SERVER "192.168.0.148"
#define MQTT_USERNAME "rabbit"
#define MQTT_PASS "rabbit"


std::string ArduinoHAL::getUUID() {
  String macAddr = WiFi.macAddress();
  macAddr.toLowerCase();

  return generateUUID(std::string(macAddr.c_str()));
}

ArduinoHAL::ArduinoHAL() {
    this->wifiClient = new WiFiClient();
    this->dhtClient = new ArduinoDHTClient(DHTPIN, DHTTYPE);
    this->mqttClient = new ArduinoMQTTClient(this->wifiClient, MQTT_SERVER, 1883, MQTT_USERNAME, MQTT_PASS);
}

unsigned long ArduinoHAL::getMillis() {
    return millis();
}

bool ArduinoHAL::connect() {
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
