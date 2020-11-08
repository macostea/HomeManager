#include "arduino_pir_client.h"
#include <Arduino.h>

ArduinoPIRClient::ArduinoPIRClient(int pin) {
    this->pin = pin;
}

ArduinoPIRClient::~ArduinoPIRClient() {

}

void ArduinoPIRClient::begin() {
    pinMode(pin, OUTPUT);
}

void ArduinoPIRClient::preventSleep(bool prevent) {
    digitalWrite(pin, prevent ? HIGH : LOW);
}
