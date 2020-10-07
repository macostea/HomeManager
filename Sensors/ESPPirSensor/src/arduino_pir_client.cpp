#include "arduino_pir_client.h"
#include <Arduino.h>

ArduinoPIRClient::ArduinoPIRClient(int pin) {
    this->pin = pin;
}

void ArduinoPIRClient::preventSleep(bool prevent) {
    digitalWrite(pin, prevent ? HIGH : LOW);
}
