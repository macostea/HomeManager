#include "arduino_dht_client.h"

#include <DHT.h>
#include <DHT_U.h>


ArduinoDHTClient::ArduinoDHTClient(const uint8_t pin, int type) {
    this->pin = pin;
    this->type = type;

    this->dht = new DHT_Unified(pin, type);
}

void ArduinoDHTClient::begin() {
    this->dht->begin();
}

void ArduinoDHTClient::end() {
    delete this->dht;
}

bool ArduinoDHTClient::getEnvironment(Environment *environment) {
    sensors_event_t event;

    this->dht->temperature().getEvent(&event);
    if (isnan(event.temperature)) {
        return false;
    } else {
        environment->temperature = event.temperature;
    }

    this->dht->humidity().getEvent(&event);
    if (isnan(event.relative_humidity)) {
        return false;
    } else {
        environment->humidity = event.relative_humidity;
    }

    return true;
}