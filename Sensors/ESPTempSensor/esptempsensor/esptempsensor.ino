#include <ESP8266WiFi.h>
#include <Adafruit_MQTT.h>
#include <Adafruit_MQTT_Client.h>
#include <Adafruit_Sensor.h>
#include <DHT.h>
#include <DHT_U.h>

#define WLAN_SSID "risky click"
#define WLAN_PASS "dinozaurulfertil"

#define MQTT_SERVER "192.168.0.11"
#define MQTT_USERNAME "guest"
#define MQTT_PASS "guest"

#define DHTPIN D5
#define DHTTYPE DHT11

WiFiClient client;
Adafruit_MQTT_Client mqtt(&client, MQTT_SERVER, 1883, MQTT_USERNAME, MQTT_PASS);

uint32_t delayMS;

DHT_Unified dht(DHTPIN, DHTTYPE);

void MQTT_connect() {
  int8_t ret;

  // Stop if already connected.
  if (mqtt.connected()) {
    return;
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
         // basically die and wait for WDT to reset me
         while (1);
       }
  }
  Serial.println("MQTT Connected!");
}

void connect() {
  Serial.print("checking wifi...");
  WiFi.mode(WIFI_STA);
  WiFi.begin(WLAN_SSID, WLAN_PASS);
  while (WiFi.status() != WL_CONNECTED) {
    Serial.print(".");
    delay(1000);
  }

  Serial.println("");
  Serial.println("WiFi connected");
  Serial.println("IP address: ");
  Serial.println(WiFi.localIP());

  Serial.print("\nconnecting...");
  MQTT_connect(); 
  Serial.println("\nconnected!");
}

void setupSensor() {
  dht.begin();
}

void setup() {
  Serial.begin(115200);
 
  connect();
  setupSensor();
}

bool publishTemperature(double temperature) {
  Adafruit_MQTT_Publish temperaturePublish = Adafruit_MQTT_Publish(&mqtt, "temperature");

  bool result = temperaturePublish.publish("{\"sensorId\": 2, \"time\":\"2018-11-25T13:14:17Z\", \"reading\":21.0}");
  if (result) {
    Serial.print("Successfully published message");
  } else {
    Serial.print("Error publishing message");
  }
}

void loop() {
  if (!mqtt.connected()) {
    connect();
  }

  delay(delayMS);
  sensors_event_t event;
  dht.temperature().getEvent(&event);
  if (isnan(event.temperature)) {
    Serial.println("Error reading temperature!");
  } else {
    Serial.print("Temperature: ");
    Serial.print(event.temperature);
    Serial.println(" *C");
  }
}
