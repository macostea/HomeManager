#include "arduino_homey_client.h"


ArduinoHomeyClient::ArduinoHomeyClient() {
    this->client = new HomeyClass();
}

ArduinoHomeyClient::~ArduinoHomeyClient() {
    delete this->client;
}

bool ArduinoHomeyClient::begin(std::string id) {
    this->client->begin(String(id.c_str()));
    this->client->setClass("sensor");
    bool humRes = this->client->addCapability("measure_humidity");
    bool tempRes = this->client->addCapability("measure_temperature");

    return humRes && tempRes;
}

bool ArduinoHomeyClient::updateTemperature(double temperature) {
    return this->client->setCapabilityValue("measure_temperature", temperature);
}
bool ArduinoHomeyClient::updateHumidity(double humidity) {
    return this->client->setCapabilityValue("measure_humidity", humidity);
}
bool ArduinoHomeyClient::loop() {
    return this->client->loop();
}
