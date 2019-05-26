#include <ESP8266WiFi.h>
#include <Adafruit_MQTT.h>
#include <Adafruit_MQTT_Client.h>
#include <Adafruit_Sensor.h>
#include <DHT.h>
#include <DHT_U.h>

#define WLAN_SSID ""
#define WLAN_PASS ""
#define CONNECTION_ATTEMPTS 20
#define SLEEP_TIME_SECONDS 900

#define MQTT_SERVER "192.168.0.11"
#define MQTT_USERNAME "rabbit"
#define MQTT_PASS "rabbit"

#define DHTPIN D5
#define DHTTYPE DHT11

#define SENSOR_ID 1

WiFiClient client;
Adafruit_MQTT_Client mqtt(&client, MQTT_SERVER, 1883, MQTT_USERNAME, MQTT_PASS);

uint32_t delayMS;

DHT_Unified dht(DHTPIN, DHTTYPE);

bool MQTT_connect() {
  int8_t ret;

  // Stop if already connected.
  if (mqtt.connected()) {
    Serial.println("Already connected, don't try to connect again");
    return true;
  }

  Serial.print("Connecting to MQTT... ");

  uint8_t retries = 3;
  while ((ret = mqtt.connect()) != 0) { // connect will return 0 for connected
       Serial.println(mqtt.connectErrorString(ret));
       Serial.println("Retrying MQTT connection in 5 seconds...");
       mqtt.disconnect();
       delay(5000);  // wait 5 seconds
       retries--;
       if (retries == 0) {
         return false;
       }
  }
  Serial.println("MQTT Connected!");
  return true;
}

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
  return MQTT_connect();
}

void setupSensor() {
  dht.begin();
}

void deepSleep(int seconds) {
  ESP.deepSleep(seconds * 1000000);
}

bool publish(const char *type, int sensorId, double temperature, double humidity) {
  Adafruit_MQTT_Publish publisher = Adafruit_MQTT_Publish(&mqtt, type, 1);

  String message = "{\"sensorId\":" + String(sensorId) + ", \"environment\":{\"temperature\":" + String(temperature) + ",\"humidity\":" + String(humidity) + "}}";

  bool result = publisher.publish(message.c_str());
  Serial.print(message.c_str());
  if (result) {
    Serial.print("Successfully published message");
  } else {
    Serial.print("Error publishing message");
  }

  return result;
}

void readAndPublish() {
  if (!mqtt.connected()) {
    if (!connect()) {
      Serial.println("Could not connect to MQTT broker, sleeping...");
      deepSleep(SLEEP_TIME_SECONDS);
    }
  }
  
  sensors_event_t event;
  double temperature;
  double humidity;
  dht.temperature().getEvent(&event);
  if (isnan(event.temperature)) {
    Serial.println("Error reading temperature!");
  } else {
    Serial.print("Temperature: ");
    Serial.print(event.temperature);
    Serial.println(" *C");
    temperature = event.temperature;
  }

  dht.humidity().getEvent(&event);

  if (isnan(event.relative_humidity)) {
    Serial.println("Error reading humidity!");
  } else {
    Serial.print("Humidity: ");
    Serial.print(event.relative_humidity);
    Serial.println(" %");
    humidity = event.relative_humidity;
  }

  bool result = publish("environment", SENSOR_ID, temperature, humidity);
  if (!result) {
    Serial.println("Could not publish MQTT message");
  }
}

void setup() {
  Serial.begin(115200);
  Serial.setTimeout(2000);

  Serial.println("Device wake up");
 
  if (!connect()) {
    Serial.println("Could not connect to MQTT broker, sleeping...");
    deepSleep(SLEEP_TIME_SECONDS);
  }
  setupSensor();
  readAndPublish();

  Serial.println("Done sending, sleeping...");
  deepSleep(SLEEP_TIME_SECONDS);
}

void loop() {

}
