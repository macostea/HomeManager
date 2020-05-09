#include "sensor.h"

#include "arduino_hal.h"

#define SLEEP_TIME_SECONDS 900


ArduinoHAL hal;
Sensor s(hal.getUUID(), "temp+hum", &hal);

void deepSleep(int seconds) {
  s.stop();
  ESP.deepSleep(seconds * 1000000);
}

void setup() {
  Serial.begin(115200);

  Serial.println("Device wake up");

  if (!hal.connect()) {
    Serial.println("Could not connect to WIFI, sleeping...");
    deepSleep(SLEEP_TIME_SECONDS);
  }

  s.setup();
  if (!hal.getMQTTClient()->connect()) {
    Serial.println("Could not connect to MQTT broker, sleeping...");
    deepSleep(SLEEP_TIME_SECONDS);
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

  if (s.getState() == Sleepy) {
    // We can sleep now, the device was registered and data was sent
    Serial.println("Done sending, sleeping...");
    deepSleep(SLEEP_TIME_SECONDS);
  }
}
